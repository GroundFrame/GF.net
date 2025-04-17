using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    /// <summary>
    /// A record respenting the configuration of a SimSig font token
    /// </summary>
    public record SimSigFontTokenConfig
    {
        /// <summary>
        /// Gets or sets the key of the token. This is a string containing the hex representation (for example 0x4A) for the characters that will be return by the font
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the colour of the returned token
        /// </summary>
        public SimSigColour Colour { get; set; }

        /// <summary>
        /// Gets the horizontal offset of where the returned token should be placed on the screen. (0 is the left-hand side of the screen)
        /// </summary>
        public int XOffSet { get; set; }

        /// <summary>
        /// Gets the vertical offset of where the returned token should be placed on the screen. (0 is the top of the screen)
        /// </summary>
        public int YOffSet { get; set; }

        /// <summary>
        /// Gets the flag which indicates whether the item is flashing
        /// </summary>
        public bool IsFlashing { get; set; }

        /// <summary>
        /// Constructs a <see cref="SimSigFontTokenConfig"/>
        /// </summary>
        /// <param name="key">The key of the token the font should return when parsing the config</param>
        /// <param name="colour">The colour of the returned token</param>
        /// <param name="xOffSet">The horizontal offset (left to right)</param>
        /// <param name="yOffSet">The vertical offset (top to bottom)</param>
        /// <param name="isFlashing">Indicates whether the returned token should flash</param>
        public SimSigFontTokenConfig(string key, SimSigColour colour, int xOffSet, int yOffSet, bool isFlashing = false)
        {
            this.Key = key;
            this.Colour = colour;
            this.XOffSet = xOffSet;
            this.YOffSet = yOffSet;
            this.IsFlashing = isFlashing;
        }
    }
}
