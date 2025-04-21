using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    [Flags]
    public enum SignallingType
    {
        None = 0,
        MainAspect = 1,
        CallOn = 2,
        ShuntDolly = 4,
        HandSignal = 8,
    }
}
