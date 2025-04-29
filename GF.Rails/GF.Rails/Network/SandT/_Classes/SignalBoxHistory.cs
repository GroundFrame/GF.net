using GF.Common;
using GF.Graph;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network.SandT
{
    public class SignalBoxHistory : TemporalHistoryBase<SignalBoxId>, ITemporalHistory<SignalBoxId>
    {
        /// <summary>
        /// Gets the business unit the signal box is assigned to
        /// </summary>
        public IBusinessUnit BusinessUnit { get; private set; }

        /// <summary>
        /// Gets the signal box type
        /// </summary>
        public SignalBoxType Type { get; private set; }

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
        /// <param name="businessUnit">The business unit that signal box belongs to at this time</param>
        /// <param name="type">The signal box type</param>
        /// <param name="code">The signal box code</param>
        public SignalBoxHistory(SignalBoxId id, DateTime startDate, DateTime? endDate, IBusinessUnit businessUnit, SignalBoxType type, string code) : base(id, startDate, endDate) 
        {
            this.BusinessUnit = businessUnit;
            this.Type = type;
            this.Code = code;
        }
    }
}
