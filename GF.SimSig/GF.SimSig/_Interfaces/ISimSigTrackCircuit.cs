using Newtonsoft.Json;

namespace GF.SimSig
{
    /// <summary>
    /// Interface represent a track circuit
    /// </summary>
    public interface ISimSigTrackCircuit
    {
        /// <summary>
        /// Gets or sets the flag to indiate whether the track circuit is occupied
        /// </summary>
        [JsonProperty("isOccupied")]
        public bool IsOccupied { get; set; }

        /// <summary>
        /// Gets or sets the flag to indiate whether the track circuit has a route set
        /// </summary>
        [JsonProperty("hasRouteSet")]
        public bool HasRouteSet { get; set; }

        /// <summary>
        /// Gets the track circuit id
        /// </summary>
        [JsonProperty("trackCircuitId")]
        public string TrackCircuitID { get; }

        /// <summary>
        /// Builds the track circuit config
        /// </summary>
        public void BuildConfig();
    }
}
