using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public abstract class ColourLightBase
    {
        /// <summary>
        /// Gets the signal head Id
        /// </summary>
        public SignalHeadId Id { get; }

        /// <summary>
        /// Gets the signalling type
        /// </summary>
        public SignallingType Type { get { return SignallingType.ColourLight; } }

        /// <summary>
        /// Gets the bulb type
        /// </summary>
        public ColourLightBulbType BulbType { get; private set; }

        /// <summary>
        /// Gets the state options of the signal
        /// </summary>
        public SignalStateOptions PossibleSignalStateOptions { get; private set; }

        /// <summary>
        /// Gets the state options which indicate the signal is off
        /// </summary>
        public SignalStateOptions OffSignalState { get; private set; }

        /// <summary>
        /// Gets the default state of the signal head
        /// </summary>
        public ISignalState DefaultState { get; private set; }

        #region Constructors

        public ColourLightBase(ColourLightBulbType bulbType, SignalStateOptions possibleSignalStateOptions, SignalStateOptions offSignalState, SignalStateOptions defaultState)
        {
            this.Id = new SignalHeadId(Guid.NewGuid());
            this.BulbType = bulbType;
            this.PossibleSignalStateOptions = possibleSignalStateOptions;
            this.OffSignalState = offSignalState;
        }

        protected void SetDefaultState(ISignalState signalState)
        {
            this.DefaultState = signalState;
        }

        #endregion Constructors
    }
}
