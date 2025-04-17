using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    public class BRRegionHistory : TemporalHistoryBase<BusinessUnitId>, ITemporalHistory<BusinessUnitId>
    {
        #region Fields

        private PersonId _managerId;

        #endregion Fields

        /// <summary>
        /// Gets the manager
        /// </summary>
        public IPerson Manager { get; set; }

        public BRRegionHistory(BusinessUnitId regionId, DateTime startDate, DateTime? endDate, string name, IPerson manager) : base (regionId, startDate, endDate)
        {
            this.Name = name;
            this.Manager = manager;
            this._managerId = manager.Id;
        }
    }
}
