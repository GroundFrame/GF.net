using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public enum SignalStateOptions
    {
        Red = 1,
        SingleYellow = 2,
        DoubleYellow = 4,
        Green = 8,
        IsFlashing = 16,
        RedWhite = 32,
        DoubleRed = 64,
        DoubleWhite = 128,

    }
}
