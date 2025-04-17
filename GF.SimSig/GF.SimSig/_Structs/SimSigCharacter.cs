using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    /// <summary>
    /// Struct representing a single character of the SimSig font
    /// </summary>
    /// <remarks>The SimSigV2 font is a fixed sized font with each character comprising of a 8x16 grid. This struct represents each of the 16 lines of the character with a byte with each bit indiciating whether the 'pixel' is 'turned on'. The bit values work from left to right. 
    /// Byte1 value 10000001 would indicate the left most and right most pixels of line 1 are enabled. Byte16 value of 00111100 would indicate the central 4 pixels of line 16 are enabled.</remarks>
    public struct SimSigCharacter
    {
        #region Fields

        [JsonProperty]
        private readonly byte[] _bytes; //stores the bytes used to configure the character

        #endregion Fields

        /// <summary>
        /// The character key
        /// </summary>
        [Newtonsoft.Json.JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// The character description
        /// </summary>
        [Newtonsoft.Json.JsonProperty("description")]
        public string Description { get; set; }


        /// <summary>
        /// Instantiates a <see cref="SimSigCharacter"/> using the supplied byte configuration
        /// </summary>
        /// <param name="key">The character key</param>
        /// <param name="description">The character description</param>
        /// <param name="line1">Byte value for line 1 of the character</param>
        /// <param name="line2">Byte value for line 2 of the character</param>
        /// <param name="line3">Byte value for line 3 of the character</param>
        /// <param name="line4">Byte value for line 4 of the character</param>
        /// <param name="line5">Byte value for line 5 of the character</param>
        /// <param name="line6">Byte value for line 6 of the character</param>
        /// <param name="line7">Byte value for line 7 of the character</param>
        /// <param name="line8">Byte value for line 8 of the character</param>
        /// <param name="line9">Byte value for line 9 of the character</param>
        /// <param name="line10">Byte value for line 10 of the character</param>
        /// <param name="line11">Byte value for line 11 of the character</param>
        /// <param name="line12">Byte value for line 12 of the character</param>
        /// <param name="line13">Byte value for line 13 of the character</param>
        /// <param name="line14">Byte value for line 14 of the character</param>
        /// <param name="line15">Byte value for line 15 of the character</param>
        /// <param name="line16">Byte value for line 16 of the character</param>
        public SimSigCharacter(
            string key,
            string description,
            byte line1 = 0,
            byte line2 = 0,
            byte line3 = 0,
            byte line4 = 0,
            byte line5 = 0,
            byte line6 = 0,
            byte line7 = 0,
            byte line8 = 0,
            byte line9 = 0,
            byte line10 = 0,
            byte line11 = 0,
            byte line12 = 0,
            byte line13 = 0,
            byte line14 = 0,
            byte line15 = 0,
            byte line16 = 0
        )
        {
            this.Key =  key;
            this.Description = description;
            this._bytes =
            [
                line1,
                line2,
                line3,
                line4,
                line5,
                line6,
                line7,
                line8,
                line9,
                line10,
                line11,
                line12,
                line13,
                line14,
                line15,
                line16
            ];
        }

        private readonly string RenderLineToSVG(int lineNumber)
        {
            if (this._bytes[lineNumber - 1] == 0)
            {
                return string.Empty;
            }

            BitArray bits = new(new byte[] { this._bytes[lineNumber - 1] });
            string result = string.Empty;
            bool isRendering = false; //indicates whether the SVG is currently drawing a line
            byte relativeColumn = 0; //indicates the relative column relative whilst drawing
            byte lineLength = 1; //keeps track of the length of the line to draw
            bool hasPreviouslyRendered = false; //keeps track as to whether something has previously rendered on the line

            for (int i = 0; i < bits.Length; i++)
            {
                //if the current bit is true and the svg is not rendering a line then start rendering the path
                if (bits[i] && !isRendering)
                {
                    int relativelineNumber = hasPreviouslyRendered ? 0 : lineNumber - 1;

                    result += $@"m{relativeColumn} {relativelineNumber}h";
                    lineLength = 1;
                    isRendering = true;
                    hasPreviouslyRendered = true;

                    //if the bit is the last bit in the byte arry and we've just started rendering then the line length must be 1.
                    if (i == bits.Length - 1)
                    {
                        result += "1";
                    }

                    continue;
                }

                //if the current bit is true the svg is rendering and we're not in the last column increment the relative column
                if (bits[i] && isRendering && i != (bits.Length - 1))
                {
                    lineLength++;
                    continue;
                }

                //if the current bit is false and the SVG is rendering or we're in the last column
                if ((!bits[i] || i == (bits.Length - 1)) && isRendering)
                {
                    //if last column increase length by 1
                    if (i == (bits.Length - 1) && bits[i])
                    {
                        lineLength++;
                    }

                    result += $"{lineLength}";
                    relativeColumn = 1; //reset the relative column
                    isRendering = false;
                    continue;
                }

                //if the current bit false and SVG is not rendering then increment the relative column
                if (!bits[i] && !isRendering)
                {
                    relativeColumn++;
                    continue;
                }
            }

            return $@"<path d=""{result}""/>";
        }

        /// <summary>
        /// Renders the character to an SVG
        /// </summary>
        /// <param name="svgId">The svg id</param>
        /// <param name="xOffset">The number of characters on the x axis the character should be offset (0 would be rendered at the left hand edge of the SVG)</param>
        /// <param name="yOffset">The number of characters on the y axis the character should be offset (0 would be rendered at the top edge of the SVG)</param>
        /// <returns>A <see cref="string"/> containing all the rect clauses for an SVG representing the character</returns>
        public readonly string RenderCharacterToSVGGroup(string svgId, int xOffset = 0, int yOffset = 0)
        {
            string result = string.Empty;

            for (int i = 1; i <= 16; i++)
            {
                //result += this.RenderLineToSVG(i, pixelSize, xOffset, yOffset);
                result += this.RenderLineToSVG(i);
            }

            return $@"<g id=""{svgId}"" x=""{xOffset * 8}"" y=""{(yOffset * 16)}"">{result}</g>";
        }
    }
}
