using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public class SignalState : ISignalState
    {
        /// <summary>
        /// Gets the flag to indicate whether the signal is off
        /// </summary>
        public bool IsOff { get; private set; }

        /// <summary>
        /// Gets the current state of the signal
        /// </summary>
        public SignalStateOptions CurrentState { get; private set; }

        /// <summary>
        /// Sets the current state
        /// </summary>
        /// <param name="signalHead">The <see cref="ISignalHead"/> the state is being set for</param>
        /// <param name="newState">The new state</param>
        public void SetCurrentState(ISignalHead signalHead, SignalStateOptions newState)
        {
            if ((signalHead.PossibleSignalStateOptions & newState) != newState)
            {
                throw new ArgumentException("The supplied new state is not valid based on the possible states for the supplied signal head");
            }

            //set the current state
            this.CurrentState = newState;

            //set the is off flag
            this.IsOff = ((signalHead.OffSignalState & newState) != 0);
        }

        /// <summary>
        /// Builds a signal state for the supplied <see cref="ISignalHead"/>. Will validate the signal state against the possible states for the signal head
        /// </summary>
        /// <param name="signalHead">The signal head</param>
        /// <param name="signalState">The signal state</param>
        /// <returns></returns>
        public static SignalState BuildSignalState(ISignalHead signalHead, SignalStateOptions signalState)
        {
            var result = new SignalState();
            result.SetCurrentState(signalHead, signalState);
            return result;
        }
    }
}
