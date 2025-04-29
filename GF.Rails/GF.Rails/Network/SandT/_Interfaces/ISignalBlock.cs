using GF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public interface ISignalBlock : ITemporal<SignalBlockId, SignalBlockHistory>
    {
        /// <summary>
        /// Gets the appox length of the signal block
        /// </summary>
        public Length Length { get; }

        /// <summary>
        /// Gets the platform code assoicated with this signal block
        /// </summary>
        public string PlatformCode { get; }
    }
}
