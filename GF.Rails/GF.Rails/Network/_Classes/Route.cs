using GF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    /// <summary>
    /// Class representing a route
    /// </summary>
    public class Route : TemporalBase<RouteId, RouteHistory>
    {
        #region Fields

        #endregion Fields

        #region Properties

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="Place"/>
        /// </summary>
        /// <param name="name">The place name</param>
        /// <param name="key">The place key</param>
        public Route(string name, string key) : base (new RouteId(Guid.NewGuid()), name, key){}

        public Route(string name, string key, DateTime startDate, DirectionType direction, params RouteTimingPoint[] routeTimingPoints) : this (name, key)
        {
            this.AddHistory(startDate, direction, routeTimingPoints);
        }

        #endregion Constructors

        /// <summary>
        /// Adds a new history item to this place.
        /// </summary>
        /// <param name="startDate">The start date of the history item</param>
        /// <param name="name">The place name</param>
        public void AddHistory(DateTime startDate, DirectionType direction, params RouteTimingPoint[] routeTimingPoints)
        {
            base.AddHistory(new RouteHistory(this.Id, startDate, null, direction, routeTimingPoints));
        }
    }
}
