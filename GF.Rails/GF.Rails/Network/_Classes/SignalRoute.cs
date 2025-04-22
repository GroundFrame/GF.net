using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network
{
    public class SignalRoute
    {
        private readonly List<SignalBlock> _routeSignalBlocks;
       
        public TemporalPeriod Period { get; private set; }

        public ISignalBoxPanelOrWorkstation SignalBoxPanelOrWorkstation { get; private set; }

        public ISignal FromSignal { get; private set; }

        public ISignal ToSignal { get; private set; }

        public SignalRoute(DateTime startDate, DateTime? endDate, ISignalBoxPanelOrWorkstation panelOrWorkstation, ISignal fromSignal, ISignalBox toSignal)
        {

        }
    }
}
