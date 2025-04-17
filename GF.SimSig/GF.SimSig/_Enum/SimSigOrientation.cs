using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    [Flags]
    public enum SimSigOrientation
    {
        North = 1,
        NorthEast = 3,
        West = 2,
        SouthEast = 6,
        South = 4,
        SouthWest = 12,
        East = 8,
        NorthWest = 9
    }
}
