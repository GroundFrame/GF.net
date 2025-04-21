using GF.Common;
using GF.Graph;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    public class SignalBoxPanelOrWorkstationHistory : TemporalHistoryBase<SignalBoxPanelOrWorkstationId>, ITemporalHistory<SignalBoxPanelOrWorkstationId>
    {
        /// <summary>
        /// Gets the code associated with this signal box
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Instantiates a new <see cref="SignalBoxHistory"/>
        /// </summary>
        /// <param name="id">The parent signal <see cref="SignalBoxId"/></param>
        /// <param name="startDate">The signal block start date</param>
        /// <param name="endDate">The signal block end date (pass null if still current)</param>
        /// <param name="code">The signal box code</param>
        public SignalBoxPanelOrWorkstationHistory(SignalBoxPanelOrWorkstationId id, DateTime startDate, DateTime? endDate, string code) : base(id, startDate, endDate) 
        {
            this.Code = code;
        }
    }
}
