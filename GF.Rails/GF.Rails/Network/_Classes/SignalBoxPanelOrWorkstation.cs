using GF.Common;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network
{
    public class SignalBoxPanelOrWorkstation : TemporalBase<SignalBoxPanelOrWorkstationId, SignalBoxPanelOrWorkstationHistory>, ITemporal<SignalBoxPanelOrWorkstationId, SignalBoxPanelOrWorkstationHistory>, ISignalBoxPanelOrWorkstation
    {
        #region Constructors

        /// <summary>
        /// Gets the signal box the panel or workstation belongs to
        /// </summary>
        public ISignalBox SignalBox { get; }

        /// <summary>
        /// Instantiates a new <see cref="SignalBoxPanelOrWorkstation"/>
        /// </summary>
        /// <param name="name">The signal box panel or workstation name</param>
        /// <param name="signalBox">The signal box this panel or workstation belongs to</param>
        public SignalBoxPanelOrWorkstation(string name, ISignalBox signalBox) : base(new SignalBoxPanelOrWorkstationId(Guid.NewGuid()), name) 
        { 
            this.SignalBox = signalBox;
        }

        /// <summary>
        /// Instantiates a new <see cref="SignalBoxPanelOrWorkstation"/>
        /// </summary>
        /// <param name="name">The signal box panel or workstation name</param>
        /// <param name="signalBox">The signal box this panel or workstation belongs to</param>
        /// <param name="startDate">The date the signal box panel or workstation came into operation</param>
        /// <param name="code">The signal box panel or workstation code</param>
        public SignalBoxPanelOrWorkstation(string name, ISignalBox signalBox, DateTime startDate, string code) : this(name, signalBox)
        {
            this.AddHistory(new SignalBoxPanelOrWorkstationHistory(this.Id, startDate, null, code));
        }

        #endregion Constructors
    }
}
