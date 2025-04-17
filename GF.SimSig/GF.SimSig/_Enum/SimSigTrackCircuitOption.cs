using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    /// <summary>
    /// Enum / Flags containing the various track circuit options
    /// </summary>
    [Flags]
    public enum SimSigTrackCircuitOption
    {
        /// <summary>
        /// Indicates whether the track circuit has no breaks
        /// </summary>
        NoBreak = 1,
        /// <summary>
        /// Indicates whether the track circuit has a break at the start of the circuit. Only visible if track circuit breaks are displayed
        /// </summary>
        BreakAtStart = 2,
        /// <summary>
        /// Indicates whether the track circuit has a break at the end of the circuit. Only visible if track circuit breaks are displayed
        /// </summary>
        BreakAtEnd = 4,
        /// <summary>
        /// Indicates the track circuit has is an overlap
        /// </summary>
        IsOverlap = 8
    }
}
