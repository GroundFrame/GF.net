using GF.Common;
using GF.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    public class SignalHistory : TemporalHistoryBase<SignalId>, ITemporalHistory<SignalId>
    {
        /// <summary>
        /// Instantiates a new <see cref="SignalHistory"/>
        /// </summary>
        /// <param name="id">The parent signal <see cref="SignalId"/></param>
        /// <param name="startDate">The signal block start date</param>
        /// <param name="endDate">The signal block end date (pass null if still current)</param>
        public SignalHistory(SignalId id, DateTime startDate, DateTime? endDate) : base(id, startDate, endDate) { }


    }
}
