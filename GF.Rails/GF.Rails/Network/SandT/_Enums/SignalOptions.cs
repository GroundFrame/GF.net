using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    [Flags]
    public enum SignalOptions
    {
        MAS = 1,
        Semaphore = 2,
        Radio = 4,
        ECTMS = 8
    }
}
