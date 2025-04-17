using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    [Flags]
    public enum SimSigSignalHeadOption
    {
        None = 0,
        SingleMainAspect = 1,
        DoubleMainAspect = 2,
        Flashing = 4,
        Red = 8,
        Yellow = 16,
        Green = 32,
        ShuntAspect = 64,
        ShuntOff = 128,
        IsReversed = 256,
        IsProved = 512,
        SingleMainWithCallOn = 1024,
        DoubleMainWithCallOn = 2048,
        IsAutoSignal = 4096,
        HasRouteSet = 8192,
        Semaphore = 16384,
        DistantSemaphore = 32768
    }
}
