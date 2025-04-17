using GF.Common.Translations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.Common.SimSig
{
    /// <summary>
    /// Class representing a single SimSig simulation data transport object. Used to send SimSig simulation to the client from GF services.
    /// </summary>
    public class GFSimSigSimulationDTO
    {

        /// <summary>
        /// Gets or sets the user's key
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; private set; }

        /// <summary>
        /// Gets or sets the simulation name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the simulation code
        /// </summary>
        [JsonProperty("code")]
        public string SimSigCode { get; set; }

        public GFSimSigSimulationDTO(string key, string name, string simSigCode)
        {
            Key = key;
            Name = name;
            SimSigCode = simSigCode;
        }
    }

    /// <summary>
    /// Class representing a new SimSig simulation. Used to send details of a edited SimSig simulation from a client to GF services.
    /// </summary>
    public class GFSimSigSimulationNewDTO
    {
        /// <summary>
        /// Gets or sets the simulation name
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the user's last name
        /// </summary>
        [JsonProperty("code")]
        public string? SimSigCode { get; set; }
    }

    /// <summary>
    /// Class representing an edited SimSig simulation. Used to send details of an edited SimSig simulation from a client to GF services.
    /// </summary>
    public class GFSimSigSimulationEditDTO : GFSimSigSimulationNewDTO
    {
        /// <summary>
        /// Gets or sets the simulation key
        /// </summary>
        [JsonProperty("key")]
        public string? Key { get; set; }
    }
}
