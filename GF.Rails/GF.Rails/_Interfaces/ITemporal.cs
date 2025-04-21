using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    public interface ITemporal<TId, THistory>
    {
        /// <summary>
        /// Gets the object Id
        /// </summary>
        public TId Id { get; }

        /// <summary>
        /// Gets the object name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the object key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the object history
        /// </summary>
        public IEnumerable<THistory> History { get; }

        /// <summary>
        /// Adds a new history item to this object.
        /// </summary>
        /// <param name="history">The history item to add</param>
        public void AddHistory(THistory history);

        /// <summary>
        /// Removes the history item that covered by the start and end date.
        /// </summary>
        /// <param name="startDate">The start date of the history item to remove</param>
        /// <param name="endDate">The end date of the history item to remove</param>
        public void RemoveHistory(DateTime startDate, DateTime? endDate);

        /// <summary>
        /// Removes the history items the fall on the supplied <see cref="DateTime"/>
        /// </summary>
        /// <param name="date">The date of history items to remove</param>
        public void RemoveHistory(DateTime date);

        /// <summary>
        /// Replaces the history with a new history.
        /// </summary>
        /// <param name="newHistory"></param>
        public void ReplaceHistory(IEnumerable<THistory> newHistory);

        /// <summary>
        /// Gets the start date of this temporal object. Returns null if no history found
        /// </summary>
        /// <returns><see cref="DateTime"/></returns>
        public DateTime? GetStartDate();

        /// <summary>
        /// Gets the end date of this temporal object. If null is return the temporal object is still active
        /// </summary>
        /// <returns><see cref="DateTime?"/></returns>
        public DateTime? GetEndDate();

        /// <summary>
        /// Gets the history item for the specified date
        /// </summary>
        /// <param name="date">The date of the history item to return</param>
        /// <returns><see cref="THistory"/> the history item</returns>
        public THistory GetHistory(DateTime date);

        /// <summary>
        /// Returns a flag to indicate that at least one history item overlaps the supppled period
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool HistoryOverlapPeriod(DateTime startDate, DateTime? endDate);
    }
}
