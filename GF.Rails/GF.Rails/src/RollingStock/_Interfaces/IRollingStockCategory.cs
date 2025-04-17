using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.RollingStock
{
    public interface IRollingStockCategory<TTemporalId>
    {
        public TTemporalId Id { get; }

        /// <summary>
        /// Gets or sets the category code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets hte category description
        /// </summary>
        public string Description { get; set; }
    }
}
