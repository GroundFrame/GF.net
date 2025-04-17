using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using GF.Common;

namespace GF.Graph
{
    public interface INode<TNodeID, TNeighbourPropertyKey>
    {
        /// <summary>
        /// Gets the node id
        /// </summary>
        [JsonProperty("id")]
        public TNodeID Id { get; }

        /// <summary>
        /// Gets the coordinates of the node.
        /// </summary>
        public ISpatial Coordinates { get; }

        /// <summary>
        /// Gets the name of the node
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the collection of neighbours of this node.
        /// </summary>
        [JsonProperty("neighbours")]
        public IEnumerable<INeighbour<TNodeID, TNeighbourPropertyKey>> Neighbours { get;  }

        /// <summary>
        /// Adds a directed neighbour of this node
        /// </summary>
        /// <param name="neighbour">The node which is to be added as directed neighbour of this node</param>
        public void AddDirectedNeighbour(INeighbour<TNodeID, TNeighbourPropertyKey> neighbour);
    }
}
