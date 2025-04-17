using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    /// <summary>
    /// Class containing the SVG styling. Intended to be a Singleton to ensure the indexing of the individual classes remaining the in the order within the _styleClasses dictionary
    /// </summary>
    public sealed class SimSigStyle
    {
        private readonly Dictionary<SimSigColour, string> _styleClasses = new (); //stores the collection of style classes

        private static readonly Lazy<SimSigStyle> lazy =
            new(() => new SimSigStyle());

        /// <summary>
        /// Gets the class instance
        /// </summary>
        public static SimSigStyle Instance { get { return lazy.Value; } }

        /// <summary>
        /// Gets the style class defintion.
        /// </summary>
        public string StyleHTML { get; private set; }

        /// <summary>
        /// Gets the class name
        /// </summary>
        /// <param name="colour">The SimSig colour</param>
        /// <returns>The class name</returns>
        public string GetClassName (SimSigColour colour)
        {
            return this._styleClasses[colour].ToString();
        }

        /// <summary>
        /// Private constructor. Builds the SVG styles from the colours available in the <see cref="SimSigColour"/> enum.
        /// </summary>
        private SimSigStyle()
        {
            //set the colour array
            var colourArray = Enum.GetValues(typeof(SimSigColour)).Cast<SimSigColour>().ToArray<SimSigColour>();

            string styleHTML = string.Empty; //stores the style html.

            //loop around each colour and add to the dictionary with the index prefixed with "colour-"
            for (int i = 0; i < colourArray.Length; i++)
            {
                this._styleClasses.Add(colourArray[i], $"colour-{i}");
                styleHTML += $".colour-{i} {{stroke: {colourArray[i].GetHex()};stroke-width:1.25;stroke-linecap=\"butt\"}}";
            }

            //add the style to make an item flash
            styleHTML += $".flash {{animation: blinker 1s step-start infinite}} @keyframes blinker {{ 50% {{opacity: 0;}}}}";

            //wrap the html in a style tag and set the styleHTML property.
            this.StyleHTML = $"<style>{styleHTML}</style>";
        }
    }
}
