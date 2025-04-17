using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    [Flags]
    public enum Power
    {
        Diesel = 1,
        Overhead25Kv = 2,
        ThirdRail650v = 4,
        ThirdRail750v = 8,
        FourthRail = 16,
        Steam = 32
    }
}
