using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Network
{
    public class PlaceHistory : ITemporalHistory<PlaceId>
    {
        #region Fields

        private readonly PlaceId _placeId;

        #endregion Fields

        /// <summary>
        /// Gets the place history Id;
        /// </summary>
        public TemporalId<PlaceId> Id { get; private set; }

        /// <summary>
        /// Gets the place name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type
        /// </summary>
        public PlaceType Type { get; }

        /// <summary>
        /// Gets the business unit that had responsibility at this time.
        /// </summary>
        public IBusinessUnit BusinessUnit { get; }

        public PlaceHistory (PlaceId placeId, DateTime startDate, DateTime? endDate, string name, PlaceType type, IBusinessUnit region)
        {
            this._placeId = placeId;
            this.Id = new TemporalId<PlaceId>(placeId, startDate, endDate);
            this.Name = name;
            this.Type = type;
            this.BusinessUnit = region;
        }

        /// <summary>
        /// Updates the Id
        /// </summary>
        /// <param name="newId">The new <see cref="ITemporalId<PlaceId>"/></param>
        public void UpdateId(TemporalId<PlaceId> newId)
        {
            if (newId.Id != this._placeId)
            {
                throw new ArgumentException($"The new Id must have the same {nameof(PlaceId)} as the existing Id.");
            }

            this.Id = newId;
        }
    }
}
