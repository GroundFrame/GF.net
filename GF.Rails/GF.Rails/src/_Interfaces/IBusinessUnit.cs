using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
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
