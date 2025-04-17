using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    /// <summary>
    /// Struct representing a temporal Id. This is an Id which is bound by a temporal period.
    /// </summary>
    /// <typeparam name="TId">The type of the Id</typeparam>
    public struct TemporalId<TId> : IEquatable<TemporalId<TId>>
    {
        /// <summary>
        /// Gets the Id
        /// </summary>
        public TId Id { get; }

        /// <summary>
        /// Gets or set sets the temporal period
        /// </summary>
        public TemporalPeriod TemporalPeriod { get; set; }

        /// <summary>
        /// Instantiates a new <see cref="TemporalId{TId}"/> with the supplied id, start date and end date
        /// </summary>
        /// <param name="id">The Id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">THe end date. Pass null if the temporal period is still current / open</param>
        public TemporalId(TId id, DateTime startDate, DateTime? endDate)
        {
            this.Id = id;
            this.TemporalPeriod = new TemporalPeriod(startDate, endDate);
        }

        /// <summary>
        /// Determines whether this <see cref="TemporalId{TId}"/> is equal to another <see cref="TemporalId{TId}"/>
        /// </summary>
        /// <param name="other">The other <see cref="TemporalId{TId}"/> to compare</param>
        /// <returns><see cref="bool"/>. Returns true if equal otherwise false.</returns>
        public bool Equals(TemporalId<TId> other)
        {
            return this.EqualsId(other) && this.TemporalPeriod.Equals(other.TemporalPeriod);
        }

        /// <summary>
        /// Determines whether the Id property of this <see cref="TemporalId{TId}"/> is equal to the Id property of another <see cref="TemporalId{TId}"/>
        /// </summary>
        /// <param name="other">The other <see cref="TemporalId{TId}"/> to compare</param>
        /// <returns><see cref="bool"/>. Returns true if equal otherwise false.</returns>
        public readonly bool EqualsId(TemporalId<TId> other)
        {
            return this.Id.Equals(other.Id);
        }

        /// <summary>
        /// Builds and returns a new <see cref="TemporalId{TId}"/>
        /// </summary>
        /// <param name="id">The Id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">THe end date. Pass null if the temporal period is still current / open</param>
        /// <returns><see cref="TemporalId{TId}"/></returns>
        public static TemporalId<TId> Build(TId id, DateTime startDate, DateTime? endDate)
        {
            return new TemporalId<TId>(id, startDate, endDate);
        }
    }
}
