using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Network.SandT
{
    [Flags]
    public enum SignallingType
    {
        ColourLight = 1,
        Semaphore = 2,
        Radio = 4,
        ECTMS = 8
    }
}
