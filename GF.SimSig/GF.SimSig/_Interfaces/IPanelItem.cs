using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    public interface IPanelItem
    {
        public IEnumerable<string> SimSigIDs { get; }

        public IEnumerable<SimSigFontTokenConfig> Config { get; }

        /// <summary>
        /// Gets the horizontal offset of where the returned token should be placed on the screen. (0 is the left-hand side of the screen)
        /// </summary>
        public int XOffSet { get; }

        /// <summary>
        /// Gets the vertical offset of where the returned token should be placed on the screen. (0 is the top of the screen)
        /// </summary>
        public int YOffSet { get; }

        public string GenerateSVG(SimSigFont font, float pixelSize = 1);
    }
}
