using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Common
{
    public interface ISpatial
    {
        public float X { get; }
        public float Y { get; }

        /// <summary>
        /// Gets the distance to another spatial
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Length DistanceTo(ISpatial target);
    }
}
