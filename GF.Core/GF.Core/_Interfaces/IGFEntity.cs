using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.Core
{
    internal interface IGFEntity<T>
    {
        /// <summary>
        /// Gets the Id of the entity
        /// </summary>
        internal Guid Id { get; set; }

        /// <summary>
        /// Gets the key of the entity
        /// </summary>
        internal string Key { get; set; }

        /// <summary>
        /// Gets the Id of the user who owns the entity
        /// </summary>
        internal Guid OwnerId { get; }

        /// <summary>
        /// Gets the Id of the user who created the entity
        /// </summary>
        internal Guid CreatedById { get; set; }

        /// <summary>
        /// Gets the Utc date the entity was created
        /// </summary>
        internal DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets the Id of the uiser who last modified the entity
        /// </summary>
        internal Guid ModifiedById { get; set; }

        /// <summary>
        /// Gets the Utc date the entity was last modified
        /// </summary>
        internal DateTime ModifiedOnUtc { get; set; }

        /// <summary>
        /// Saves the entity to the MongoDb
        /// </summary>
        /// <param name="currentUser">The <see cref="GFUserEntity"/> making the save</param>
        /// <param name="database">The MongoDb database</param>
        /// <param name="session">The MongoDb session</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        internal Task SaveToDbAsync(GFUserEntity currentUser, MongoDatabaseBase database, IClientSessionHandle session, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null);

        internal static abstract Task<T> InitialiseAsync(Guid id, GFUserEntity currentUser, MongoDatabaseBase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null);
    }
}
