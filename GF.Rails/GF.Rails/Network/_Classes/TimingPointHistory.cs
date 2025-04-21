using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Network
{
    public class TimingPointHistory : ITemporalHistory<TimingPointId>
    {
        private readonly TimingPointId _timingPointId;

        public TemporalId<TimingPointId> Id { get; private set; }

        /// <summary>
        /// Gets the timing point type
        /// </summary>
        public TimingPointType Type { get; }

        /// <summary>
        /// Gets the business unit with responsbility for the timing point
        /// </summary>
        public IBusinessUnit BusinessUnit{ get; }

        /// <summary>
        /// Gets the place the timing point relates to.
        /// </summary>
        public Place Place { get; }

        /// <summary>
        /// Instantiates a new <see cref="TimingPointHistory"/>.
        /// </summary>
        /// <param name="timingPointId">The <see cref="TimingPointId"/></param>
        /// <param name="startDate">The start date of the history item</param>
        /// <param name="endDate">The end date of the history item (pass null if still current)</param>
        /// <param name="type">The timing point type during the timing point history</param>
        /// <param name="businessUnit">The busines unit with responsibility for the timing point at the <paramref name="startDate"/></param>
        /// <param name="place">The place the timing point is located at during the timing point history</param> 
        public TimingPointHistory(TimingPointId timingPointId, DateTime startDate, DateTime? endDate, TimingPointType type, IBusinessUnit businessUnit, Place place = null)
        {
            this._timingPointId = timingPointId;
            this.Id = new TemporalId<TimingPointId>(timingPointId, startDate, endDate);
            this.Type = type;
            this.BusinessUnit = businessUnit;
            this.Place = place;
        }

        /// <summary>
        /// Updates the Id
        /// </summary>
        /// <param name="newId">The new <see cref="TemporalId{TimingPointId}"/></param>
        public void UpdateId(TemporalId<TimingPointId> newId)
        {
            if (newId.Id != this._timingPointId)
            {
                throw new ArgumentException($"The new Id must have the same {nameof(TimingPointId)} as the existing Id.");
            }

            this.Id = newId;
        }
    }
}
