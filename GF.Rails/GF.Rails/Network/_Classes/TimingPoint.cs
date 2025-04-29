using GF.Common;
using GF.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using GF.Rails;
using GF.Rails.Operations;
using GF.Rails.Network.SandT;

namespace GF.Rails.Network
{
    /// <summary>
    /// Class representing a timing point
    /// </summary>
    public class TimingPoint : TemporalBase<TimingPointId, TimingPointHistory>, INode<TimingPointId, TimingPointNeighbourProperty>, ITimingPoint
    {
        #region Fields

        private HashSet<SignalBlockId> _associatedSignalBlockIds;
        private HashSet<INeighbour<TimingPointId, TimingPointNeighbourProperty>> _neighbours;

        #endregion Fields

        public IEnumerable<SignalBlockId> AssociatedSignalBlockIds { get { return this._associatedSignalBlockIds; } }

        public ISpatial Coordinates { get; }

        /// <summary>
        /// Gets the collection of neighbours of the timing point
        /// </summary>
        public IEnumerable<INeighbour<TimingPointId, TimingPointNeighbourProperty>> Neighbours { get { return this._neighbours; } }

        #region Properties

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="Plvace"/>
        /// </summary>
        /// <param name="name">The timing point name</param>
        /// <param name="key">The timing point key (TIPLOC)</param>
        public TimingPoint(string name, string key, double latitude, double longitude) : base (new TimingPointId(Guid.NewGuid()), name, key)
        {
            this.Coordinates = new Coordinates(latitude, longitude);
            this._neighbours = new HashSet<INeighbour<TimingPointId, TimingPointNeighbourProperty>>();
            this._associatedSignalBlockIds = new HashSet<SignalBlockId>();
        }

        public TimingPoint(string name, string key, double latitude, double longitude, DateTime startDate, TimingPointType type, IBusinessUnit region, Place place = null) : this(name, key, latitude, longitude)
        {
            this.AddHistory(startDate, type, region, place);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Associates a signal block to this timing point
        /// </summary>
        /// <param name="signalBlock">The <see cref="ISignalBox"/> to associate</param>
        public void AddAssociatedSignalBlock(ISignalBlock signalBlock)
        {
            this._associatedSignalBlockIds.Add(signalBlock.Id);
        }

        /// <summary>
        /// Removes a signal block to this timing point
        /// </summary>
        /// <param name="signalBlock">The <see cref="ISignalBlock"/> to remove</param>
        public void RemoveAssociatedSignalBlock(ISignalBlock signalBlock)
        {
            this._associatedSignalBlockIds.Remove(signalBlock.Id);
        }

        /// <summary>
        /// Adds a new history item to this timing point.
        /// </summary>
        /// <param name="startDate">The start date of the history item</param>
        /// <param name="type">The timing point type</param>
        /// <param name="businessUnit">The business unit which is responsible the timing point at the <paramref name="startDate"/></param>
        /// <param name="place">The place the timing point is located at the <paramref name="startDate"/>. If not located in a place then pass null</param>
        public void AddHistory(DateTime startDate, TimingPointType type, IBusinessUnit businessUnit, Place place = null)
        {
            base.AddHistory(new TimingPointHistory(this.Id, startDate, null, type, businessUnit, place));
        }

        /// <summary>
        /// Adds a directed neighbour to this timing point
        /// </summary>
        /// <param name="neighbour">The directed neighbour</param>
        public void AddDirectedNeighbour(INeighbour<TimingPointId, TimingPointNeighbourProperty> neighbour)
        {
            this._neighbours.Add(neighbour);
        }

        /// <summary>
        /// Adds a undirected neighbour to this timing point
        /// </summary>
        /// <param name="neighbour">The undirected <see cref="TimingPointHistory"/></param>
        public void AddUndirectedNeighbour(INeighbour<TimingPointId, TimingPointNeighbourProperty> neighbour, Graph<TimingPointId, TimingPointNeighbourProperty, Length, YMDV> graph)
        {
            if (!graph.Nodes.Any(n => n.Id.Equals(neighbour.NeighbourID)))
            {
                throw new ArgumentException("The 'To Node' in the supplied neighbour doesn't exist the graph database");
            }

            //get the signal block neighbour
            var timingBlockNeighbour = (TimingPointNeighbour)neighbour;

            //validate the neighbour has a direction of 'Both'
            var neighbourDirection = timingBlockNeighbour.GetProperty<Direction>(TimingPointNeighbourProperty.Direction);

            if (neighbourDirection.Value != (DirectionType.Up | DirectionType.Down))
            {
                throw new ArgumentException("An undirectected SignalBlockNeighbour must have a direction of Up & Down");
            }

            //add neighbours
            this.AddDirectedNeighbour(neighbour);
            graph.Nodes.FirstOrDefault(n => n.Id.Equals(neighbour.NeighbourID)).AddDirectedNeighbour(new TimingPointNeighbour(this, neighbourDirection.ReciprocalValue, timingBlockNeighbour.GetProperty<Length>(TimingPointNeighbourProperty.Distance), timingBlockNeighbour.TemporalId.TemporalPeriod.StartDate, timingBlockNeighbour.TemporalId.TemporalPeriod.EndDate));
        }

        /// <summary>
        /// Calculates the direct distance between this node and the supplied node.
        /// </summary>
        /// <param name="node">The node to calculate the distance to</param>
        /// <returns><see cref="Length"/> representing the distance between the two nodes.</returns>
        public Length DistanceTo(INode<TimingPointId, TimingPointNeighbourProperty> node)
        {
            return this.Coordinates.DistanceTo(node.Coordinates);
        }

        public IEnumerable<ISignalBlock> GetAssociatedSignalBlocks(Railway network)
        {
            throw new NotImplementedException();
            //return network.SignalBoxes.ToList().Where(sb => this._associatedSignalBlockIds.Any(asb => asb == sb.Id));
        }

        public IEnumerable<ISignalBlock> GetAssociatedPlatforms(Railway network)

        {
            return this.GetAssociatedSignalBlocks(network).Where(asb => !string.IsNullOrEmpty(asb.PlatformCode));
        }

        #endregion Methods
    }
}
