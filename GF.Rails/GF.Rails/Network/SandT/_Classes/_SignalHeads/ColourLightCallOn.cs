using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public class ColourLightCallOn : ColourLightBase, ISignalHead
    {
        public ColourLightCallOn(ColourLightBulbType bulbType, SignalStateOptions possibleSignalStateOptions, SignalStateOptions offSignalState, SignalStateOptions defaultState) : base(bulbType, possibleSignalStateOptions, offSignalState, defaultState)
        {
            this.SetDefaultState(SignalState.BuildSignalState(this, defaultState));
        }
    }
}

