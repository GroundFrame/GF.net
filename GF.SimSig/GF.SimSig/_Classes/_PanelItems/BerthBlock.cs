using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    public class BerthBlock : PanelItemBase, ISimSigPanelItem
    {
        BerthConfig _berthConfig = default;

        public BerthBlock(string simSigID, ushort xOffSet, ushort yOffSet, SimSigColour colour = SimSigColour.Grey) : base (simSigID, xOffSet, yOffSet)
        {
            this.Colour = colour;
            this._berthConfig = new BerthConfig(0, 0);
            this.BuildConfig();
        }

        public void SetBerthText(string text)
        {
            this._berthConfig.SetText(text);
        }

        public void ClearBerthText()
        {
            this._berthConfig.ClearText();
        }

        private void BuildConfig()
        {
            base.ClearConfig();
            base.AddConfig(new SimSigFontTokenConfig($"{String.Concat(Enumerable.Repeat("0x5C", 4))}", this.Colour, 0, 0));

            if (!string.IsNullOrEmpty(this._berthConfig.Text))
            {
                base.AddConfig(new SimSigFontTokenConfig(SimSigFont.ConvertTextToHex(this._berthConfig.Text!), SimSigColour.Cyan, this._berthConfig.XOffSet, this._berthConfig.YOffSet));
            }
        }
    }
}
