using GF.Rails.Network.SandT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network
{
    public class RailwayNetwork
    {
        private readonly List<ISignalBox> _signalBoxes = new List<ISignalBox>();

        public IEnumerable<ISignalBox> SignalBoxes { get { return this._signalBoxes; } }

        public RailwayNetwork()
        {
            
        }

        public void AddSignalBox(ISignalBox signalBox)
        {
            this._signalBoxes.Add(signalBox);
        }
    }
}
