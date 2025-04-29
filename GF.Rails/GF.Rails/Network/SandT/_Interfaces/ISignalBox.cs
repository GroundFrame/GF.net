using GF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public interface ISignalBox : ITemporal<SignalBoxId, SignalBoxHistory>
    {
        /// <summary>
        /// Gets the coordinates of the signal box
        /// </summary>
        public ISpatial Coordinates { get; }

        /// <summary>
        /// Gets the panel or workstations associated with this signal box history
        /// </summary>
        public IEnumerable<ISignalBoxPanelOrWorkstation> PanelOrWorkstations { get; }
    }
}
