using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Common
{
    /// <summary>
    /// Interface represening a graph cost
    /// </summary>
    /// <typeparam name="TCost">The type of the cost</typeparam>
    public interface IGraphCost<TCost>
    {
        /// <summary>
        /// Returns the maximum possible length.
        /// </summary>
        public static TCost MaxValue { get; }

        /// <summary>
        /// Returns the minumum possible length.
        /// </summary>
        public static TCost MinValue { get; }

        /// <summary>
        /// Adds a <see cref="TCost"/> to this cost and returns a new <see cref="TCost"/>
        /// </summary>
        /// <param name="other">The other <see cref="TCost"/> to add to this <see cref="TCost"/></param>
        /// <returns>A <see cref="TCost"/> containing the result of adding this <see cref="TCost"/> to <paramref name="other"/></returns>
        public TCost Add(TCost other);
    }
}
