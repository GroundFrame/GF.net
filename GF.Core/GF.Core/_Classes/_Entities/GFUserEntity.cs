using GF.Common;
using GF.Common.Translations;
using GF.Core.Attributes;
using MongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using SharpCompress.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static GF.Core.Helpers;

[assembly: InternalsVisibleTo("GF.SimSig.UnitTests")]
[assembly: InternalsVisibleTo("GF.SimSig")]
namespace GF.Core
{
    [GFMongoAttribute("users", createRole: Role.SysAdmin, readRole: Role.Default, updateRole: Role.SysAdmin, deleteRole: Role.SysAdmin, allowOwnerEdit: true)]
    internal class GFUserEntity : GFBaseEntity, IGFEntity<GFUserEntity>
    {
        #region Members

        /// <summary>
        /// Gets or sets the user's first name
        /// </summary>
        [BsonElement("firstName")]
        internal string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name
        /// </summary>
        [BsonElement("lastName")]
        internal string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's email address
        /// </summary>
        [BsonElement("email")]
        internal string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's user name
        /// </summary>
        [BsonElement("username")]
        internal string Username { get; set; }

        /// <summary>
        /// Gets or sets the user's role
        /// </summary>
        [BsonElement("role")]
        internal Role Role { get; set; }

        #endregion Members

        /// <summary>
        /// Instantiates a new user entity.
        /// </summary>
        /// <param name="newUser">The source <see cref="GFUser"/> containing the new user details</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/> logger used to log this object</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException"></exception>
        private GFUserEntity (GFUserNew newUser, Serilog.ILogger logger, [CallerMemberName] string? callingMethod = null) : base(Guid.Empty, logger.BuildMethodContext<GFUserEntity>(callingMethod))
        {
            //log arguments
            base.Logger.LogMethodArgument<GFUserNew>(nameof(newUser), newUser);

            //validate arguments
            if (newUser == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(newUser), callingMethod), "You must supply a new user");
            }

            FirstName = newUser.FirstName;
            LastName = newUser.LastName;
            Email = newUser.Email;
            Username= newUser.Username;
            Role = Role.Default;
        }

        /// <summary>
        /// Builds a new <see cref="GFUserEntity"/> from the supplied <see cref="GFUserNew"/> object
        /// </summary>
        /// <param name="activeDirectoryId">The active directory B2C id of the user being created</param>
        /// <param name="newUser">A <see cref="GFUser"/> containing the details of the new user to create</param>
        /// <param name="currentUser">The <see cref="GFUserEntity"/> initialising the user. Must have create rights on the <see cref="GFUserEntity"/> entity</param>
        /// <param name="database">The MongoDb database</param>
        /// <param name="session">The MongoDb session</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="activeDirectoryId"/> is an empty guid</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="newUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        /// <exception cref="GFException">If another user already exists with the supplied email address</exception>
        internal static async Task<GFUserEntity> BuildAsync(Guid activeDirectoryId, GFUserNew newUser, GFUserEntity currentUser, MongoDatabaseBase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply the logger");
            }

            //set up logger
            Serilog.ILogger methodLogger = logger.BuildMethodContext<GFUserEntity>(callingMethod);

            //log arguments
            methodLogger.LogMethodArgument<Guid>(nameof(activeDirectoryId), activeDirectoryId);
            methodLogger.LogMethodArgument<GFUserNew>(nameof(newUser), newUser);
            methodLogger.LogMethodArgument<GFUserEntity>(nameof(currentUser), currentUser);
            methodLogger.LogMethodArgument<MongoDatabaseBase>(nameof(database), database);
            methodLogger.LogMethodArgument<IClientSessionHandle>(nameof(session), session);

            //validate arguments
            if (activeDirectoryId == default)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(activeDirectoryId), callingMethod), "You must supply the active directory B2C id for the user");
            }

            if (newUser == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(newUser), callingMethod), "You must supply a user object");
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

            //start the stopwatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                //instantiate the new user
                GFUserEntity newUserEntity = new(newUser, methodLogger)
                {
                    Id = activeDirectoryId //set the id to be active directory B2C id
                };

                //save the new record to the database
                await newUserEntity.SaveToDbAsync(currentUser, database, session, cancellationToken);

                //return the new user
                return newUserEntity;
            }
            catch (Exception ex)
            {
                methodLogger.Error<GFUserNew, Guid>(ex, "An error has occurred trying to build the new user {NewUser} with Active Directory {ActiveDirectoryId}", newUser, activeDirectoryId);
                throw;
            }
            finally
            {
                methodLogger.LogDuration(stopwatch);
            }
        }

        /// <summary>
        /// Initialises the user with the supplied key from the MongoDb
        /// </summary>
        /// <param name="key">The key of the user to initialise</param>
        /// <param name="currentUser">The <see cref="GFUserEntity"/> initialising the user. Must have read rights on the <see cref="GFUserEntity"/> entity</param>
        /// <param name="database">The MongoDb database</param>
        /// <param name="session">The MongoDb session</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> is an empty string or null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        /// <exception cref="GFAuthorisationException">If the <paramref name="currentUser"/> does not have read rights on the <see cref="GFUserEntity"/> entity</exception>
        /// <exception cref="GFEntityNotFoundException">If the user with the supplied <paramref name="key"/> isn't found in the MongoDb</exception>
        public static async Task<GFUserEntity> InitialiseAsync(string key, GFUserEntity currentUser, MongoDatabaseBase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply the logger");
            }

            //set up logger
            Serilog.ILogger methodLogger = logger.BuildMethodContext<GFUserEntity>(callingMethod);

            //log arguments
            methodLogger.LogMethodArgument<string>(nameof(key), key);
            methodLogger.LogMethodArgument<GFUserEntity>(nameof(currentUser), currentUser);
            methodLogger.LogMethodArgument<MongoDatabaseBase>(nameof(database), database);
            methodLogger.LogMethodArgument<IClientSessionHandle>(nameof(session), session);

            //validate arguments
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(key), callingMethod), "You must supply the key of the user to initialise");
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

            //start the stopwatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                Guid id = await Helpers.MongoDb.GetIdFromKeyAsync<GFUserEntity>(key, currentUser, database, session, methodLogger, cancellationToken);
                return await GFUserEntity.InitialiseAsync(id, currentUser, database, session, methodLogger, cancellationToken);
            }
            catch (Exception ex)
            {
                methodLogger.Error<string>(ex, "An error has occurred trying to initialise customer with key {CustomerKey}", key);
                throw;
            }
            finally
            {
                //log the duration
                methodLogger.LogDuration(stopwatch);
            }
        }

        /// <summary>
        /// Initialises the user with the supplied id from the MongoDb
        /// </summary>
        /// <param name="id">The id of the user to initialise</param>
        /// <param name="currentUser">The <see cref="GFUserEntity"/> initialising the user. Must have read rights on the <see cref="GFUserEntity"/> entity</param>
        /// <param name="database">The MongoDb database</param>
        /// <param name="session">The MongoDb session</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is an empty guid</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        /// <exception cref="GFAuthorisationException">If the <paramref name="currentUser"/> does not have read rights on the <see cref="GFUserEntity"/> entity</exception>
        /// <exception cref="GFEntityNotFoundException">If the user with the supplied <paramref name="key"/> isn't found in the MongoDb</exception>
        public static async Task<GFUserEntity> InitialiseAsync(Guid id, GFUserEntity currentUser, MongoDatabaseBase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply the logger");
            }

            //set up logger
            Serilog.ILogger methodLogger = logger.BuildMethodContext<GFUserEntity>(callingMethod);

            //log arguments
            methodLogger.LogMethodArgument<Guid>(nameof(id), id);
            methodLogger.LogMethodArgument<GFUserEntity>(nameof(currentUser), currentUser);
            methodLogger.LogMethodArgument<MongoDatabaseBase>(nameof(database), database);
            methodLogger.LogMethodArgument<IClientSessionHandle>(nameof(session), session);

            //validate parameters
            if (id == default)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(id), callingMethod), "You must supply the id of the user to initialise");
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

            //start the stopwatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                return await Helpers.MongoDb.LoadFromDbAsync<GFUserEntity>(id, currentUser, database, session, logger, cancellationToken);
            }
            catch (Exception ex)
            {
                methodLogger.Error<Guid>(ex, "An error has occurred trying to initialise customer with Active Directory id {Active Directory}", id);
                throw;
            }
            finally
            {
                //log the duration
                methodLogger.LogDuration(stopwatch);
            }
        }

        /// <summary>
        /// Saves this user entity to the MongoDb
        /// </summary>
        /// <param name="currentUser">The <see cref="GFUserEntity"/> making the save</param>
        /// <param name="database">The MongoDb database</param>
        /// <param name="session">The MongoDb session</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        public async Task SaveToDbAsync(GFUserEntity currentUser, MongoDatabaseBase database, IClientSessionHandle session, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null)
        {
            if (base.Logger == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(base.Logger), callingMethod), "You must supply the logger");
            }

            //set up logger
            Serilog.ILogger methodLogger = base.Logger.BuildMethodContext<GFUserEntity>(callingMethod);

            //log arguments
            methodLogger.LogMethodArgument<GFUserEntity>(nameof(currentUser), currentUser);
            methodLogger.LogMethodArgument<MongoDatabaseBase>(nameof(database), database);
            methodLogger.LogMethodArgument<IClientSessionHandle>(nameof(session), session);

            //validate parameters
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

            //start the stopwatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {

                //handle new user entity if the key is null or an empty string
                if (string.IsNullOrEmpty(this.Key))
                {
                    try
                    {
                        methodLogger.Verbose("This is a new user (the key is not populated). Checking whether the email address or username has already been taken.");

                        //check user can read the user record
                        if (!Helpers.MongoDb.CanCreateEntity<GFUserEntity>(currentUser))
                        {
                            throw GFAuthorisationException.Build<GFUserEntity>(currentUser);
                        }

                        var collection = database.GetCollection<GFUserEntity>(Helpers.MongoDb.GetMongoDbCollectionName<GFUserEntity>());
                        var filter = Builders<GFUserEntity>.Filter.Eq("email", this.Email);
                        filter |= Builders<GFUserEntity>.Filter.Eq("username", this.Username);

                        if (await collection.CountDocumentsAsync(session, filter, new CountOptions(), cancellationToken) != 0)
                        {
                            var existingUser = await collection.Find(session, filter, new FindOptions()).FirstOrDefaultAsync(cancellationToken);

                            if (existingUser.Email == this.Email)
                            {
                                throw GFException.Build($"The email address is already in use.", "GFUserEntity.EmailInUse");
                            }
                            else
                            {
                                throw GFException.Build($"The username is already in use.", "GFUserEntity.UsernameInUse");
                            }
                        }
                    }
                    catch (Common.GFAuthorisationException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        throw GFException.Build<string>(ex, "An error has occurred trying to save the new user (Email Address: {EmailAddress}) to the database", "GFUserEntity.SaveNewUser.Error", this.Email);
                    }
                }
                else
                {
                    methodLogger.Verbose("This is an existing user.");
                }

                await base.SaveToDbAsync(this, currentUser, database, session, methodLogger, cancellationToken);
            }
            catch (Common.GFAuthorisationException)
            {
                methodLogger.Error<string, Guid>("The user {CurrentUserEmail} (Id: {CurrentUserId}) does not have permission to save this user.", currentUser.Email, currentUser.Id);
                throw;
            }
            catch (Common.GFEntityNotFoundException)
            {
                methodLogger.Error<string, Guid>("The user {UserEmail} (Id: {UserId}) cannot be found in the database.", this.Email, this.Id);
                throw;
            }
            catch (Exception ex)
            {
                methodLogger.Error<string>(ex, "An error occurred trying to save user {UserEmail} to the database.", this.Email);
                throw;
            }
            finally
            {
                //log the duration
                methodLogger.LogDuration(stopwatch);
            }
        }

        /// <summary>
        /// Converts this <see cref="GFUserEntity"/> object to a <see cref="GFUser"/> object
        /// </summary>
        /// <returns><see cref="GFUser"/> representing this user</returns>
        internal GFUser ToGFUser()
        {
            return new GFUser()
            {
                Key = this.Key,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Username = this.Username, 
                Email = this.Email,
                Role = this.Role
            };
        }
    }
}