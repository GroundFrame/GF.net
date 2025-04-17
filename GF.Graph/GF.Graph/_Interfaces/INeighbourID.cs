using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Graph
{
    public interface INeighbourID<TNodeId>
    {
        public TNodeId ToNodeId { get; }
    }
}
