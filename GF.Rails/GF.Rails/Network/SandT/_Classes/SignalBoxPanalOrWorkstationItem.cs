using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public class SignalBoxPanalOrWorkstationItem
    {
        public TemporalPeriod Period { private set; get; }

        public ISignalBoxPanelOrWorkstationItem PanelItem { private set; get; }

        public SignalBoxPanalOrWorkstationItem(DateTime startDate, DateTime? endDate, ISignalBoxPanelOrWorkstationItem panelItem)
        {
            this.Period = new TemporalPeriod(startDate, endDate);
            this.PanelItem = panelItem;
        }
    }
}
