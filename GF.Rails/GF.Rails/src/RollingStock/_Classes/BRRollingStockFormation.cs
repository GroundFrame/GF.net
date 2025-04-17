using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.RollingStock
{
    public class BRRollingStockFormation
    {
        #region Fields

        private readonly HashSet<RollingStockId> _specificRollingStockPool = new HashSet<RollingStockId>();

        #endregion Fields

        /// <summary>
        /// Gets the Id
        /// </summary>
        public TemporalId<BRRollingStockFormationId> Id { get; }

        /// <summary>
        /// Gets the tops code Id
        /// </summary>
        public RollingStockCategoryId TOPSCodeId { get; set; }

        /// <summary>
        /// Gets the region Id
        /// </summary>
        public BusinessUnitId BusinessUnitId { get; set; }

        /// <summary>
        /// Get the number of rolling stock items. If a multiple unit then 1 unit equals a quantity of 1.
        /// </summary>
        public byte Quanity { get; set; }

        /// <summary>
        /// Gets the list of the pool of specific rolling stock items from which this formation must be sourced.
        /// </summary>
        public IEnumerable<RollingStockId> SpecificRollingStockPool { get { return this._specificRollingStockPool; } }

        public BRRollingStockFormation(TOPSRollingStockCode topsCode, IBusinessUnit businessUnit, byte quantity)
        {
            this.TOPSCodeId = topsCode.Id.Id;
            this.BusinessUnitId = businessUnit.Id;
            this.Quanity = quantity;
        }

        public void AddSpecificRollingStockToPool (RollingStockId rollingStockId)
        {
            this._specificRollingStockPool.Add(rollingStockId);
        }
    }
}
