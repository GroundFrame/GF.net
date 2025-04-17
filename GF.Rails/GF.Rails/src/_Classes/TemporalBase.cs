using System;
using System.Collections.Generic;
using System.Linq;

namespace GF.Rails
{
    public abstract class TemporalBase<TId, THistory> where THistory : ITemporalHistory<TId>
    {
        #region Fields

        private List<THistory> _history; //stores the place history

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the place Id
        /// </summary>
        public TId Id { get; }

        /// <summary>
        /// Gets the place name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the place key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the place history
        /// </summary>
        public IEnumerable<THistory> History { get { return this._history; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="TemporalBase"/>
        /// </summary>
        /// <param name="id">The parent object id</param>
        /// <param name="name">The place name</param>
        /// <param name="Key">The place key</param>
        /// <param name="startHistoryItem">The start hsitory item</param>
        public TemporalBase(TId id, string name, string Key)
        {
            this.Id = id;
            this.Name = name;
            this.Key = Key;
            this._history = new List<THistory>();
        }

        #endregion Constructors

        /// <summary>
        /// Adds a new history item to this place.
        /// </summary>
        /// <param name="history">The history item to add</param>
        public void AddHistory(THistory history)
        {
            this._history.Add(history);
            this.FixHistoryChronology();
        }

        /// <summary>
        /// Removes the history item that covered by the start and end date.
        /// </summary>
        /// <param name="startDate">The start date of the history item to remove</param>
        /// <param name="endDate">The end date of the history item to remove</param>
        public void RemoveHistory(DateTime startDate, DateTime? endDate)
        {
            int index = this._history.FindIndex(h => h.Id.TemporalPeriod.StartDate.Equals(startDate) && h.Id.TemporalPeriod.StartDate.Equals(endDate));
            if (index != -1)
            {
                this._history.RemoveAt(index);
                this.FixHistoryChronology();
            }
        }

        /// <summary>
        /// Removes the history items the fall on the supplied <see cref="DateTime"/>
        /// </summary>
        /// <param name="date">The date of history items to remove</param>
        public void RemoveHistory(DateTime date)
        {
            if (this._history.Count == 1)
            {
                throw new Exception("You cannot remove the last history item assiociated with this object");
            }

            this._history.RemoveAll(h => h.Id.TemporalPeriod.ContainsDate(date));
            this.FixHistoryChronology();
        }

        /// <summary>
        /// Replaces the history with a new history.
        /// </summary>
        /// <param name="newHistory"></param>
        public void ReplaceHistory(IEnumerable<THistory> newHistory)
        {
            this._history = (List<THistory>)newHistory;
        }
        
        /// <summary>
        /// Fixes the chronoology by insuring it's contiguous.
        /// </summary>
        private void FixHistoryChronology() //where TTemporalId : ITemporalId<TId> 
        {
            List<THistory> fixedHistory = new List<THistory>();

            foreach (var placeHistory in this._history.OrderBy(h => h.Id.TemporalPeriod.StartDate))
            {
                fixedHistory.Add(placeHistory);
            }

            for (int h = 0; h < fixedHistory.Count; h++)
            {
                DateTime? endDate = h < (fixedHistory.Count - 1) ? (DateTime?)fixedHistory[h + 1].Id.TemporalPeriod.StartDate.AddDays(-1) : null;
                //var newId = ITemporalId<TId>.Build(fixedHistory[h].Id.Id, fixedHistory[h].Id.TemporalPeriod.StartDate, endDate);
                fixedHistory[h].UpdateId(TemporalId<TId>.Build(fixedHistory[h].Id.Id, fixedHistory[h].Id.TemporalPeriod.StartDate, endDate));
            }

            this.ReplaceHistory(fixedHistory);
        }

        /// <summary>
        /// Gets the start date of this temporal object. Returns null if no history found
        /// </summary>
        /// <returns><see cref="DateTime"/></returns>
        public DateTime? GetStartDate()
        {
            if (!this._history.Any())
            {
                return null;
            }

            return this._history.Min(h => h.Id.TemporalPeriod.StartDate);
        }

        /// <summary>
        /// Gets the end date of this temporal object. If null is return the temporal object is still active
        /// </summary>
        /// <returns><see cref="DateTime?"/></returns>
        public DateTime? GetEndDate()
        {
            if (this._history.Any(h => h.Id.TemporalPeriod.EndDate is null))
            {
                return null;
            }

            return this._history.Max(h => h.Id.TemporalPeriod.EndDate);
        }

        /// <summary>
        /// Gets the history item for the specified date
        /// </summary>
        /// <param name="date">The date of the history item to return</param>
        /// <returns><see cref="THistory"/> the history item</returns>
        public THistory GetHistory(DateTime date)
        {
            return this._history.FirstOrDefault(h => h.Id.TemporalPeriod.ContainsDate(date));
        }
    }
}
