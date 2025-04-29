using GF.Common;
using GF.Graph;
using GF.Rails.Network;
using GF.Rails.Operations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public class Signal : TemporalBase<SignalId, SignalHistory>, ITemporal<SignalId, SignalHistory>, ISignal, ISignalBoxPanelOrWorkstationItem
    {
        public ISpatial Coordinates { get; private set; }

        public Guid PanelItemId { get { return this.Id.Value; } }

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="Signal"/>
        /// </summary>
        /// <param name="number">The signal number excluding the signal box code</param>
        /// <param name="coordinates">The signal coordinates</param>
        public Signal(string number, ISpatial coordinates) : base(new SignalId(Guid.NewGuid()), number)
        {
            this.Coordinates = coordinates;
        }

        public Signal(string number, ISpatial coordinates, DateTime startDate, ISignalBoxPanelOrWorkstation signalBoxPanelOrWorkstation, SignalHeadType signalHeadType, ISignalHead signalHead, ISignalBlock associatedSignalBlock, DirectionType associatedSignalBlockDirection) : this(number, coordinates)
        {
            this.AddHistory(new SignalHistory(this.Id, startDate, null, signalBoxPanelOrWorkstation, signalHeadType, signalHead, associatedSignalBlock, associatedSignalBlockDirection));
            signalBoxPanelOrWorkstation.AddPanelItem(startDate, this);
        }

        #endregion Constructors

        #region Methods

        public void AddHistory(DateTime startDate, DateTime? endDate, ISignalBoxPanelOrWorkstation signalBoxOrWorkstation, SignalHeadType signalHeadType, ISignalHead signalHead, ISignalBlock associatedSignalBlock, DirectionType associatedSignalBlockDirection)
        {
            this.AddHistory(new SignalHistory(this.Id, startDate, endDate, signalBoxOrWorkstation, signalHeadType, signalHead, associatedSignalBlock, associatedSignalBlockDirection));
        }

        /// <summary>
        /// Determines whether 2 <see cref="Signal"/> items match
        /// </summary>
        /// <param name="obj">The other object to compare</param>
        /// <returns>True if the items are equal otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Signal))
            {
                return false;            }

            return this.PanelItemId.Equals((obj as Signal).PanelItemId);
        }

        /// <summary>
        /// Returns the hashcode for this isntance
        /// </summary>
        public override int GetHashCode()
        {
            return (this.PanelItemId).GetHashCode();
        }

        #endregion Methods
    }
}
