using GF.Common;
using GF.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    public class SignalBoxHistory : TemporalHistoryBase<SignalBoxId>, ITemporalHistory<SignalBoxId>
    {
        /// <summary>
        /// Instantiates a new <see cref="SignalBoxHistory"/>
        /// </summary>
        /// <param name="id">The parent signal <see cref="SignalBoxId"/></param>
        /// <param name="startDate">The signal block start date</param>
        /// <param name="endDate">The signal block end date (pass null if still current)</param>
        public SignalBoxHistory(SignalBoxId id, DateTime startDate, DateTime? endDate) : base(id, startDate, endDate) { }


    }
}
