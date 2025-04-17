using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    public class EmergencyReplacement : PanelItemBase, ISimSigPanelItem
    {
        /// <summary>
        /// Gets the orientation of the emergency replacement button
        /// </summary>
        [JsonProperty("orientation")]
        public SimSigOrientation Orientation { get; private set; }

        /// <summary>
        /// Gets the flag which indicates whether the replace button is pressed.
        /// </summary>
        [JsonProperty("isReplaced")]
        public bool IsReplaced { get; private set; }

        public EmergencyReplacement(string simSigID, ushort xOffSet, ushort yOffSet, SimSigOrientation orientation, bool isReplaced) : base (simSigID, xOffSet, yOffSet)
        {
            this.Orientation = orientation; 
            this.IsReplaced = isReplaced;
            this.BuildConfig();
        }

        private void BuildConfig()
        {
            base.ClearConfig();

            switch (this.Orientation)
            {
                case SimSigOrientation.East:
                    base.AddConfig(new SimSigFontTokenConfig($"{GetButtonKey()}0x45", SimSigColour.Red, 0, 0));
                    break;
                case SimSigOrientation.West:
                    base.AddConfig(new SimSigFontTokenConfig($"0x45{GetButtonKey()}", SimSigColour.Red, 0, 0));
                    break;
                default:
                    throw new NotImplementedException($"The orientation {Enum.GetName(typeof(SimSigOrientation), this.Orientation)} has not been implented in {nameof(EmergencyReplacement.BuildConfig)}");
            }
        }

        private string GetButtonKey()
        {
            return (this.IsReplaced) ? "0x71" : "0x70";
        }
    }
}
