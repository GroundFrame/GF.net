using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public interface ISignalBoxPanelOrWorkstation
    {
        /// <summary>
        /// Gets the signal box panel or workstation id
        /// </summary>

        public SignalBoxPanelOrWorkstationId Id { get; }

        /// <summary>
        /// Gets the signal box panel or workstation period
        /// </summary>
        public TemporalPeriod Period { get; }

        /// <summary>
        /// Gets the signal box this panel or workstation period belongs to
        /// </summary>
        public ISignalBox SignalBox { get; }

        /// <summary>
        /// Gets the signal box panel or workstation name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the signal box panel or workstation code
        /// </summary>
        public string Code { get; }

        public void AddPanelItem(DateTime startDate, ISignalBoxPanelOrWorkstationItem panelOrWorkstationItem);

        public IEnumerable<TPanelItem> GetPanelItems<TPanelItem>(DateTime date) where TPanelItem : ISignalBoxPanelOrWorkstationItem;

        public void RemovePanelItem(ISignalBoxPanelOrWorkstationItem panelItem);
    }
}
