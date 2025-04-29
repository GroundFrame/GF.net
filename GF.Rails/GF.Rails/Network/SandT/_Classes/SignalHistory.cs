using GF.Common;
using GF.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network.SandT
{
    /// <summary>
    /// Class representing a single signal history
    /// </summary>
    public class SignalHistory : TemporalHistoryBase<SignalId>, ITemporalHistory<SignalId>
    {
        #region Fields

        private readonly SignalBoxPanelOrWorkstationId _signalBoxPanelOrWorkstationId;
        private readonly List<(SignalHeadType, ISignalHead)> _heads = new List<(SignalHeadType, ISignalHead)>();

        #endregion Fields

        /// <summary>
        /// Gets the signalling type
        /// </summary>
        public SignallingType SignallingType { get; private set; }
        
        /// <summary>
        /// Gets the signal box / panel that this signal belongs to.
        /// </summary>
        public ISignalBoxPanelOrWorkstation SignalBoxPanelOrWorkstation { get; private set; }

        /// <summary>
        /// Gets the signal box that this signal belongs to
        /// </summary>
        public ISignalBox SignalBox { get { return this.SignalBoxPanelOrWorkstation.SignalBox; } }

        /// <summary>
        /// Gets the signal heads associated with this signal
        /// </summary>
        public IEnumerable<KeyValuePair<SignalHeadType, ISignalHead>> SignalHeads { get; private set; }

        /// <summary>
        /// Gets the signal block associated with this signal
        /// </summary>
        public ISignalBlock AssociatedSignalBlock { get; private set; }

        /// <summary>
        /// Gets the direction of the associated signal box
        /// </summary>
        public Direction AssociatedSignalBlockDirection { get; private set; }

        /// <summary>
        /// Instantiates a new <see cref="SignalHistory"/>
        /// </summary>
        /// <param name="id">The parent signal <see cref="SignalId"/></param>
        /// <param name="startDate">The signal block start date</param>
        /// <param name="endDate">The signal block end date (pass null if still current)</param>
        /// <param name="panelOrWorkstation">The panel or workstation this signal belongs to</param>
        /// <param name="signalHeadType">The signalkl head type</param>
        /// <param name="signalHead">The default or first signal head</param>
        public SignalHistory(SignalId id, DateTime startDate, DateTime? endDate, ISignalBoxPanelOrWorkstation panelOrWorkstation, SignalHeadType signalHeadType, ISignalHead signalHead, ISignalBlock associatedSignalBlock, DirectionType associatedSignalBlockDirection) : base(id, startDate, endDate) 
        { 
            this.SignalBoxPanelOrWorkstation = panelOrWorkstation;
            this._signalBoxPanelOrWorkstationId = panelOrWorkstation.Id;
            this.AssociatedSignalBlock = associatedSignalBlock;
            this.AssociatedSignalBlockDirection = new Direction(associatedSignalBlockDirection);
            this._heads.Add((signalHeadType, signalHead));
        }
    }
}
