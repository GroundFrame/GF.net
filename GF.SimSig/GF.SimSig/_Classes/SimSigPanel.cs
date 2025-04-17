using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{

    /// <summary>
    /// Class representing a SimSig panel
    /// </summary>
    public class SimSigPanel
    {
        #region Fields

        private readonly List<ISimSigPanelItem> _panelItems; //stores the collection of panel items which make up the panel

        #endregion Fields

        /// <summary>
        /// Gets the panel description
        /// </summary>
        public string Description { get; private set; }


        /// <summary>
        /// Gets the collection of panel items.
        /// </summary>
        public IEnumerable<ISimSigPanelItem> PanelItems { get { return this._panelItems; } }

        /// <summary>
        /// Instantiates a new panel.
        /// </summary>
        /// <param name="description">The panel description</param>
        public SimSigPanel(string description)
        {
            this.Description = description;
            this._panelItems = [];
        }

        /// <summary>
        /// Adds a panel item.
        /// </summary>
        /// <param name="panelItem"></param>
        public void AddPanelItem(ISimSigPanelItem panelItem)
        {
            this._panelItems.Add(panelItem);
        }

        /// <summary>
        /// Builds the SVG for the panel
        /// </summary>
        /// <param name="font">A struct containining the SimSig font.</param>
        /// <param name="styleClass">A <see cref="SimSigStyle"/> object that defines the css for the SVG.</param>
        /// <returns></returns>
        public string BuildSVG(ISimSigFont font, SimSigStyle styleClass)
        {
            string result = string.Empty; //stores the resulting SVG

            List<string> usedFontKeys = []; //stores any font keys used whilst building the SVG. Prevents characters being redefined everytime they are reference. Instead they are only defined once and then reference if reused.

            //loop around each panel item and generate the SVG.
            foreach (var panelItem in this.PanelItems)
            {
                result += panelItem.GenerateSVG(font, styleClass, usedFontKeys);
            }

            //calculate the canvas / viewbox of the SVG
            int maxXOffSet = this.PanelItems.SelectMany(pi => pi.Config.ToList()).Max(c => (c.XOffSet + 1) * (c.Key.Length / 4)) - 1;
            int maxYOffSet = this.PanelItems.SelectMany(pi => pi.Config.ToList()).Max(c => c.YOffSet);

            //return the SVG
            return $@"{styleClass.StyleHTML}<svg xmlns=""http://www.w3.org/2000/svg""  width=""{(maxXOffSet + 1)}"" height=""{(font.FontHeightPixels * (maxYOffSet + 1)) + 1}"">{result}</svg>";
        }

        /// <summary>
        /// Gets the collection of panel items for this panel of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of panel item to return</typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetPanelItemsOfType<T>()
        {
            return this.PanelItems.OfType<T>();
        }

        /// <summary>
        /// Updates the occupancy of the track circuit on the panel matching the supplied track circuit id
        /// </summary>
        /// <param name="trackCircuitId">The track circuit to update</param>
        /// <param name="isOccuipied">The flag to indicate whether the track circuit is occupied</param>
        /// <remarks>No exception will be thrown if the track circuit id does not exist on the panel</remarks>
        /// <exception cref="ArgumentNullException">Thrown if  null or an empty string is passed to the <paramref name="trackCircuitId"/> argument</exception>
        public void UpdateTrackCircuitOccupancy(string trackCircuitId, bool isOccuipied)
        {
            if (string.IsNullOrEmpty(trackCircuitId))
            {
                throw new ArgumentNullException(nameof(trackCircuitId), "You must supply a track circuit id.");
            }

            foreach(var panelItem in this.GetPanelItemsOfType<ISimSigTrackCircuit>().Where(tc => tc.TrackCircuitID.Equals(trackCircuitId)))
            {
                panelItem.IsOccupied = isOccuipied;
                panelItem.BuildConfig();
            }
        }
    }
}