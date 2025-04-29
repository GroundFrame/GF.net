using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network
{
    public interface ISignalBoxPanelOrWorkstationItem
    {
        /// <summary>
        /// Gets the panel item id
        /// </summary>
        public Guid PanelItemId { get; }

        /// <summary>
        /// Determines whether 2 <see cref="ISignalBoxPanelOrWorkstationItem"/> items match
        /// </summary>
        /// <param name="obj">The other object to compare</param>
        /// <returns>True if the items are equal otherwise false</returns>
        public abstract bool Equals(object obj);

        /// <summary>
        /// Returns the hashcode for this isntance
        /// </summary>
        public abstract int GetHashCode();

    }
}
