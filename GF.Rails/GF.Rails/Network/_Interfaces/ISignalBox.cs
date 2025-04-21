using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network
{
    public interface ISignalBox
    {
        public SignalBoxId Id { get; }
        public string Name { get; }

        public string Key { get; }
    }
}
