using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    public interface ITemporal<TId, THistory>
    {
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
        public IEnumerable<THistory> History { get; }

        /// <summary>
        /// Adds a new history item to this place.
        /// </summary>
        /// <param name="startDate">The start date of the history item</param>
        /// <param name="name">The place name</param>
        /// <param name="type">The place type</param>
        public void AddHistory(DateTime startDate, string name, PlaceType type);
    }
}
