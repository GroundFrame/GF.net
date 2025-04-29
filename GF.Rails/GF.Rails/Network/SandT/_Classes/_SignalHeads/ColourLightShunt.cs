using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public class ColourLightShunt : ColourLightBase, ISignalHead
    {
        public ColourLightShunt(ColourLightBulbType bulbType, SignalStateOptions possibleSignalStateOptions, SignalStateOptions offSignalState, SignalStateOptions defaultState) : base(bulbType, possibleSignalStateOptions, offSignalState, defaultState)
        {
            this.SetDefaultState(SignalState.BuildSignalState(this, defaultState));
        }
    }
}
