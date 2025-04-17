using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    public abstract class TemporalHistoryBase<TId>
    {
        private readonly TId _id;

        public TemporalId<TId> Id { get; private set; }

        public string Name { get; set; }

        public TemporalHistoryBase(TId id, DateTime startDate, DateTime? endDate)
        {
            this._id = id;
            this.Id = new TemporalId<TId>(id, startDate, endDate);
        }

        /// <summary>
        /// Updates the Id
        /// </summary>
        /// <param name="newId">The new <see cref="TemporalId{TId}"/></param>
        public void UpdateId(TemporalId<TId> newId)
        {
            if (!newId.Id.Equals(this._id))
            {
                throw new ArgumentException($"The new Id must have the same {nameof(TId)} as the existing Id.");
            }

            this.Id = newId;
        }
    }
}
