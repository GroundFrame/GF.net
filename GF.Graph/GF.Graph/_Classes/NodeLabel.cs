using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Graph
{
    public struct NodeLabel<TNodeId>
    {
        public TNodeId Id { get; }

        public string Label { get; }

        public NodeLabel (TNodeId id, string label)
        {
            this.Id = id;
            this.Label = label;
        }
    }
}
