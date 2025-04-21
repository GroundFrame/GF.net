using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Operations
{
    /// <summary>
    /// Class representing a historic <see cref="BRDivision"/>
    /// </summary>
    public class BRDivisionHistory : TemporalHistoryBase<BusinessUnitId>, ITemporalHistory<BusinessUnitId>
    {
        #region Fields

        private PersonId _managerId; //gets the manager Id
        private BusinessUnitId _regionId; //get the region Id

        #endregion Fields

        /// <summary>
        /// Gets the manager
        /// </summary>
        public IPerson Manager { get; set; }

        /// <summary>
        /// Gets the parent region
        /// </summary>
        public BRRegion Region { get; set; }

        public BRDivisionHistory(BusinessUnitId divisionId, DateTime startDate, DateTime? endDate, string name, IPerson manager, BRRegion region) : base (divisionId, startDate, endDate)
        {
            this.Name = name;
            this.Manager = manager;
            this._managerId = manager.Id;
            this.Region = region;
            this._regionId = region.Id;
        }
    }
}
