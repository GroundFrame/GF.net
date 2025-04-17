using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    public class Platform : PanelItemBase, ISimSigPanelItem
    {
        /// <summary>
        /// Gets the length of the platform in screen units
        /// </summary>
        public int Length { get; private set; }

        public Platform(string simSigID, ushort xOffSet, ushort yOffSet, int length) : base (simSigID, xOffSet, yOffSet)
        {
            this.Length = length;
            base.AddConfig(new SimSigFontTokenConfig($"{String.Concat(Enumerable.Repeat("0x8E", length))}",SimSigColour.Orange,0,0));
        }
    }
}
