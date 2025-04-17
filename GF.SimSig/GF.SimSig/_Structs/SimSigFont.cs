using GF.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{ 
    /// <summary>
    /// A struct representing the SimSig font
    /// </summary>
    public readonly struct SimSigFont : ISimSigFont
    {
        #region Constants
        
        private const int _fontWidthPixels = 8;
        private const int _fontHeightPixels = 16;

        #endregion Constants

        #region Fields

        [JsonProperty("characters")]
        private readonly Dictionary<string, SimSigCharacter> _characters = []; //contains a dictionary of all the fonts

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the width of the font in pixels
        /// </summary>
        public int FontWidthPixels { get { return _fontWidthPixels; } }

        /// <summary>
        /// Gets the height of the font in pixels
        /// </summary>
        public int FontHeightPixels { get { return _fontHeightPixels; } }

        #endregion Properties

        public SimSigFont() { }

        /// <summary>
        /// Builds the standard SimSig font
        /// </summary>
        /// <returns>A build <see cref="SimSigFont"/></returns>
        public static SimSigFont BuildDefault()
        {
            SimSigFont font = new();
            font.AddCharacter(new SimSigCharacter("0x26", "Ampersand", 0, 0, 0, 28, 54, 54, 28, 28, 78, 123, 51, 115, 110, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x27", "Apostrophe", 0, 0, 0, 24, 24, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x2A", "Asterix", 0, 0, 0, 0, 0, 99, 54, 28, 127, 28, 54, 99, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x2E", "Full stop", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 24, 24, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x32", "2", 0, 0, 0, 62, 99, 99, 48, 24, 12, 6, 3, 99, 127, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x3D", "Equal", 0, 0, 0, 0, 0, 0, 0, 255, 0, 255, 0, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x41", "A", 0, 0, 0, 24, 60, 102, 102, 126, 102, 102, 102, 102, 102, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x42", "B", 0, 0, 0, 63, 102, 102, 102, 62, 102, 102, 102, 102, 63, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x43", "C", 0, 0, 0, 62, 99, 99, 3, 3, 3, 3, 3, 99, 62, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x44", "D", 0, 0, 0, 31, 54, 102, 102, 102, 102, 102, 102, 54, 31, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x45", "E", 0, 0, 0, 127, 102, 6, 22, 30, 22, 6, 6, 102, 127, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x46", "F", 0, 0, 0, 127, 102, 6, 22, 30, 22, 6, 6, 6, 15, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x47", "G", 0, 0, 0, 62, 99, 99, 3, 3, 3, 123, 99, 99, 126, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x48", "H", 0, 0, 0, 99, 99, 99, 99, 127, 99, 99, 99, 99, 99, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x49", "I", 0, 0, 0, 63, 12, 12, 12, 12, 12, 12, 12, 12, 63, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x4A", "J", 0, 0, 0, 126, 48, 48, 48, 48, 48, 48, 51, 51, 30, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x4B", "K", 0, 0, 0, 99, 99, 51, 27, 15, 27, 51, 99, 99, 99, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x4C", "L", 0, 0, 0, 15, 6, 6, 6, 6, 6, 6, 102, 102, 127, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x4D", "M", 0, 0, 0, 65, 99, 119, 127, 107, 99, 99, 99, 99, 99, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x4E", "N", 0, 0, 0, 99, 103, 103, 111, 107, 123, 115, 115, 99, 99, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x4F", "O", 0, 0, 0, 62, 99, 99, 99, 99, 99, 99, 99, 99, 62, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x50", "P", 0, 0, 0, 63, 102, 102, 62, 6, 6, 6, 6, 6, 15, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x51", "Q", 0, 0, 0, 62, 99, 99, 99, 99, 99, 99, 115, 51, 110, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x52", "R", 0, 0, 0, 63, 102, 102, 102, 62, 30, 54, 102, 102, 111, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x53", "S", 0, 0, 0, 62, 99, 99, 3, 62, 96, 96, 99, 99, 62, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x54", "T", 0, 0, 0, 126, 90, 24, 24, 24, 24, 24, 24, 24, 60, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x55", "U", 0, 0, 0, 99, 99, 99, 99, 99, 99, 99, 99, 99, 62, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x56", "V", 0, 0, 0, 99, 99, 99, 99, 99, 54, 54, 28, 8, 8, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x57", "W", 0, 0, 0, 99, 99, 99, 99, 99, 107, 107, 127, 119, 34, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x58", "X", 0, 0, 0, 99, 99, 54, 54, 28, 28, 54, 54, 99, 99, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x59", "Y", 0, 0, 0, 51, 51, 51, 30, 12, 12, 12, 12, 12, 30, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x5A", "Z", 0, 0, 0, 127, 99, 96, 48, 24, 12, 6, 3, 99, 127, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x5B", "Overlap Right", 0, 0, 252, 252, 252, 252, 252, 252, 252, 252, 252, 252, 252, 252, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x5C", "Blank Character", 0, 126, 126, 126, 126, 126, 126, 126, 126, 126, 126, 126, 126, 126, 126, 0));
            font.AddCharacter(new SimSigCharacter("0x5D", "Overlap Left", 0, 0, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x61", "Horizontal Bar", 0, 0, 0, 0, 0, 255, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x62", "Horizontal Bar Left", 0, 0, 0, 0, 0, 63, 63, 63, 63, 63, 63, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x63", "Horizontal Bar Right", 0, 0, 0, 0, 0, 252, 252, 252, 252, 252, 252, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x64", "Point Back With Normal Bottom", 60, 120, 120, 240, 240, 255, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x65", "Point Back With Normal Top", 0, 0, 0, 0, 0, 255, 255, 255, 255, 255, 255, 15, 15, 30, 30, 60));
            font.AddCharacter(new SimSigCharacter("0x66", "Point Forward with Normal Top", 0, 0, 0, 0, 0, 255, 255, 255, 255, 255, 255, 240, 240, 120, 120, 60));
            font.AddCharacter(new SimSigCharacter("0x67", "Point Forward with Normal Bottom", 60, 30, 30, 15, 15, 255, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0)  );
            font.AddCharacter(new SimSigCharacter("0x68", "Circuited Back Top", 60, 120, 120, 240, 240, 224, 224, 192, 192, 128, 128, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x69", "Circuited Back Bottom", 0, 0, 0, 0, 0, 1, 1, 3, 3, 7, 7, 15, 15, 30, 30, 60));
            font.AddCharacter(new SimSigCharacter("0x6A", "Circuited Forward Bottom", 0, 0, 0, 0, 0, 128, 128, 192, 192, 224, 224, 240, 240, 120, 120, 60));
            font.AddCharacter(new SimSigCharacter("0x6B", "Circuited Forward Top", 60, 30, 30, 15, 15, 7, 7, 3, 3, 1, 1, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x6C", "Circuited Back Top Short", 60, 120, 120, 112, 112, 96, 96, 64, 64, 0, 0, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x6D", "Circuited Back Bottom Short", 0, 0, 0, 0, 0, 0, 0, 2, 2, 6, 6, 14, 14, 30, 30, 60));
            font.AddCharacter(new SimSigCharacter("0x6E", "Circuited Forward Bottom Short", 0, 0, 0, 0, 0, 0, 0, 64, 64, 96, 96, 112, 112, 120, 120, 60));
            font.AddCharacter(new SimSigCharacter("0x6F", "Circuited Forward Top Short", 60, 30, 30, 14, 14, 6, 6, 2, 2, 0, 0, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x70", "Signal Hollow", 0, 0, 0, 0, 60, 102, 195, 129, 129, 195, 102, 60, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x71", "Signal Head", 0, 0, 0, 0, 60, 126, 255, 255, 255, 255, 126, 60, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x72", "Shunt Right", 0, 0, 0, 0, 3, 15, 31, 63, 127, 127, 255, 255, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x73", "Shunt Left", 0, 0, 0, 0, 255, 255, 254, 254, 252, 248, 240, 192, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x74", "Uncircuited Back Top", 36, 72, 72, 144, 144, 32, 32, 64, 64, 128, 128, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x75", "Uncircuited Back Bottom", 0, 0, 0, 0, 0, 1, 1, 2, 2, 4, 4, 9, 9, 18, 18, 36));
            font.AddCharacter(new SimSigCharacter("0x76", "Uncircuited Forward Bottom", 0, 0, 0, 0, 0, 128, 128, 64, 64, 32, 32, 144, 144, 72, 72, 36));
            font.AddCharacter(new SimSigCharacter("0x77", "Uncircuited Forward Top", 36, 18, 18, 9, 9, 4, 4, 2, 2, 1, 1, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x78", "Uncircuited Section", 0, 0, 0, 0, 0, 255, 0, 0, 0, 0, 255, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x89", "Border Horizontal", 0, 0, 0, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x80", "Border Edge Top Left", 0, 0, 0, 0, 0, 0, 0, 248, 248, 24, 24, 24, 24, 24, 24, 24));
            font.AddCharacter(new SimSigCharacter("0x81", "Border Edge Top Right", 0, 0, 0, 0, 0, 0, 0, 31, 31, 24, 24, 24, 24, 24, 24, 24));
            font.AddCharacter(new SimSigCharacter("0x82", "Border Edge Bottom Left", 24, 24, 24, 24, 24, 24, 24, 248, 248, 0, 0, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x83", "Border Edge Bottom Right", 24, 24, 24, 24, 24, 24, 24, 31, 31, 0, 0, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0x8A", "Border Vertical", 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24));
            font.AddCharacter(new SimSigCharacter("0x8C", "Left Arrow Large", 0, 128, 192, 224, 240, 248, 252, 254, 254, 252, 248, 240, 224, 192, 128, 0));
            font.AddCharacter(new SimSigCharacter("0x8D", "Right Arrow Large", 0, 1, 3, 7, 15, 31, 63, 127, 127, 63, 31, 15, 7, 3, 1, 0));
            font.AddCharacter(new SimSigCharacter("0x8E", "Complete Block", 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255));
            font.AddCharacter(new SimSigCharacter("0x90", "Signal Post Up Right", 0, 0, 0, 0, 0, 96, 96, 252, 252, 108, 108, 12, 12, 12, 12, 12));
            font.AddCharacter(new SimSigCharacter("0x93", "Signal Post Down Left", 48, 48, 48, 48, 48, 54, 54, 63, 63, 6, 6, 0, 0, 0, 0, 0));
            font.AddCharacter(new SimSigCharacter("0xE0", " ", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));

            return font;
        }

        /// <summary>
        /// Gets the number of characters registered with the font
        /// </summary>
        /// <returns>The number of characters registered with the font</returns>
        public readonly int GetCharacterCount()
        {
            if (this._characters == null)
            {
                return 0;
            }
            else
            {
                return this._characters.Count;
            }
        }

        /// <summary>
        /// Adds a character to the font
        /// </summary>
        /// <param name="key">The character key</param>
        /// <param name="character">The character definition</param>
        public readonly void AddCharacter(SimSigCharacter character)
        {
            this._characters.Add(character.Key, character);
        }

        /// <summary>
        /// Generates an SVG which returns the token generated from the supplied key
        /// </summary>
        /// <param name="config">The configuration of the SVG to be returned</param>
        /// <param name="simSigStyle">A <see cref="SimSigStyle"/> object contained the various style classes to be used by the SVG.</param>
        /// <param name="usedFontKeys">A list of the font keys which have already been built. Used to help optimse the SVG</param>
        /// <returns></returns>
        private string GetTokenSVG(SimSigFontTokenConfig config, SimSigStyle simSigStyle, List<string> usedFontKeys)
        {
            string result = string.Empty;
            int counter = 0;

            usedFontKeys ??= [];

            //split the config key into font keys by splitting every 4 characters (for example 0x70, 0x71 etc.) and generate the SVG for each font character.
            foreach (string fontKey in config.Key.SplitInParts(4))
            {
                //check to see whether the SVG has already been generated for the font key.
                if (!usedFontKeys.Any(t => t.Equals(fontKey)))
                {
                    //if not create an SVG which is rendered 8 characters to the left of the screen (so off screen)
                    result += $@"<svg x=""-{_fontWidthPixels}"" y=""0"">{this._characters[fontKey].RenderCharacterToSVGGroup(fontKey, counter + config.XOffSet, config.YOffSet)}</svg>";
                    //now render in position
                    result += $@"<use href=""#{fontKey}"" x=""{((counter + config.XOffSet) * _fontWidthPixels) + _fontWidthPixels}"" y =""{config.YOffSet * _fontHeightPixels}"" />";
                    //result += $@"<use href=""#{fontKey}"" x=""{((counter + config.XOffSet) * _fontWidthPixels) + _fontWidthPixels}"" y =""{config.YOffSet * _fontHeightPixels}"" />";
                    //result += $@"<svg x=""{(counter + config.XOffSet) * _fontWidthPixels}"" y=""{config.YOffSet * _fontHeightPixels}"">{this._characters[fontKey].RenderCharacterToSVG(fontKey, counter + config.XOffSet, config.YOffSet)}</svg>";
                    usedFontKeys.Add(fontKey);
                    
                }
                else
                {
                    //if yes just reference the SVG element already generated for the font key.
                    result += $@"<use href=""#{fontKey}"" x=""{((counter + config.XOffSet) * _fontWidthPixels) + _fontWidthPixels}"" y =""{config.YOffSet * _fontHeightPixels}"" />";
                    //result += $@"<use href=""#{fontKey}"" x=""{(counter + config.XOffSet) * _fontWidthPixels}"" y =""{config.YOffSet * _fontHeightPixels}"" />";
                }

                counter++;
            }

            string flashClass = config.IsFlashing ? "flash" : "";
            return $@"<g class=""{simSigStyle.GetClassName(config.Colour)}{flashClass}"">{result}</g>";
        }

        /// <summary>
        /// Builds an SVG from the font for the supplied config and style.
        /// </summary>
        /// <param name="configs">A collection of <see cref="SimSigFontTokenConfig"/> containing the tokens for each SVG to be returned</param>
        /// <param name="simSigStyle">A <see cref="SimSigStyle"/> object which contains the style / classes to be applied to each SVG element</param>
        /// <param name="xOffSet">The grid horizontal offset (left to right)</param>
        /// <param name="yOffSet">The grid vertical offset (top to bottom)</param>
        /// <returns>A <see cref="string"/> containing the SVG definition</returns>
        public string BuildSVG(IEnumerable<SimSigFontTokenConfig> configs, SimSigStyle simSigStyle, List<string> usedFontKeys, int xOffSet = 0, int yOffSet = 0)
        {
            string result = string.Empty;

            //used font keys
            if (usedFontKeys is null)
            {
                usedFontKeys = [];
            }

            //loop around each config in the configs collection and build the SVG
            foreach (var config in configs)
            {
                config.XOffSet += xOffSet;
                config.YOffSet += yOffSet;
                string configResult = this.GetTokenSVG(config, simSigStyle , usedFontKeys);
                result += configResult;
            }

            return result;
        }

        public static string ConvertTextToHex(string text)
        {
            string result = string.Empty;

            foreach (char character in text)
            {
                if (character.Equals(' '))
                {
                    result += "0xE0";
                }
                else
                {
                    result += $"0x{BitConverter.ToString(new byte[] { Convert.ToByte(character) })}";
                }
            }

            return result;
        }
    }
}
