using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GF.Core.Helpers;

namespace GF.SimSig
{
    /// <summary>
    /// Class representing a SimSig panel point / switch
    /// </summary>
    public class Point : PanelItemBase, ISimSigPanelItem, ISimSigTrackCircuit
    {
        /// <summary>
        /// Gets the orientation of the point.
        /// </summary>
        [JsonProperty("orientation")]
        public SimSigOrientation Orientation { get; private set; }

        /// <summary>
        /// Gets the track circuit id
        /// </summary>
        [JsonProperty("trackCircuitId")]
        public string TrackCircuitID { get; private set; }

        /// <summary>
        /// Gets or sets the flag to indicate whether the point is occupied
        /// </summary>
        [JsonProperty("isOccupied")]
        public bool IsOccupied { get; set; }

        /// <summary>
        /// Gets or sets the flag to indicate whether the point has a route set over it
        /// </summary>
        [JsonProperty("hasRouteSet")]
        public bool HasRouteSet { get; set; }

        /// <summary>
        /// Gets or sets the point config
        /// </summary>
        [JsonProperty("pointConfig")]
        public SimSigPointConfig PointConfig { get; set; }

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="simSigID">The SimSig ID of the point / switch</param>
        /// <param name="xOffSet">The x (horizontal) offset of where the point should be placed on the panel</param>
        /// <param name="yOffSet">The y (vertical) offset of where the point should be placed on the panel</param>
        /// <param name="orientation">The <see cref="SimSigOrientation"/> of the point</param>
        /// <param name="trackCircuitId">The track circuit Id</param>
        /// <param name="isOccupied">Indicates whether the associated track circuit is occupied</param>
        /// <param name="hasRouteSet">Indicates whether a rinoute is set across the points</aaram>
        /// <param name="config">The <see cref="SimSigPointConfig"/> config associated with the points</param>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="orientation"/> argument is invalid.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="trackCircuitId"/> is null or an empty string.</exception>
        public Point(string simSigID, ushort xOffSet, ushort yOffSet, SimSigOrientation orientation, string trackCircuitId, bool isOccupied, bool hasRouteSet, SimSigPointConfig config) : base (simSigID, xOffSet, yOffSet)
        {
            if (orientation.Equals(SimSigOrientation.North) | orientation.Equals(SimSigOrientation.West) | orientation.Equals(SimSigOrientation.South) | orientation.Equals(SimSigOrientation.East))
            {
                throw new ArgumentException("An invalid orientation was supplied. Only NE, SE, SW & NW are valid.", nameof(orientation));
            }

            this.Orientation = orientation;

            if (string.IsNullOrEmpty(trackCircuitId))
            {
                throw new ArgumentNullException(nameof(trackCircuitId), "You must supply the Track Circuit ID for the point");
            }

            this.TrackCircuitID = trackCircuitId;
            this.IsOccupied = isOccupied;
            this.HasRouteSet = hasRouteSet;
            this.PointConfig = config;

            //build the initial config.
            this.BuildConfig();
        }

        /// <summary>
        /// Builds the configuration
        /// </summary>
        public void BuildConfig()
        {
            base.ClearConfig();

            string key  = string.Empty;

            switch (this.Orientation)
            {
                case SimSigOrientation.SouthEast:

                    key = this.PointConfig == SimSigPointConfig.Auto ? "0x65" : this.PointConfig == SimSigPointConfig.Reverse ? "0x69" : "0x61";
                    break;
                case SimSigOrientation.SouthWest:
                    key = this.PointConfig == SimSigPointConfig.Auto ? "0x66" : this.PointConfig == SimSigPointConfig.Reverse ? "0x6A" : "0x61";
                    break;
                case SimSigOrientation.NorthWest:
                    key = this.PointConfig == SimSigPointConfig.Auto ? "0x64" : this.PointConfig == SimSigPointConfig.Reverse ? "0x68" : "0x61";
                    break;
                case SimSigOrientation.NorthEast:
                    key = this.PointConfig == SimSigPointConfig.Auto ? "0x67" : this.PointConfig == SimSigPointConfig.Reverse ? "0x6B" : "0x61";
                    break;
            }

            base.AddConfig(new SimSigFontTokenConfig(key, this.GetSimSigColour(), 0, 0, (this.PointConfig & SimSigPointConfig.InTransition) == SimSigPointConfig.InTransition));
        }

        /// <summary>
        /// Calculate the colour of the point
        /// </summary>
        /// <returns>The <see cref="SimSigColour"/> of the point</returns>
        private SimSigColour GetSimSigColour()
        {
            if ((this.PointConfig & SimSigPointConfig.Keyed) == SimSigPointConfig.Keyed) return SimSigColour.Blue;
            if (this.IsOccupied) return SimSigColour.Red;
            if (this.HasRouteSet) return SimSigColour.White;
            return SimSigColour.Grey;
        }

    }
}
