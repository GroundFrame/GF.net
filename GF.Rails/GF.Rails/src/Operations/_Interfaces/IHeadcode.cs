using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Operations
{
    public interface IHeadcode
    {
        /// <summary>
        /// Gets the train priority. The lower the number the higher the priority
        /// </summary>
        public byte Priority { get; }

        /// <summary>
        /// Converts the headcode to a <see cref="string"/>
        /// </summary>
        /// <returns>The headcode as a string <see cref="string"/></returns>
        public string ToString();
    }
}
