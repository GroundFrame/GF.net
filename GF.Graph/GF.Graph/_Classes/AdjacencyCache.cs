using GF.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GF.Graph
{
    /// <summary>
    /// Class representing a graph adjency cache. This caches the adjancency matrix for the supplied predicate.
    /// </summary>
    /// <typeparam name="TNodeId">The node Id type</typeparam>
    /// <typeparam name="TEdgePropertyKey">The edge property key type<typeparam>
    /// <typeparam name="TCost">The cost type</typeparam>
    /// <typeparam name="TAdjacencyCacheKey">The type of the adjacency cache key</typeparam>
    public class AdjacencyCache<TNodeId, TEdgePropertyKey, TCost, TAdjacencyCacheKey>
    {
        /// <summary>
        /// The adjanacy cache key
        /// </summary>
        public TAdjacencyCacheKey Key { get; }

        /// <summary>
        /// The expression used to build the adjacency cache.
        /// </summary>
        public Expression<Func<INode<TNodeId, TEdgePropertyKey>, bool>> Predicate { get; }

        /// <summary>
        /// The adjacency matrix
        /// </summary>
        public TCost[,] AdjacencyMatrix { get; }

        /// <summary>
        /// Instantiates a new <see cref="AdjacencyCache{TNodeId, TEdgePropertyKey, TCost, TAdjacencyCacheKey}"/>
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="predicate">The predicate used to build the adjancency matrix</param>
        /// <param name="adjacencyMatrix"></param>
        public AdjacencyCache(TAdjacencyCacheKey key, Expression<Func<INode<TNodeId, TEdgePropertyKey>, bool>> predicate, TCost[,] adjacencyMatrix)
        {
            this.Predicate = predicate;
            this.AdjacencyMatrix = adjacencyMatrix;
            this.Key = key;
        }
    }
}
