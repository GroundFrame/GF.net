using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Graph
{
    public struct PathNode<TNodeId, TCost>
    {
        public TNodeId NodeId { get; }

        public TCost Cost { get; }

        public PathNode(TNodeId nodeId, TCost cost)
        {
            this.NodeId = nodeId;
            this.Cost = cost;
        }
    }
}
