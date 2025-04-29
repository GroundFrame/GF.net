using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public class SignalBoxPanalOrWorkstationConfig
    {
        #region Fields

        private readonly List<SignalBoxPanalOrWorkstationItem> _panelItems;

        #endregion Fields

        /// <summary>
        /// Gets the signal box or workstation
        /// </summary>
        public ISignalBoxPanelOrWorkstation SignalBoxPanelOrWorkstation { get; set; }

        public IEnumerable<SignalBoxPanalOrWorkstationItem> PanelItems { get {  return _panelItems; } }

        #region Constructors

        public SignalBoxPanalOrWorkstationConfig(SignalBoxPanelOrWorkstation signalBoxPanelOrWorkstation)
        {
            this._panelItems = new List<SignalBoxPanalOrWorkstationItem>();
            this.SignalBoxPanelOrWorkstation = signalBoxPanelOrWorkstation;
        }

        #endregion Constructors

        #region Methods

        public void AddPanelItem(DateTime startDate, DateTime? endDate, ISignalBoxPanelOrWorkstationItem signalBoxPanelOrWorkstationItem)
        {
            this._panelItems.Add(new SignalBoxPanalOrWorkstationItem(startDate, endDate, signalBoxPanelOrWorkstationItem));
        }

        #endregion Methods

        public IEnumerable<TPanelItem> GetPanelItems<TPanelItem>(DateTime date) where TPanelItem : ISignalBoxPanelOrWorkstationItem
        {
            return this._panelItems.Where(pi => pi.PanelItem is TPanelItem && pi.Period.ContainsDate(date)).Select(i => (TPanelItem)i.PanelItem);
        }

        public void RemovePanelItem(ISignalBoxPanelOrWorkstationItem panelItem)
        {
            int existingId = this._panelItems.FindIndex(pi => pi.PanelItem.PanelItemId == panelItem.PanelItemId && pi.PanelItem.GetType().Equals(panelItem.GetType()));

            if (existingId != -1)
            {
                this._panelItems.RemoveAt(existingId);
            }
        }

    }
}
