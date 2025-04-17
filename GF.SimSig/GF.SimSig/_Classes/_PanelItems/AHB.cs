using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    /// <summary>
    /// Class representing an automatic half barrier
    /// </summary>
    public class AHB : PanelItemBase, ISimSigPanelItem
    {
        /// <summary>
        /// Gets the configuration of the automatic half barrier
        /// </summary>
        [JsonProperty("ahbConfig")]
        public SimSigAHB AHBConfig { get; private set; }

        /// <summary>
        /// Constructs a <see cref="AHB"/>.
        /// </summary>
        /// <param name="simSigID">The SimSig Id for the panel item. For example the track circuit ID or Signal number</param>
        /// <param name="xOffSet">The horizontal offset of where the item should be placed in the panel</param>
        /// <param name="yOffSet">The vertical offset of where the item should be planed in the panel</param>
        /// <param name="ahbConfig">The initial configuration of the automatic half barrier</param>
        public AHB(string simSigID, ushort xOffSet, ushort yOffSet, SimSigAHB ahbConfig) : base (simSigID, xOffSet, yOffSet)
        {
            this.AHBConfig = ahbConfig;
            this.BuildConfig();
        }

        /// <summary>
        /// Builds the automatic half barrier configuration
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown if the <see cref="AHBConfig"/> property is not implemented in the method.</exception>
        private void BuildConfig()
        {
            base.ClearConfig();
            //add text
            base.AddConfig(new SimSigFontTokenConfig($"{SimSigFont.ConvertTextToHex("RAI WKG FAI")}", SimSigColour.Grey, 0, 0));

            switch (this.AHBConfig)
            {
                case SimSigAHB.Raised:
                    this.AddConfig(new SimSigFontTokenConfig("0x71", SimSigColour.White, 1, 1, false));
                    this.AddConfig(new SimSigFontTokenConfig("0x70", SimSigColour.White, 5, 1, false));
                    this.AddConfig(new SimSigFontTokenConfig("0x70", SimSigColour.White, 9, 1, false));
                    break;
                case SimSigAHB.Working:
                    this.AddConfig(new SimSigFontTokenConfig("0x70", SimSigColour.White, 1, 1, false));
                    this.AddConfig(new SimSigFontTokenConfig("0x71", SimSigColour.White, 5, 1, true));
                    this.AddConfig(new SimSigFontTokenConfig("0x70", SimSigColour.White, 9, 1, false));
                    break;
                case SimSigAHB.Failed:
                    this.AddConfig(new SimSigFontTokenConfig("0x70", SimSigColour.White, 1, 1, false));
                    this.AddConfig(new SimSigFontTokenConfig("0x70", SimSigColour.White, 5, 1, false));
                    this.AddConfig(new SimSigFontTokenConfig("0x71", SimSigColour.Red, 9, 1, true));
                    break;
                case SimSigAHB.Lowered:
                    this.AddConfig(new SimSigFontTokenConfig("0x70", SimSigColour.White, 1, 1, false));
                    this.AddConfig(new SimSigFontTokenConfig("0x70", SimSigColour.White, 5, 1, false));
                    this.AddConfig(new SimSigFontTokenConfig("0x70", SimSigColour.White, 9, 1, true));
                    break;
                default:
                    throw new NotImplementedException($"The {nameof(this.AHBConfig)} property {Enum.GetName(typeof(SimSigAHB), this.AHBConfig)} has not been implemented in the {nameof(BuildConfig)} method.");
            }
        }
    }
}
