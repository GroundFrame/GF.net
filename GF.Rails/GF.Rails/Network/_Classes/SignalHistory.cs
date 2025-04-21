using GF.Common;
using GF.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    /// <summary>
    /// Class representing a single signal history
    /// </summary>
    public class SignalHistory : TemporalHistoryBase<SignalId>, ITemporalHistory<SignalId>
    {
        #region Fields

        private readonly SignalBoxPanelOrWorkstationId _signalBoxPanelOrWorkstationId;

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
        /// Instantiates a new <see cref="SignalHistory"/>
        /// </summary>
        /// <param name="id">The parent signal <see cref="SignalId"/></param>
        /// <param name="startDate">The signal block start date</param>
        /// <param name="endDate">The signal block end date (pass null if still current)</param>
        /// <param name="signallingType">The signalling type</param>
        /// <param name="panelOrWorkstation">The panel or workstation this signal belongs to</param>
        public SignalHistory(SignalId id, DateTime startDate, DateTime? endDate, SignallingType signallingType, ISignalBoxPanelOrWorkstation panelOrWorkstation) : base(id, startDate, endDate) 
        { 
            if (signallingType == SignallingType.CallOn)
            {
                throw new ArgumentException("You cannot pass SignallingType.CallOn as a signalling type on it's own. It must be passed together with another SignallingType. For example SignallingType.MainAspect | SignallingType.CallOn");
            }

            this.SignallingType = signallingType;
            this.SignalBoxPanelOrWorkstation = panelOrWorkstation;
            this._signalBoxPanelOrWorkstationId = panelOrWorkstation.Id;
        }
    }
}
