using GF.Common;
using GF.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    /// <summary>
    /// Class representing a signal block or track circuit
    /// </summary>
    public class SignalBlock : TemporalBase<SignalBlockId, SignalBlockHistory>, INode<SignalBlockId, SignalBlockNeighbourProperty>
    {
        #region Fields

        private HashSet<INeighbour<SignalBlockId, SignalBlockNeighbourProperty>> _neighbours;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the coordinates of the signal block.
        /// </summary>
        public ISpatial Coordinates { get; }

        /// <summary>
        /// Gets the appox length of the signal block
        /// </summary>
        public Length Length { get; }

        /// <summary>
        /// Gets the platform code of the signal block
        /// </summary>
        public string PlatformCode { get; }

        /// <summary>
        /// Gets the line code of the signal block
        /// </summary>
        public string LineCode { get; }

        /// <summary>
        /// Gets the collection of neighbours of the signal block
        /// </summary>
        public IEnumerable<INeighbour<SignalBlockId, SignalBlockNeighbourProperty>> Neighbours { get { return this._neighbours; } }


        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="Place"/>
        /// </summary>
        /// <param name="name">The place name</param>
        /// <param name="key">The place key</param>
        public SignalBlock(string name, string key, ISpatial coordinates, Length length, string platformCode = null, string lineCode = null) : base (new SignalBlockId(Guid.NewGuid()), name, key)
        {
            this._neighbours = new HashSet<INeighbour<SignalBlockId, SignalBlockNeighbourProperty>>();
            this.Coordinates = coordinates;
            this.Length = length;
            this.PlatformCode = platformCode;
            this.LineCode = lineCode;
        }

        #endregion Constructors

        /// <summary>
        /// Adds a new history item to this place.
        /// </summary>
        /// <param name="startDate">The start date of the history item</param>
        /// <param name="name">The place name</param>
        /// <param name="type">The place type</param>
        /// <param name="region">The place region</param>
        public void AddHistory(DateTime startDate, Power power, byte ra, Speed averageSpeed, Gradient averageGradient, DirectionType gradientDirection, bool canControlTrain = false)
        {
            base.AddHistory(new SignalBlockHistory(this.Id, startDate, null, power, ra, averageSpeed, averageGradient, gradientDirection, canControlTrain));
        }

        /// <summary>
        /// Adds a directed neighbour to this signal block
        /// </summary>
        /// <param name="neighbour">The directed neighbour</param>
        public void AddDirectedNeighbour(INeighbour<SignalBlockId, SignalBlockNeighbourProperty> neighbour)
        {
            this._neighbours.Add(neighbour);
        }

        /// <summary>
        /// Adds a undirected neighbour to this signal block
        /// </summary>
        /// <param name="neighbour">The undirected <see cref="SignalBlockNeighbour"/></param>
        public void AddUndirectedNeighbour(INeighbour<SignalBlockId, SignalBlockNeighbourProperty> neighbour, Graph<SignalBlockId, SignalBlockNeighbourProperty, Length, YMDV> graph)
        {
            if (!graph.Nodes.Any(n => n.Id.Equals(neighbour.NeighbourID)))
            {
                throw new ArgumentException("The 'To Node' in the supplied neighbour doesn't exist the graph database");
            }

            //get the signal block neighbour
            var signalBlockNeighbour = (SignalBlockNeighbour)neighbour;

            //validate the neighbour has a direction of 'Both'
            var neighbourDirection = signalBlockNeighbour.GetProperty<Direction>(SignalBlockNeighbourProperty.Direction);

            if (neighbourDirection.Value != (DirectionType.Up | DirectionType.Down))
            {
                throw new ArgumentException("An undirectected SignalBlockNeighbour must have a direction of Up & Down");
            }

            //add neighbours
            this.AddDirectedNeighbour(neighbour);
            graph.Nodes.FirstOrDefault(n => n.Id.Equals(neighbour.NeighbourID)).AddDirectedNeighbour(new SignalBlockNeighbour(this, neighbourDirection.ReciprocalValue, signalBlockNeighbour.GetProperty<SignallingType>(SignalBlockNeighbourProperty.SignalType)));
        }

        /// <summary>
        /// Calculates the direct distance between this node and the supplied node.
        /// </summary>
        /// <param name="node">The node to calculate the distance to</param>
        /// <returns><see cref="Length"/> representing the distance between the two nodes.</returns>
        public Length DistanceTo(INode<SignalBlockId, SignalBlockNeighbourProperty> node)
        {
            return this.Coordinates.DistanceTo(node.Coordinates);
        }

        /// <summary>
        /// Gets the history item for the specified date
        /// </summary>
        /// <param name="date">The date of the history item to return</param>
        /// <returns><see cref="THistory"/> the history item</returns>
        public SignalBlockHistory GetHistory(DateTime date, Power power, byte RA)
        {
            return this.History.ToList().FirstOrDefault(h => h.Id.TemporalPeriod.ContainsDate(date) && (h.Power & power) == power & h.RA > RA);
        }
    }
}
