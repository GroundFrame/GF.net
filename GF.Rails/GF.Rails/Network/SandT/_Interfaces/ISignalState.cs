using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public interface ISignalState
    {
        /// <summary>
        /// Gets the flag to indicate whether the signal is off
        /// </summary>
        public bool IsOff { get; }

        /// <summary>
        /// Gets the current state of the signal
        /// </summary>
        public SignalStateOptions CurrentState { get;}

        /// <summary>
        /// Sets the current state
        /// </summary>
        /// <param name="signalHead">The <see cref="ISignalHead"/> the state is being set for</param>
        /// <param name="newState">The new state</param>
        public void SetCurrentState(ISignalHead signalHead, SignalStateOptions newState);
    }
}
