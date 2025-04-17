using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    /// <summary>
    /// Interface for SimSig fonts.
    /// </summary>
    public interface ISimSigFont
    {
        #region Properties

        /// <summary>
        /// Gets the width of the font in pixels
        /// </summary>
        public int FontWidthPixels { get; }

        /// <summary>
        /// Gets the height of the font in pixels
        /// </summary>
        public int FontHeightPixels { get; }

        #endregion

        /// <summary>
        /// Builds an SVG from the font for the supplied config and style.
        /// </summary>
        /// <param name="configs">A collection of <see cref="SimSigFontTokenConfig"/> containing the tokens for each SVG to be returned</param>
        /// <param name="styleClass">A <see cref="SimSigStyle"/> object which contains the style / classes to be applied to each SVG element</param>
        /// <param name="xOffSet">The grid horizontal offset (left to right)</param>
        /// <param name="yOffSet">The grid vertical offset (top to bottom)</param>
        /// <returns>A <see cref="string"/> containing the SVG definition</returns>
        public string BuildSVG(IEnumerable<SimSigFontTokenConfig> configs, SimSigStyle styleClass, List<string> usedFontKeys, int xOffSet = 0, int yOffSet = 0);
    }
}
