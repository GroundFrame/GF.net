using GF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Operations
{
    /// <summary>
    /// Class representing a BR Division
    /// </summary>
    public class BRDivision : TemporalBase<BusinessUnitId, BRDivisionHistory>, IBusinessUnit, ITemporal<BusinessUnitId, BRDivisionHistory>
    {
        #region Fields

        #endregion Fields

        #region Properties

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="BRDivision"/>
        /// </summary>
        /// <param name="name">The place name</param>
        /// <param name="key">The place key</param>
        public BRDivision(string name, string key) : base (new BusinessUnitId(Guid.NewGuid()), name, key){}

        #endregion Constructors

        /// <summary>
        /// Adds a new history item to this place.
        /// </summary>
        /// <param name="startDate">The start date of the history item</param>
        /// <param name="name">The place name</param>
        /// <param name="manager">The manager</param>
        /// <param name="parentRegion">The parent region</param>
        public void AddHistory(DateTime startDate, string name, IPerson manager, BRRegion parentRegion)
        {
            base.AddHistory(new BRDivisionHistory(this.Id, startDate, null, name, manager, parentRegion));
        }
    }
}
