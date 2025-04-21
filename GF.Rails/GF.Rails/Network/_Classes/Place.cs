using GF.Common;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    /// <summary>
    /// Class representing a phyical place
    /// </summary>
    public class Place : TemporalBase<PlaceId, PlaceHistory>
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
        public Place (string name, string key) : base (new PlaceId(Guid.NewGuid()), name, key) {}

        /// <summary>
        /// Instantiates a new <see cref="Place"/> with an initial history item
        /// </summary>
        /// <param name="name">The place name</param>
        /// <param name="key">The place key</param>
        /// <param name="startDate">The start date</param>
        /// <param name="type">The place type as at the <paramref name="startDate"/></param>
        /// <param name="businessUnit">The region as at the <paramref name="startDate"/></param>
        public Place (string name, string key, DateTime startDate, PlaceType type, IBusinessUnit businessUnit) : this(name, key)
        {
            base.AddHistory(new PlaceHistory(this.Id, startDate, null, name, type, businessUnit));
        }

        #endregion Constructors

        /// <summary>
        /// Adds a new history item to this place.
        /// </summary>
        /// <param name="startDate">The start date of the history item</param>
        /// <param name="name">The place name</param>
        /// <param name="type">The place type</param>
        /// <param name="businessUnit">The place region</param>
        public void AddHistory(DateTime startDate, string name, PlaceType type, IBusinessUnit businessUnit)
        {
            base.AddHistory(new PlaceHistory(this.Id, startDate, null, name, type, businessUnit));
        }
    }
}
