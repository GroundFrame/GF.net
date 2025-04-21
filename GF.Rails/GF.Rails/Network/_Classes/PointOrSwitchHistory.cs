using GF.Common;
using GF.Graph;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    public class PointOrSwitchHistory : TemporalHistoryBase<PointOrSwitchId>, ITemporalHistory<PointOrSwitchId>
    {
        #region Fields

        private SignalBoxPanelOrWorkstationId _signalBoxPanelOrWorkstationId;

        #endregion Fields

        /// <summary>
        /// Gets the signal box panel or workstation this point or switch belongs to
        /// </summary>
        public ISignalBoxPanelOrWorkstation SignalBoxPanelOrWorkstation { get; private set; }

        /// <summary>
        /// Gets the signal box type
        /// </summary>
        public SignalBoxType Type { get; private set; }

        /// <summary>
        /// Gets the code associated with this signal box
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Instantiates a new <see cref="PointOrSwitchHistory"/>
        /// </summary>
        /// <param name="id">The parent signal <see cref="PointOrSwitchId"/></param>
        /// <param name="startDate">The signal block start date</param>
        /// <param name="endDate">The signal block end date (pass null if still current)</param>
        /// <param name="panelOrWorkstation">The signal box panel or workstation this point or switch belongs to</param>
        public PointOrSwitchHistory(PointOrSwitchId id, DateTime startDate, DateTime? endDate, ISignalBoxPanelOrWorkstation panelOrWorkstation) : base(id, startDate, endDate) 
        {
            if (panelOrWorkstation is null)
            {
                throw new ArgumentNullException(nameof(panelOrWorkstation), "You must supply the signal box panel or workstation this point or switch belongs to");
            }

            if (!panelOrWorkstation.HistoryOverlapPeriod(startDate, endDate))
            {
                throw new ArgumentException("The supplied signal box panel or workstation doesn't have a history which overlaps the supplied start and end date.");
            }

            this.SignalBoxPanelOrWorkstation = panelOrWorkstation;
            this._signalBoxPanelOrWorkstationId = panelOrWorkstation.Id;
        }
    }
}
