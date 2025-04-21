using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network
{
    public interface ISignalBoxPanelOrWorkstation
    {
        /// <summary>
        /// Gets the signal box panel or workstation id
        /// </summary>

        public SignalBoxPanelOrWorkstationId Id { get; }

        /// <summary>
        /// Gets the signal box panel or workstation name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the signal box panel or workstation key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the signal box the panel or workstation belongs to
        /// </summary>
        public ISignalBox SignalBox { get; }

        /// <summary>
        /// Returns a flag to indicate that at least one history item overlaps the supppled period
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool HistoryOverlapPeriod(DateTime startDate, DateTime? endDate);
    }
}
