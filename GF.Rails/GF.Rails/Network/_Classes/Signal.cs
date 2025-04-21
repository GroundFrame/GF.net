using GF.Common;
using GF.Graph;
using GF.Rails.Network;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network
{
    public class Signal : TemporalBase<SignalId, SignalHistory>, ITemporal<SignalId, SignalHistory>
    {
        public ISpatial Coordinates { get; private set; }

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

        public Signal(string number, ISpatial coordinates, DateTime startDate, SignallingType type, ISignalBoxPanelOrWorkstation panelWorkstation) : this(number, coordinates)
        {
            this.AddHistory(new SignalHistory(this.Id, startDate, null, type, panelWorkstation));
        }

        #endregion Constructors
    }
}
