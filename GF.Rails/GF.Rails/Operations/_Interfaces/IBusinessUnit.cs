using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Operations
{
    /// <summary>
    /// Interface representing a business unit
    /// </summary>
    public interface IBusinessUnit
    {
        /// <summary>
        /// Gets the business unit Id
        /// </summary>
        public BusinessUnitId Id { get; }

        /// <summary>
        /// Gets the business unit name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the business unit key
        /// </summary>
        public string Key { get; }
    }
}
