using GF.Common;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

[assembly: InternalsVisibleTo("GF.SimSig.IntegrationTests")]
namespace GF.Core
{
    internal partial class Helpers
    {
        internal static class Keys
        {
            internal static readonly char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

            /// <summary>
            /// Builds a unique key of the specified size
            /// </summary>
            /// <param name="size">The size of the key</param>
            /// <returns>A random string of the specified size</returns>
            /// <remarks>Method lifted from <see cref="https://stackoverflow.com/a/1344255"/>. Credit to Eric J (<see cref="https://stackoverflow.com/users/141172/eric-j"/>)</remarks>
            private static string Generate(int size, [CallerMemberName] string? callingMethod = null)
            {
                if (size < 1)
                {
                    throw new ArgumentException("The size cannot be less than 1", GFException.BuildArgumentName(nameof(size), callingMethod) );
                }

                byte[] data = new byte[4 * size];
                using (var crypto = RandomNumberGenerator.Create())
                {
                    crypto.GetBytes(data);
                }
                StringBuilder result = new(size);
                for (int i = 0; i < size; i++)
                {
                    var rnd = BitConverter.ToUInt32(data, i * 4);
                    var idx = rnd % chars.Length;

                    result.Append(chars[idx]);
                }

                return result.ToString();
            }

            /// <summary>
            /// Generates a new key for the supplied entity type
            /// </summary>
            /// <typeparam name="TGFEntity">The entity to generate the new key for</typeparam>
            /// <param name="size">The size of the key. Must be at least 8 characters</param>
            /// <param name="currentUser">The current <see cref="GFUserEntity"/>. Must have read rights on the entity</param>
            /// <param name="database">The MongoDb database</param>
            /// <param name="session">The current MongoDb session</param>
            /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
            /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
            /// <param name="callingMethod">The calling method name. Do not pass</param>
            /// <returns></returns>
            /// <exception cref="ArgumentNullException"></exception>
            internal static async Task<string> GenerateAsync<TGFEntity>(GFUserEntity currentUser, IMongoDatabase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null) where TGFEntity : IGFEntity<TGFEntity>
            {
                if (currentUser == null)
                {
                    throw new ArgumentNullException(GFException.BuildArgumentName(nameof(currentUser), callingMethod), "You must supply the current user who is trying to geneate a new key");
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
                    bool isUnique = false; //flag to keep track of the uniqueness of the key within the entity

                    //check the user has permission to create this entity
                    if (!MongoDb.CanReadEntity<TGFEntity>(currentUser))
                    {
                        throw GFAuthorisationException.Build<TGFEntity>(currentUser);
                    }

                    //generate the initial key
                    string key = Generate(8);

                    while (!isUnique)
                    {
                        //check to see whether the key already exists in the entity
                        var collection = database.GetCollection<TGFEntity>(MongoDb.GetMongoDbCollectionName<TGFEntity>());
                        var filter = Builders<TGFEntity>.Filter.Eq("key", key);

                        if (await collection.CountDocumentsAsync(session, filter, new CountOptions(), cancellationToken) == 0)
                        {
                            isUnique = true;
                        }
                        else
                        {
                            //generate a new key
                            key = Generate(8);
                        }
                    }

                    return key;
                }
                catch (Common.GFAuthorisationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error<string>(ex, "An error has occurred trying to generate a key for entity type {EntityType}", typeof(TGFEntity).Name);
                    throw;
                }
            }
        }
    }
}