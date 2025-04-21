using GF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Operations
{
    /// <summary>
    /// Class representing a BR Region
    /// </summary>
    public class BRRegion : TemporalBase<BusinessUnitId, BRRegionHistory>, IBusinessUnit, ITemporal<BusinessUnitId, BRRegionHistory>
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
        public BRRegion(string name, string key) : base (new BusinessUnitId(Guid.NewGuid()), name, key){}

        #endregion Constructors

        /// <summary>
        /// Adds a new history item to this place.
        /// </summary>
        /// <param name="startDate">The start date of the history item</param>
        /// <param name="name">The place name</param>
        /// <param name="manager">The manager</param>
        public void AddHistory(DateTime startDate, string name, IPerson manager)
        {
            base.AddHistory(new BRRegionHistory(this.Id, startDate, null, name, manager));
        }
    }
}
