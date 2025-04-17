using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    public class AutoButton : PanelItemBase, ISimSigPanelItem
    {
        /// <summary>
        /// Gets the orientation of the auto button
        /// </summary>
        public SimSigOrientation Orientation { get; private set; }

        /// <summary>
        /// Gets or sets the Is Enabled flag
        /// </summary>
        public bool IsEnabled { get; set; }

        public AutoButton(string simSigID, ushort xOffSet, ushort yOffSet, SimSigOrientation orientation, bool isEnabled) : base (simSigID, xOffSet, yOffSet)
        {
            this.Orientation = orientation;
            this.IsEnabled = isEnabled;
            this.BuildConfig();
        }

        /// <summary>
        /// Builds the AutoButton configuration
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown if the <see cref="Orientation"/> property has not been implemented</exception>
        private void BuildConfig()
        {
            base.ClearConfig();
            switch (this.Orientation)
            {
                case SimSigOrientation.East:
                    this.BuildEast();
                    break;
                case SimSigOrientation.West:
                    this.BuildEast();
                    break;
                default:
                    throw new NotImplementedException($"The orientation {Enum.GetName(typeof(SimSigOrientation), this.Orientation)} has not been implented in {nameof(AutoButton.BuildConfig)}.");
            }
        }

        /// <summary>
        /// Builds the east facing auto button
        /// </summary>
        private void BuildEast()
        {
            base.AddConfig(new SimSigFontTokenConfig($"{this.GetButtonToken()}{SimSigFont.ConvertTextToHex("A")}", SimSigColour.Blue, 0, 0, false));
        }

        /// <summary>
        /// Builds the west facing auto button
        /// </summary>
        private void BuildWest()
        {
            base.AddConfig(new SimSigFontTokenConfig($"{SimSigFont.ConvertTextToHex("A")}{this.GetButtonToken()}", SimSigColour.Blue, 0, 0, false));
        }

        /// <summary>
        /// Builds the button token
        /// </summary>
        /// <returns>A <see cref="string"/> containing the key token for the button</returns>
        private string GetButtonToken()
        {
            return this.IsEnabled ? "0x71" : "0x70";
        }
    }
}
