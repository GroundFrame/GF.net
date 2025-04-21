using GF.Common;
using GF.Graph;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Network
{
    public interface ITimingPoint
    {
        /// <summary>
        /// Gets the place Id
        /// </summary>
        public TimingPointId Id { get; }

        /// <summary>
        /// Gets the place name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the place key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the timing point history
        /// </summary>
        public IEnumerable<TimingPointHistory> History { get; }

        public IEnumerable<SignalBlockId> AssociatedSignalBlockIds { get; }

        public ISpatial Coordinates { get; }

        /// <summary>
        /// Associates a signal block to this timing point
        /// </summary>
        /// <param name="signalBlock">The <see cref="SignalBlock"/> to associate</param>
        public void AddAssociatedSignalBlock(SignalBlock signalBlock);
        /// <summary>
        /// Removes a signal block to this timing point
        /// </summary>
        /// <param name="signalBlock">The <see cref="SignalBlock"/> to remove</param>
        public void RemoveAssociatedSignalBlock(SignalBlock signalBlock);

        /// <summary>
        /// Adds a new history item to this place.
        /// </summary>
        /// <param name="history">The history item to add</param>
        public void AddHistory(TimingPointHistory history);

        /// <summary>
        /// Adds a new history item to this timing point.
        /// </summary>
        /// <param name="startDate">The start date of the history item</param>
        /// <param name="type">The timing point type</param>
        /// <param name="businessUnit">The business unit with responsbility for this timing point at the <paramref name="startDate"/></param>
        /// <param name="place">The place the timing point is located at the <paramref name="startDate"/>. If not located in a place then pass null</param>
        public void AddHistory(DateTime startDate, TimingPointType type, IBusinessUnit businessUnit, Place place = null);

        /// <summary>
        /// Removes the history item that covered by the start and end date.
        /// </summary>
        /// <param name="startDate">The start date of the history item to remove</param>
        /// <param name="endDate">The end date of the history item to remove</param>
        public void RemoveHistory(DateTime startDate, DateTime? endDate);

        /// <summary>
        /// Removes the history items the fall on the supplied <see cref="DateTime"/>
        /// </summary>
        /// <param name="date">The date of history items to remove</param>
        public void RemoveHistory(DateTime date);

        /// <summary>
        /// Replaces the history with a new history.
        /// </summary>
        /// <param name="newHistory"></param>
        public void ReplaceHistory(IEnumerable<TimingPointHistory> newHistory);

        /// <summary>
        /// Gets the start date of this temporal object. Returns null if no history found
        /// </summary>
        /// <returns><see cref="DateTime"/></returns>
        public DateTime? GetStartDate();

        /// <summary>
        /// Gets the end date of this temporal object. If null is return the temporal object is still active
        /// </summary>
        /// <returns><see cref="DateTime?"/></returns>
        public DateTime? GetEndDate();

        /// <summary>
        /// Gets the history item for the specified date
        /// </summary>
        /// <param name="date">The date of the history item to return</param>
        /// <returns><see cref="THistory"/> the history item</returns>
        public TimingPointHistory GetHistory(DateTime date);

        /// <summary>
        /// Adds a directed neighbour to this timing point
        /// </summary>
        /// <param name="neighbour">The directed neighbour</param>
        public void AddDirectedNeighbour(INeighbour<TimingPointId, TimingPointNeighbourProperty> neighbour);

        /// <summary>
        /// Adds a undirected neighbour to this timing point
        /// </summary>
        /// <param name="neighbour">The undirected <see cref="TimingPointHistory"/></param>
        public void AddUndirectedNeighbour(INeighbour<TimingPointId, TimingPointNeighbourProperty> neighbour, Graph<TimingPointId, TimingPointNeighbourProperty, Length, YMDV> graph);
        /// <summary>
        /// Calculates the direct distance between this node and the supplied node.
        /// </summary>
        /// <param name="node">The node to calculate the distance to</param>
        /// <returns><see cref="Length"/> representing the distance between the two nodes.</returns>
        public Length DistanceTo(INode<TimingPointId, TimingPointNeighbourProperty> node);

        /// <summary>
        /// Gets the signal blocks assoicated with this timing point
        /// </summary>
        /// <param name="network">The source <see cref="Railway"/></param>
        /// <returns></returns>
        public IEnumerable<SignalBlock> GetAssociatedSignalBlocks(Railway network);

        /// <summary>
        /// Gets the platforms assoicated with this timing point
        /// </summary>
        /// <param name="network">The source <see cref="Railway"/></param>
        /// <returns></returns>
        public IEnumerable<SignalBlock> GetAssociatedPlatforms(Railway network);
    }
}
