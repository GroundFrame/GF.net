using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public interface ISignalHead
    {
        /// <summary>
        /// Gets the signal head Id
        /// </summary>
        public SignalHeadId Id { get; }

        /// <summary>
        /// Gets the signalling type
        /// </summary>
        public SignallingType Type { get; }

        /// <summary>
        /// Gets the possible state options of the signal
        /// </summary>
        public SignalStateOptions PossibleSignalStateOptions { get; }

        /// <summary>
        /// Gets the state options which indicate the signal is off
        /// </summary>
        public SignalStateOptions OffSignalState { get; }

        /// <summary>
        /// Gets the default state of the signal head
        /// </summary>
        public ISignalState DefaultState { get; }
    }
}
