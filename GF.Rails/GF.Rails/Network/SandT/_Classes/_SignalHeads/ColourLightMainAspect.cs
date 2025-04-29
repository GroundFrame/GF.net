using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public class ColourLightMainAspect : ColourLightBase, ISignalHead
    {
        /// <summary>
        /// Gets the signal head Id
        /// </summary>


        /// <summary>
        /// Gets a flag to indicate whether this head has an emergency replacement button
        /// </summary>
        public bool HasEmergencyReplacement { get; private set; }


        #region Constructors

        public ColourLightMainAspect(ColourLightBulbType bulbType, SignalStateOptions possibleSignalStateOptions, SignalStateOptions offSignalState, SignalStateOptions defaultState, bool hasEmergencyReplacement = false) : base(bulbType, possibleSignalStateOptions, offSignalState, defaultState)
        {
            this.HasEmergencyReplacement = hasEmergencyReplacement;
            this.SetDefaultState(SignalState.BuildSignalState(this, defaultState));
        }

        #endregion Constructors
    }
}
