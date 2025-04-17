using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Graph
{
    public class Path<TNodeId, TCost>
    {
        private List<PathNode<TNodeId, TCost>> _nodes;

        public IEnumerable<PathNode<TNodeId, TCost>> Nodes { get { return this._nodes; } }

        /// <summary>
        /// Gets the number of nodes in the path
        /// </summary>
        public int Count { get { return this._nodes.Count; } }

        public PathNode<TNodeId, TCost> this[int index]
        {
            get => this._nodes[index];
            set => this._nodes[index] = value;
        }

        public Path()
        {
            this._nodes = new List<PathNode<TNodeId, TCost>>();
        }

        public void AddNode(TNodeId nodeId, TCost cost)
        {
            this._nodes.Add(new PathNode<TNodeId, TCost>(nodeId, cost));
        }

        public override string ToString()
        {
            if (this.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                string result = "";
                this._nodes.ForEach(n => result += $"{n.NodeId},");
                return result.Substring(0, result.Length - 1);
            }
        }

        public string ToString(NodeLabels<TNodeId> labels)
        {
            if (this.Count == 0)
            {
                return "No path found.";
            }

            string result = "";
            this._nodes.ForEach(n => result += $"{labels.GetLabel(n.NodeId)} ({n.Cost})>");
            return result.Substring(0, result.Length-1);
        }
    }
}
