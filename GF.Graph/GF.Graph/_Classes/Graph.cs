using GF.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace GF.Graph
{
    public class Graph<TNodeId, TEdgePropertyKey, TCost, TAdjacencyCacheKey> where TCost : IComparable<TCost>, IGraphCost<TCost>
    {
        #region Fields

        private readonly List<INode<TNodeId, TEdgePropertyKey>> _nodes; //stores the collection of nodes in the graph
        private readonly List<AdjacencyCache<TNodeId, TEdgePropertyKey, TCost, TAdjacencyCacheKey>> _adjacencyCache; //stores the adjacency cache
        private readonly NodeLabels<TNodeId> _labels; //stores the collection of node labels

        #endregion Fields

        /// <summary>
        /// Gets the name of the graph
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the function used to calculate the cost of traversing between 2 nodes.
        /// </summary>
        [JsonIgnore]
        public Func<INode<TNodeId, TEdgePropertyKey>, INode<TNodeId, TEdgePropertyKey>, object[], TCost> CostFunction { get; }

        /// <summary>
        /// Gets the node collection
        /// </summary>
        public IEnumerable<INode<TNodeId, TEdgePropertyKey>> Nodes { get { return this._nodes; } }

        /// <summary>
        /// Gets the node labels
        /// </summary>
        public NodeLabels<TNodeId> Labels { get { return this._labels; } }
        
        /// <summary>
        /// Instantiates a new <see cref="Graph{TNodeId, TEdgePropertyKey, TCost, TAdjacencyCacheKey}"/>
        /// </summary>
        /// <param name="name">The graph name</param>
        /// <param name="costFunction">The cost function</param>
        public Graph(string name, Func<INode<TNodeId, TEdgePropertyKey>, INode<TNodeId, TEdgePropertyKey>, object[], TCost> costFunction)
        {
            this.Name = name;
            this.CostFunction = costFunction;
            this._nodes = [];
            this._adjacencyCache = [];
            this._labels = new NodeLabels<TNodeId>();
        }

        /// <summary>
        /// Adds a node to the node collection. If a node already exists then the node is updated
        /// </summary>
        /// <param name="node">The node to add to the graph</param>
        public void AddNode(INode<TNodeId, TEdgePropertyKey> node)
        {
            int index = this._nodes.FindIndex(n => n.Id.Equals(node.Id));

            if (index == -1)
            {
                this._nodes.Add(node);
            }
            else
            {
                this._nodes[index] = node;
            }

            //add / update node label
            this._labels.AddLabel(node.Id, node.Name);
        }

        /// <summary>
        /// Adds the adjacency matrix to the cache
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="filterExpression">The node filter expression</param>
        /// <param name="adjacencyMatrix">The adjacency matrix</param>
        private void CacheAdjacencyMatrix(TAdjacencyCacheKey key, Expression<Func<INode<TNodeId, TEdgePropertyKey>, bool>> filterExpression, TCost[,] adjacencyMatrix)
        {
            this._adjacencyCache.Add(new AdjacencyCache<TNodeId, TEdgePropertyKey, TCost, TAdjacencyCacheKey>(key, filterExpression, adjacencyMatrix));
        }

        /// <summary>
        /// Builds an adjacency matrix for the supplied predicate
        /// </summary>
        /// <returns></returns>
        public TCost[,] BuildAdjacencyMatrix(object[] costFunctionArgs, TAdjacencyCacheKey key, Expression<Func<INode<TNodeId, TEdgePropertyKey>, bool>> predicate)
        {
            Func<INode<TNodeId, TEdgePropertyKey>, bool> compiledFunction = null;
            bool cacheAdjacencyMatrix = false;
            
            if (this._adjacencyCache.Any(ac => ac.Key.Equals(key) && Helpers.FuncTest.FuncEqual(ac.Predicate,predicate)))
            {
                Debug.WriteLine("Built from cache");
                compiledFunction = this._adjacencyCache.FirstOrDefault(ac => ac.Key.Equals(key) && Helpers.FuncTest.FuncEqual(ac.Predicate, predicate)).Predicate.Compile();
            }
            else
            {
                compiledFunction = predicate.Compile();
                cacheAdjacencyMatrix = true;
            }

            TCost[,] adj = new TCost[this.Nodes.Count(), this.Nodes.Count()];

            var filteredNodes = this.Nodes.Where(compiledFunction).ToList();

            for (int i = 0; i < this.Nodes.Count(); i++)
            {
                var fromNodeFilteredIndex = filteredNodes.FindIndex(fn => fn.Id.Equals(this.Nodes.ToArray()[i].Id));

                if (fromNodeFilteredIndex == -1)
                {
                    for (int j = 0; j < this.Nodes.Count(); j++)
                    {
                        adj[i, j] = default;
                    }
                }
                else
                {
                    var node1 = filteredNodes[fromNodeFilteredIndex];
                    for (int j = 0; j < this.Nodes.Count(); j++)
                    {
                        var toNodeFilteredIndex = filteredNodes.FindIndex(fn => fn.Id.Equals(this.Nodes.ToArray()[j].Id));

                        if (toNodeFilteredIndex == -1)
                        {
                            adj[i, j] = default;
                        }
                        else
                        {
                            var node2 = filteredNodes[toNodeFilteredIndex];
                            var edge = node1.Neighbours.FirstOrDefault(a => a.NeighbourID.Equals(node2.Id));
                            if (edge != null)
                            {
                                adj[i, j] = this.CostFunction(node1, node2, costFunctionArgs);
                            }
                            else
                            {
                                adj[i, j] = default;
                            }
                        }
                    }
                }
            }


            if (cacheAdjacencyMatrix)
            {
                this.CacheAdjacencyMatrix(key, predicate, adj);
            }

            return adj;
        }

        /// <summary>
        /// Gets the minium cost value for type of <see cref="TCost"/>.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="tset"></param>
        /// <returns></returns>
        private static int GetMinimumCost(TCost[] distance, bool[] tset)
        {
            var maxValue = (TCost)typeof(TCost).GetProperty("MaxValue").GetValue(null);

            TCost minimum = maxValue;
            int index = 0;
            for (int k = 0; k < distance.Length; k++)
            {
                if (!tset[k] && distance[k].CompareTo(minimum) <= 0)
                {
                    minimum = distance[k];
                    index = k;
                }
            }
            return index;
        }

        /// <summary>
        /// Returns the <see cref="PathNode{TNodeId, TCost}"/> of the short distance calculated by the graph cost function between two nodes in the supplied adjacency matrix
        /// </summary>
        /// <param name="adj">The grpah adjancency matrix</param>
        /// <param name="from">The start <see cref="INode{TNodeID, TNeighbourPropertyKey}"/></param>
        /// <param name="to">The end <see cref="INode{TNodeID, TNeighbourPropertyKey}"/></param>
        /// <returns><see cref="PathNode{TNodeId, TCost}"/> containing the nodes that makes up the shortest path</returns>
        public Path<TNodeId, TCost> Dijkstra(TCost[,] adj, INode<TNodeId, TEdgePropertyKey> from, INode<TNodeId, TEdgePropertyKey> to)
        {
            int length = adj.GetLength(0);

            if (length == 0)
            {
                return new Path<TNodeId, TCost>();
            }

            TCost[] distance = new TCost[length];
            bool[] used = new bool[length];
            int[] prev = new int[length];

            var maxValue = (TCost)typeof(TCost).GetProperty("MaxValue").GetValue(null);
            var minValue = (TCost)typeof(TCost).GetProperty("MinValue").GetValue(null);

            for (int i = 0; i < length; i++)
            {
                distance[i] = maxValue;
                used[i] = false;
                prev[i] = -1;
            }
            distance[this._nodes.IndexOf(from)] = (TCost)default;

            for (int k = 0; k < length - 1; k++)
            {
                int minNode = GetMinimumCost(distance, used);
                used[minNode] = true;
                for (int i = 0; i < length; i++)
                {   
                    if (adj[minNode, i].CompareTo(minValue) > 0)
                    {
                        TCost shortestToMinNode = distance[minNode];
                        TCost distanceToNextNode = adj[minNode, i];
                        TCost totalDistance = shortestToMinNode.Add(distanceToNextNode);
                        if (totalDistance.CompareTo(distance[i]) < 0)
                        {
                            distance[i] = totalDistance;
                            prev[i] = minNode;
                        }
                    }
                }
            }
            if (distance[this._nodes.IndexOf(to)].CompareTo(maxValue) == 0)
            {
                return new Path<TNodeId, TCost>();
            }

            var path = new LinkedList<int>();
            int currentNode = this._nodes.IndexOf(to);
            while (currentNode != -1)
            {
                path.AddFirst(currentNode);
                currentNode = prev[currentNode];
            }

            Path<TNodeId, TCost> result = new Path<TNodeId, TCost>();

            foreach(int index in path)
            {
                result.AddNode(this._nodes[index].Id, distance[index]);
            }

            return result;
        }

        public string SerializeGraph()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
