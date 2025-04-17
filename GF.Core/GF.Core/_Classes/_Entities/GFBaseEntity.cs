using GF.Common;
using GF.Core.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.Core
{
    internal abstract class GFBaseEntity
    {
        private readonly Serilog.ILogger _logger; //stores the logger

        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user's api key
        /// </summary>
        [BsonElement("key")]
        public string Key { get; set; }

        /// <summary>
        /// Gets the [user] id of the entity owner.
        /// </summary>
        [BsonElement("ownerId")]
        public Guid OwnerId { get; private set; }

        /// <summary>
        /// Gets or sets the id of the user who created the entity
        /// </summary>
        [BsonElement("createdById")]
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the date the entity was created (UTC)
        /// </summary>
        [BsonElement("createdOnUtc")]
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the id of the user who last modified the entity
        /// </summary>
        [BsonElement("modifiedById")]
        public Guid ModifiedById { get; set; }

        /// <summary>
        /// Gets or sets the date the entity was last modified (UTC)
        /// </summary>
        [BsonElement("modifedOnUtc")]
        public DateTime ModifiedOnUtc { get; set; }

        /// <summary>
        /// Gets the <see cref="Serilog.ILogger"/> associated with this entity
        /// </summary>
        internal Serilog.ILogger Logger { get { return _logger; } }

        internal GFBaseEntity (Guid id, Serilog.ILogger logger, [CallerMemberName] string? callingMethod = null)
        {
            this._logger = logger ?? throw new ArgumentNullException(GFException.BuildArgumentName(nameof(logger), callingMethod), "You must supply a logger object");

            Id = id;
            Key = default!;
            OwnerId = default;
            CreatedById = default;
            ModifiedById = default;
            CreatedOnUtc = default; 
            ModifiedOnUtc = default;
        }

        /// <summary>
        /// Saves the supplied entity
        /// </summary>
        /// <typeparam name="TGFEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="currentUser">The <see cref="GFUserEntity"/> saving the entity. Must have the necessary read rights on the entity</param>
        /// <param name="database">The MongoDb database</param>
        /// <param name="session">The MongoDb session</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="enity"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="currentUser"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="database"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is null</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="logger"/> is null</exception>
        /// <exception cref="GFAuthorisationException">If the <paramref name="currentUser"/> doesn't have the appropriate permissions to perform the save</exception>
        /// <exception cref="GFEntityNotFoundException">If the entity is supplied with an Id but that Id doesn't exist in the collection</exception>
        internal virtual async Task SaveToDbAsync<TGFEntity>(TGFEntity entity, GFUserEntity currentUser, IMongoDatabase database, IClientSessionHandle session, Serilog.ILogger logger, CancellationToken cancellationToken = default, [CallerMemberName] string? callingMethod = null) where TGFEntity : IGFEntity<TGFEntity>
        {
            await Helpers.MongoDb.SaveToDbAsync(entity, currentUser, database, session, logger, cancellationToken);
        }

    }
}
