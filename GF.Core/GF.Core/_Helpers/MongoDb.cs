using GF.Common;
using GF.Common.Translations;
using GF.Core.Attributes;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("GF.SimSig")]
namespace GF.Core
{
    internal static partial class Helpers
    {
        internal static class MongoDb
        {

            /// <summary>
            /// Saves the supplied entity to the Mongo database
            /// </summary>
            /// <typeparam name="TGFEntity"></typeparam>
            /// <param name="entity">The entity to save</param>
            /// <param name="currentUser">The user making the save. They must have the necessary role on the supplied entity to either create or update the entity as appropriate</param>
            /// <param name="database">The MongoDb database</param>
            /// <param name="session">The MongoDb session</param>
            /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
            /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
            /// <returns></returns>
            /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
            /// <exception cref="GFAuthorisationException">If the <paramref name="currentUser"/> doesn't have the appropriate permissions to perform the save</exception>
            /// <exception cref="GFEntityNotFoundException">If the entity is supplied with an Id but that Id doesn't exist in the collection</exception>
            internal static async Task SaveToDbAsync<TGFEntity>(TGFEntity entity, GFUserEntity currentUser, IMongoDatabase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null) where TGFEntity : IGFEntity<TGFEntity>
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(entity), callingMethod), "You must supply the entity to save");
                }

                if (currentUser == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(currentUser), callingMethod), "You must supply the current user object");
                }

                if (database == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(database), callingMethod), "You must supply the MongoDb database to connect to");
                }

                if (session == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(session), callingMethod), "You must supply the MongoDb session");
                }

                if (logger == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply the logger");
                }

                bool newRecord = false;
                DateTime lastModifiedOnUtc = entity.ModifiedOnUtc;
                Guid lastModifiedById = entity.ModifiedById;

                try
                {
                    var utcNow = DateTime.UtcNow; 

                    if (entity is GFUserEntity && string.IsNullOrEmpty(entity.Key))
                    {
                        logger.Verbose<string, Guid>("The entity supplied is a GFUserEntity object and contains a new user (emailaddress: {Email}). The Azure AD B2C id is {AzureAdId}.", (entity as GFUserEntity)!.Email, entity.Id);
                        //if the entity is a user and the key is empty the Id will already be assigned from the Azure B2C id
                        newRecord= true;
                    }
                    else if (entity.Id == default)
                    {
                        //if the id is default then it's a new record and a new id needs to be generated
                        entity.Id = Guid.NewGuid();
                        logger.Verbose<string, Guid>("The entity supplied is a {EntityType} object and contains a new user. The newly issued is {Id}.", entity.GetType().Name, entity.Id);
                        newRecord = true;
                    }

                    var collection = database.GetCollection<TGFEntity>(GetMongoDbCollectionName<TGFEntity>());

                    //if a new record check it doesn't exist and if doesn't throw an GFEntityNotFoundException exception
                    if (!newRecord)
                    {
                        logger.Information<string, Guid>("Saving existing {EntityType} with id {Id}.", entity.GetType().Name, entity.Id);

                        //check the user has permission to update this entity
                        if (!await CanUpdateEntityAsync<TGFEntity>(entity.Id, currentUser, database, session, logger, cancellationToken))
                        {
                            throw GFAuthorisationException.Build<TGFEntity>(currentUser);
                        }

                        var filter = Builders<TGFEntity>.Filter.Eq("_id", entity.Id);

                        if (await collection.CountDocumentsAsync(session, filter, new CountOptions(), cancellationToken) == 0)
                        {
                            throw GFEntityNotFoundException.Build<string, Guid>($"The {typeof(TGFEntity).Name} entity with Id {entity.Id} does not exist in the database.", "Entity.NotExists", typeof(TGFEntity).Name, entity.Id);
                        }

                        entity.ModifiedOnUtc = utcNow;
                        entity.ModifiedById = currentUser.Id;

                        await collection.ReplaceOneAsync(session, filter, entity, new ReplaceOptions(), cancellationToken);
                    }
                    else
                    {
                        logger.Information<string, Guid>("Creating new {EntityType} with id {Id}.", entity.GetType().Name, entity.Id);

                        //check the user has permission to create this entity
                        if (!CanCreateEntity<TGFEntity>(currentUser))
                        {
                            throw GFAuthorisationException.Build<TGFEntity>(currentUser);
                        }

                        //generate the key
                        entity.Key = await Helpers.Keys.GenerateAsync<TGFEntity>(currentUser, database, session, logger, cancellationToken);

                        entity.CreatedOnUtc = utcNow;
                        entity.ModifiedOnUtc= utcNow;
                        entity.CreatedById = currentUser.Id;
                        entity.ModifiedById = currentUser.Id;

                        await collection.InsertOneAsync(session, entity, new InsertOneOptions(), cancellationToken);
                    }
                }
                catch (OperationCanceledException)
                {
                    //undo any changes to the entity
                    if (newRecord)
                    {
                        entity.CreatedOnUtc = default;
                        entity.ModifiedOnUtc = default;
                        entity.CreatedById = default;
                        entity.ModifiedById = default;
                    }
                    else
                    {
                        entity.ModifiedById = lastModifiedById;
                        entity.ModifiedOnUtc = lastModifiedOnUtc;
                    }
                }
                catch (Common.GFAuthorisationException)
                {
                    throw;
                }
                catch (GFEntityNotFoundException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error<string>(ex, "An error has occurred trying to get the save the entity {Entity}", typeof(TGFEntity).Name);
                    throw GFException.Build<string>(ex, $"An error has occurred trying to save entity {typeof(TGFEntity).Name}.", "Entity.SaveError", typeof(TGFEntity).Name);
                }
            }

            /// <summary>
            /// Loads the an with the supplied Id
            /// </summary>
            /// <typeparam name="TGFEntity">The target entity type</typeparam>
            /// <param name="id">The id of the entity to load</param>
            /// <param name="currentUser">The <see cref="GFUserEntity"/> loading the entity. Must have the necessary read rights on the entity</param>
            /// <param name="database">The MongoDb database</param>
            /// <param name="session">The MongoDb session</param>
            /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
            /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
            /// <param name="callingMethod">The calling method name. Do not pass</param>
            /// <returns>The Id of the entity</returns>
            /// <exception cref="ArgumentNullException">If <paramref name="id"/> is an empty Guid</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
            /// <exception cref="GFAuthorisationException">If the <paramref name="currentUser"/> does not have read rights on the supplied <typeparamref name="TGFEntity"/></exception>
            /// <exception cref="GFEntityNotFoundException">If the <typeparamref name="TGFEntity"/> with the supplied <paramref name="key"/> isn't found in the MongoDb</exception>
            internal static async Task<TGFEntity> LoadFromDbAsync<TGFEntity>(Guid id, GFUserEntity currentUser, IMongoDatabase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null) where TGFEntity : IGFEntity<TGFEntity>
            {
                //check the user has permission to read this entity
                if (!CanReadEntity<TGFEntity>(currentUser))
                {
                    throw GFAuthorisationException.Build<TGFEntity>(currentUser);
                }

                return await LoadFromDbAsync<TGFEntity>(id, database, session, logger, cancellationToken, callingMethod);
            }

            /// <summary>
            /// Gets the Id of the supplied entity type and key
            /// </summary>
            /// <typeparam name="TGFEntity">The target entity type</typeparam>
            /// <param name="key">The key of the entity to find</param>
            /// <param name="currentUser">The <see cref="GFUserEntity"/> making getting the Id. Must have the necessary read rights on the entity</param>
            /// <param name="database">The MongoDb database</param>
            /// <param name="session">The MongoDb session</param>
            /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
            /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
            /// <param name="callingMethod">The calling method name. Do not pass</param>
            /// <returns>The Id of the entity</returns>
            /// <exception cref="ArgumentNullException">If <paramref name="key"/> is an empty string or null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
            /// <exception cref="GFAuthorisationException">If the <paramref name="currentUser"/> does not have read rights on the supplied <typeparamref name="TGFEntity"/></exception>
            /// <exception cref="GFEntityNotFoundException">If the <typeparamref name="TGFEntity"/> with the supplied <paramref name="key"/> isn't found in the MongoDb</exception>
            internal static async Task<Guid> GetIdFromKeyAsync<TGFEntity>(string key, GFUserEntity currentUser, IMongoDatabase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null) where TGFEntity : IGFEntity<TGFEntity>
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(key), callingMethod), "You must supply the entity key");
                }

                if (currentUser == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(currentUser), callingMethod), "You must supply the current user object");
                }

                if (database == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(database), callingMethod), "You must supply the MongoDb database to connect to");
                }

                if (session == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(session), callingMethod), "You must supply the MongoDb session");
                }

                if (logger == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply the logger");
                }

                try
                {
                    //check the user has permission to read this entity
                    if (!CanReadEntity<TGFEntity>(currentUser))
                    {
                        throw GFAuthorisationException.Build<TGFEntity>(currentUser);
                    }

                    var collection = database.GetCollection<TGFEntity>(GetMongoDbCollectionName<TGFEntity>());
                    var filter = Builders<TGFEntity>.Filter.Eq("key", key);


                    if (await collection.CountDocumentsAsync(session, filter, new CountOptions(), cancellationToken) == 0)
                    {
                        throw GFEntityNotFoundException.Build<string, string>($"The {typeof(TGFEntity).Name} entity with key {key} does not exist in the database.", "Entity.NotExists", typeof(TGFEntity).Name, key);
                    }
                    else
                    {
                        return await collection.Find(filter, new FindOptions()).Project(new ProjectionDefinitionBuilder<TGFEntity>().Expression(x => x.Id)).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                    }
                }
                catch (Common.GFAuthorisationException)
                {
                    throw;
                }
                catch (GFEntityNotFoundException)
                {
                    throw;
                }
                catch (Exception ex) 
                {
                    logger.Error<string, string>(ex, "An error has occurred trying to get the Id for key {Key} in entity {Entity}", key, typeof(TGFEntity).Name);
                    throw GFEntityNotFoundException.Build<string, string>(ex, $"An error has occurred trying to ascertain the Id of {typeof(TGFEntity).Name} entity with key {key}.", "Entity.GetIdFromKey", typeof(TGFEntity).Name, key);
                }
            }

            /// <summary>
            /// Loads the an with the supplied Id
            /// </summary>
            /// <typeparam name="TGFEntity">The target entity type</typeparam>
            /// <param name="id">The id of the entity to load</param>
            /// <param name="database">The MongoDb database</param>
            /// <param name="session">The MongoDb session</param>
            /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
            /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
            /// <param name="callingMethod">The calling method name. Do not pass</param>
            /// <returns>The Id of the entity</returns>
            /// <exception cref="ArgumentNullException">If <paramref name="id"/> is an empty Guid</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
            /// <exception cref="GFEntityNotFoundException">If the <typeparamref name="TGFEntity"/> with the supplied <paramref name="key"/> isn't found in the MongoDb</exception>
            /// <remarks>This method doesn't not apply any authorisation checks and should remain a private method. The calling method must check authorisation</remarks>
            private static async Task<TGFEntity> LoadFromDbAsync<TGFEntity>(Guid id, IMongoDatabase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null) where TGFEntity : IGFEntity<TGFEntity>
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(id), callingMethod), "You must supply the entity id");
                }

                if (database == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(database), callingMethod), "You must supply the MongoDb database to connect to");
                }

                if (session == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(session), callingMethod), "You must supply the MongoDb session");
                }

                if (logger == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply the logger");
                }

                try
                {
                    var collection = database.GetCollection<TGFEntity>(GetMongoDbCollectionName<TGFEntity>());

                    var filter = Builders<TGFEntity>.Filter.Eq("_id", id);

                    if (await collection.CountDocumentsAsync(session, filter, new CountOptions(), cancellationToken) == 0)
                    {
                        throw GFEntityNotFoundException.Build<string, Guid>($"The {typeof(TGFEntity).Name} entity with Id {id} does not exist in the database.", "Entity.NotExists", typeof(TGFEntity).Name, id);
                    }
                    else
                    {
                        return await collection.Find(filter, new FindOptions()).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                    }
                }
                catch (GFEntityNotFoundException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error<string, Guid, string>(ex, "An error has occurred trying to load the entity {Entity} with Id {Id} from the MongoDb {DatabaseName}", typeof(TGFEntity).Name, id, database.DatabaseNamespace.DatabaseName);
                    throw GFException.Build<string, Guid>(ex, $"An error has occurred trying to load entity {typeof(TGFEntity).Name} with Id {id}.", "Entity.SaveError", typeof(TGFEntity).Name, id);
                }

            }

            /// <summary>
            /// Returns the Mongo collection name for the supplied type
            /// </summary>
            /// <typeparam name="TGFEntity">The entity type to get the collection name from. Must implement <see cref="IGFEntity{T}"/></typeparam>
            /// <returns></returns>
            /// <exception cref="GFException">If the supplied entity is not decoratd with the <see cref="GFMongoAttribute"/> attribute</exception>
            internal static string GetMongoDbCollectionName<TGFEntity>() where TGFEntity : IGFEntity<TGFEntity>
            {


                if (typeof(TGFEntity).GetCustomAttributes(
                        typeof(GFMongoAttribute), true
                    ).FirstOrDefault() is GFMongoAttribute mongoAttribute)
                {
                    return mongoAttribute.CollectionName;
                }
                else
                {
                    throw GFException.Build<string>("The supplied entity is not decorated with a GFMongoAttribute", "MissingAttribute.GFMongoAttribute", typeof(TGFEntity).Name);
                };
            }

            /// <summary>
            /// Determines whether the supplied user can create the supplied entity type
            /// </summary>
            /// <typeparam name="TGFEntity">The target entity type. Must inherit <see cref="IGFEntity{T}"/></typeparam>
            /// <param name="user">The target user"/></param>
            /// <param name="callingMethod">The calling method name. Do not pass</param>
            /// <returns>True if the user can create the entity type otherwise false</returns>
            /// <exception cref="ArgumentNullException">If <paramref name="user"/> is null</exception>
            internal static bool CanCreateEntity<TGFEntity>(GFUserEntity user, [CallerMemberName] string? callingMethod = null) where TGFEntity : IGFEntity<TGFEntity>
            {
                if (user == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(user), callingMethod), "You must supply the user to check the authorisation for");
                }

                if (typeof(TGFEntity).GetCustomAttributes(
                        typeof(GFMongoAttribute), true
                    ).FirstOrDefault() is GFMongoAttribute mongoAttribute)
                {
                    if (user.Role >= mongoAttribute.CreateRole)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw GFException.Build<string>("The supplied entity is not decorated with a GFMongoAttribute", "MissingAttribute.GFMongoAttribute", typeof(TGFEntity).Name);
                };
            }

            /// <summary>
            /// Determines whether the supplied user can read the supplied entity type
            /// </summary>
            /// <typeparam name="TGFEntity">The target entity type. Must inherit <see cref="IGFEntity{T}"/></typeparam>
            /// <param name="user">The target user"/></param>
            /// <returns>True if the user can read the entity type otherwise false</returns>
            /// <exception cref="ArgumentNullException">If <paramref name="user"/> is null</exception>
            internal static bool CanReadEntity<TGFEntity>(GFUserEntity user, [CallerMemberName] string? callingMethod = null) where TGFEntity : IGFEntity<TGFEntity>
            {
                if (user == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(user), callingMethod), "You must supply the user to check the authorisation for");
                }

                if (typeof(TGFEntity).GetCustomAttributes(
                        typeof(GFMongoAttribute), true
                    ).FirstOrDefault() is GFMongoAttribute mongoAttribute)
                {
                    if (user.Role >= mongoAttribute.ReadRole)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw GFException.Build<string>("The supplied entity is not decorated with a GFMongoAttribute", "MissingAttribute.GFMongoAttribute", typeof(TGFEntity).Name);
                };
            }

            /// <summary>
            /// Determines whether the supplied user can update the supplied entity type
            /// </summary>
            /// <typeparam name="TGFEntity">The target entity type. Must inherit <see cref="IGFEntity{T}"/></typeparam>
            /// <param name="id">The id of the record the user is trying to update</param>
            /// <param name="user">The target user"/></param>
            /// <param name="database">The MongoDb database</param>
            /// <param name="session">The current MongoDb session</param>
            /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
            /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
            /// <param name="callingMethod">The calling method name. Do not pass</param>
            /// <returns>True if the user can update the entity type otherwise false</returns>
            /// <exception cref="ArgumentNullException">If <paramref name="id"/> is an empty Guid</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="user"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
            /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
            internal static async Task<bool> CanUpdateEntityAsync<TGFEntity>(Guid id, GFUserEntity user, IMongoDatabase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null) where TGFEntity : IGFEntity<TGFEntity>
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(id), callingMethod), "You must supply the entity id");
                }

                if (user == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(user), callingMethod), "You must supply the user to check the authorisation for");
                }

                if (database == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(database), callingMethod), "You must supply the MongoDb database to connect to");
                }

                if (session == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(session), callingMethod), "You must supply the MongoDb session");
                }

                if (logger == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply the logger");
                }

                if (typeof(TGFEntity).GetCustomAttributes(
                        typeof(GFMongoAttribute), true
                    ).FirstOrDefault() is GFMongoAttribute mongoAttribute)
                {
                    if (!mongoAttribute.AllowOwnerEdit)
                    {
                        if (user.Role >= mongoAttribute.UpdateRole)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        //get the entity from the MongoDb. Uses the method that doesn't apply security
                        var entity = await LoadFromDbAsync<TGFEntity>(id, database, session, logger, cancellationToken);

                        if (entity.OwnerId == user.Id)
                        {
                            return true;
                        }
                        else
                        {
                            if (user.Role >= mongoAttribute.UpdateRole)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    throw GFException.Build<string>("The supplied entity is not decorated with a GFMongoAttribute", "MissingAttribute.GFMongoAttribute", typeof(TGFEntity).Name);
                };
            }
        }

        /// <summary>
        /// Determines whether the supplied user can delete the supplied entity type
        /// </summary>
        /// <typeparam name="TGFEntity">The target entity type. Must inherit <see cref="IGFEntity{T}"/></typeparam>
        /// <param name="user">The target user"/></param>
        /// <returns>True if the user can delete the entity type otherwise false</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="user"/> is null</exception>
        internal static bool CanDeleteEntity<TGFEntity>(GFUserEntity user, [CallerMemberName] string? callingMethod = null) where TGFEntity : IGFEntity<TGFEntity>
        {
            if (user == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(user), callingMethod), "You must supply the user to check the authorisation for");
            }

            if (typeof(TGFEntity).GetCustomAttributes(
                    typeof(GFMongoAttribute), true
                ).FirstOrDefault() is GFMongoAttribute mongoAttribute)
            {
                if (user.Role >= mongoAttribute.DeleteRole)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw GFException.Build<string>("The supplied entity is not decorated with a GFMongoAttribute", "MissingAttribute.GFMongoAttribute", typeof(TGFEntity).Name);
            };
        }
    }
}
