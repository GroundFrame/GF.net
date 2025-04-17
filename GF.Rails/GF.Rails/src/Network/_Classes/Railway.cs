using GF.Common;
using GF.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GF.Rails.Network
{
    public class Railway
    {
        [JsonProperty("regions")]
        private readonly List<BRRegion> _regions;
        private readonly List<Place> _places;
        private readonly List<SignalBlock> _signalBlocks;
        private readonly List<ITimingPoint> _timingPoints;
        private readonly List<Route> _routes;
        private readonly List<Token> _tokens;

        /// <summary>
        /// Gets the signal block graph
        /// </summary>
        public Graph<SignalBlockId, SignalBlockNeighbourProperty, Length, YMDV> SignalBlockGraph { get; }

        /// <summary>
        /// Gets the timing point graph
        /// </summary>
        public Graph<TimingPointId, TimingPointNeighbourProperty, Length, YMDV> TimingPointGraph { get; }

        /// <summary>
        /// Gets the collection of regions
        /// </summary>
        [JsonIgnore]
        public IEnumerable<BRRegion> Regions { get { return this._regions; } }

        /// <summary>
        /// Gets the collection places
        /// </summary>
        public IEnumerable<Place> Places { get { return this._places; } }

        /// <summary>
        /// Gets the collection of signal blocks
        /// </summary>
        public IEnumerable<SignalBlock> SignalBlocks { get { return this._signalBlocks; } }

        /// <summary>
        /// Gets the collection of timing points
        /// </summary>
        public IEnumerable<ITimingPoint> TimingPoints { get { return this._timingPoints; } }

        /// <summary>
        /// Gets the collection of routes
        /// </summary>
        public IEnumerable<Route> Routes { get { return this._routes; } }

        public Railway()
        {
            this._regions = new List<BRRegion>();
            this._places = new List<Place>();
            this._signalBlocks = new List<SignalBlock>();
            this._timingPoints = new List<ITimingPoint>();
            this._routes = new List<Route>();
            this._tokens = new List<Token>();

            //define signal block cost function
            static Length SignalBlockCostFunction(
                INode<SignalBlockId, SignalBlockNeighbourProperty> fromNode,
                INode<SignalBlockId, SignalBlockNeighbourProperty> toNode,
                object[] args
            )
            {
                return new Length(((fromNode as SignalBlock).Length.Meters / 2) + ((toNode as SignalBlock).Length.Meters / 2));

            }
            this.SignalBlockGraph = new Graph<SignalBlockId, SignalBlockNeighbourProperty, Length, YMDV>("Signal Block Graph", SignalBlockCostFunction);

            //define timing point cost function
            static Length TimingPointCostFunction(
                INode<TimingPointId, TimingPointNeighbourProperty> fromNode,
                INode<TimingPointId, TimingPointNeighbourProperty> toNode,
                object[] args //0 = The date funtion is being run against
            )
            {
                var timingPointNeighbours = new HashSet<TimingPointNeighbour>();

                foreach (var n in fromNode.Neighbours.Where(n => n.NeighbourID.Equals(toNode.Id)))
                {
                    timingPointNeighbours.Add((TimingPointNeighbour)n);
                }

                if (!timingPointNeighbours.Any(n => n.TemporalId.TemporalPeriod.ContainsDate((DateTime)args[0])))
                {
                    return Length.MinValue;
                }

                return timingPointNeighbours.FirstOrDefault(n => n.TemporalId.TemporalPeriod.ContainsDate((DateTime)args[0])).GetProperty<Length>(TimingPointNeighbourProperty.Distance);
            }

            this.TimingPointGraph = new Graph<TimingPointId, TimingPointNeighbourProperty, Length, YMDV>("Timing Point Graph", TimingPointCostFunction);
        }

        public void AddToken(Token token)
        {
            //check the name isn't already used
            if (this._tokens.Any(tk => tk.Name.Equals(token.Name, StringComparison.OrdinalIgnoreCase) && !tk.Id.Equals(token.Id)))
            {
                throw new ArgumentException($"The timing point with the name '{token.Name}' already exists in this network.");
            }

            //check the key isn't already used
            if (this._tokens.Any(tk => tk.Key.Equals(token.Key, StringComparison.OrdinalIgnoreCase) && !tk.Id.Equals(token.Id)))
            {
                throw new ArgumentException($"The timing point with the key '{token.Key}' already exists in this network.");
            }

            if (this._tokens.Any(tk => tk.Id.Equals(token.Id)))
            {
                int index = this._tokens.FindIndex(tk => tk.Id.Equals(token.Id));
                this._tokens[index] = token;
                return;
            }

            this._tokens.Add(token);
        }

        /// <summary>
        /// Gets the shortest route between two timing points on the date specified
        /// </summary>
        /// <param name="start">The start <see cref="TimingPoint"/></param>
        /// <param name="end">The end <see cref="TimingPoint"/></param>
        /// <param name="date">The <see cref="DateTime"/></param>
        /// <returns><see cref="Path{TimingPointId, Length}"/> containing the shortest path</returns>
        public Path<TimingPointId, Length> GetShortestRouteBetweenBetweenTimingPoints(ITimingPoint start, ITimingPoint end, DateTime date)
        {
            Expression<Func<INode<TimingPointId, TimingPointNeighbourProperty>, bool>> timingBlockExpression = n => (n as TimingPoint).GetHistory(date) != null;
            var adj  = this.TimingPointGraph.BuildAdjacencyMatrix(new object[] { date }, new YMDV(date), timingBlockExpression);
            return this.TimingPointGraph.Dijkstra(adj, (INode<TimingPointId, TimingPointNeighbourProperty>)start, (INode<TimingPointId, TimingPointNeighbourProperty>)end);
        }
            
        /// <summary>
        /// Gets all possible signal block paths between two timing points.
        /// </summary>
        /// <param name="start">The start <see cref="TimingPoint"/></param>
        /// <param name="end">The end <see cref="TimingPoint"/></param>
        /// <param name="date">The date</param>
        /// <param name="power">The power traversing the path</param>
        /// <param name="RA">The route availability required</param>
        /// <param name="startSignalBlock">An optional start <see cref="SignalBlock"/> if the path needs to start at a specific signal block. Must be associated to the <paramref name="start"/> timing block</param>
        /// <param name="endSignalBlock">An optional end <see cref="SignalBlock"/> if the path needs to start at a specific signal block. Must be associated to the <paramref name="start"/> timing block</param>
        /// <returns><see cref="IEnumerable{Path{SignalBlockId, Length}}"/></returns>
        /// <remarks>Multiple paths are returned because a timing point (such as a station) may have multiple signal blocks and therefore multiple paths may exist.</remarks>
        public IEnumerable<Path<SignalBlockId, Length>> GetSignalBlockShortestPathsBetweenTimingPoints(ITimingPoint start, ITimingPoint end, DateTime date, Power power, byte RA, SignalBlock startSignalBlock = null, SignalBlock endSignalBlock = null)
        {
            List<Path<SignalBlockId, Length>> result = new List<Path<SignalBlockId, Length>>();

            //caclulate the number of iterations we need to perform. If a start or end signal block supplied then, as long as the signal block is assicated with the timing pointing then we only need to iterate once otherwise we'll look for all possible paths
            int startSignalBlocksToIterate = 0;
            int endSignalBlocksToIterate = 0;

            if (startSignalBlock != null && start.AssociatedSignalBlockIds.Any(abs => abs == startSignalBlock.Id))
            {
                startSignalBlocksToIterate = 1;
            }
            else
            {
                startSignalBlocksToIterate = start.AssociatedSignalBlockIds.Count();
            }

            if (endSignalBlock != null && end.AssociatedSignalBlockIds.Any(abs => abs == endSignalBlock.Id))
            {
                endSignalBlocksToIterate = 1;
            }
            else
            {
                endSignalBlocksToIterate = end.AssociatedSignalBlockIds.Count();
            }

            //iterate over all possible start and end blocks and calculate paths
            for (int s = 0; s < startSignalBlocksToIterate; s++)
            {
                for (int e = 0; e < endSignalBlocksToIterate; e++)
                {
                    //calculate the start and end blocks
                    SignalBlock startSignalBlockNode = null;
                    SignalBlock endSignalBlockNode = null;

                    if (startSignalBlock != null && start.AssociatedSignalBlockIds.Any(abs => abs == startSignalBlock.Id))
                    {
                        startSignalBlockNode = startSignalBlock;
                    }
                    else
                    {
                        startSignalBlockNode = (SignalBlock)this.SignalBlockGraph.Nodes.FirstOrDefault(n => n.Id == start.AssociatedSignalBlockIds.ToList()[s]);
                    }


                    if (endSignalBlock != null && end.AssociatedSignalBlockIds.Any(abs => abs == endSignalBlock.Id))
                    {
                        endSignalBlockNode = endSignalBlock;
                    }
                    else
                    {
                        endSignalBlockNode = (SignalBlock)this.SignalBlockGraph.Nodes.FirstOrDefault(n => n.Id == end.AssociatedSignalBlockIds.ToList()[s]);
                    }

                    Expression<Func<INode<SignalBlockId, SignalBlockNeighbourProperty>, bool>> signalBlockExpression = n => (n as SignalBlock).GetHistory(date, power, RA) != null;
                    var adjSignal = this.SignalBlockGraph.BuildAdjacencyMatrix(new object[] { date }, new YMDV(date), signalBlockExpression);
                    var dijkstraSignal = this.SignalBlockGraph.Dijkstra(adjSignal, startSignalBlockNode, endSignalBlockNode);
                    result.Add(dijkstraSignal);
                }
            }

            return result;
        }

        public Route GetRouteByKey(string key)
        {
            int index = this._routes.FindIndex(rt => rt.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

            if (index == -1)
            {
                return null;
            }

            return this._routes[index];
        }

        public Route AddRoute(string name, string key, DateTime startDate, DirectionType direction, params RouteTimingPoint[] routeTimingPoints)
        {
            var route = new Route(name, key, startDate, direction, routeTimingPoints);
            this.AddRoute(route);
            return route;
        }

        public void AddRoute(Route route)
        {
            //check the name isn't already used
            if (this._routes.Any(rt => rt.Name.Equals(route.Name, StringComparison.OrdinalIgnoreCase) && rt.Id != route.Id))
            {
                throw new ArgumentException($"The timing point with the name '{route.Name}' already exists in this network.");
            }

            //check the key isn't already used
            if (this._routes.Any(rt => rt.Key.Equals(route.Key, StringComparison.OrdinalIgnoreCase) && rt.Id != route.Id))
            {
                throw new ArgumentException($"The timing point with the key '{route.Key}' already exists in this network.");
            }

            if (this._routes.Any(rt => rt.Id.Equals(route.Id)))
            {
                int index = this._routes.FindIndex(rt => rt.Id.Equals(route.Id));
                this._routes[index] = route;
                return;
            }

            this._routes.Add(route);
        }

        public ITimingPoint GetTimingPointByKey(string key)
        {
            int index = this._timingPoints.FindIndex(tp => tp.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

            if (index == -1)
            {
                return null;
            }

            return this._timingPoints[index];
        }

        /// <summary>
        /// Returns a list of timing point with the supplied Ids in the order the supplied Ids.
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public IEnumerable<ITimingPoint> GetTimingPointsFromRouteTimingPointsIds(IEnumerable<RouteTimingPoint> Ids)
        {
            var timingPoints =  this.TimingPoints.Where(tp => Ids.Select(rpt => rpt.TimingPointId).Contains(tp.Id));
            return timingPoints.OrderBy(tp => Ids.ToList().FindIndex(id => id.TimingPointId.Equals(tp.Id)));
        }

        /// <summary>
        /// Returns a list of timing point histories for the supplied date and timing points Ids ordered in the same was as the supplied timing points Ids.
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="date">The date of the history item to return</param>
        /// <returns></returns>
        public IEnumerable<TimingPointHistory> GetTimingPointHistoryFromRouteTimingPointIds(IEnumerable<RouteTimingPoint> Ids, DateTime date)
        {
            var timingPoints = this.TimingPoints.Where(tp => Ids.Select(rpt => rpt.TimingPointId).Contains(tp.Id));
            var orderedTimingPoints = timingPoints.OrderBy(tp => Ids.ToList().FindIndex(id => id.TimingPointId.Equals(tp.Id)));
            return orderedTimingPoints.Select(otp => otp.GetHistory(date));
        }

        public TimingPoint AddTimingPoint(string name, string key, double latitude, double longitude, DateTime startDate, TimingPointType type, BRRegion region, Place place = null)
        {
            var timingPoint = new TimingPoint(name, key, latitude, longitude, startDate, type, region, place);
            this.AddTimingPoint(timingPoint);
            return timingPoint;
        }

        public void AddTimingPoint(TimingPoint timingPoint)
        {
            //check the name isn't already used
            if (this._timingPoints.Any(tp => tp.Name.Equals(timingPoint.Name, StringComparison.OrdinalIgnoreCase) && tp.Id != timingPoint.Id))
            {
                throw new ArgumentException($"The timing point with the name '{timingPoint.Name}' already exists in this network.");
            }

            //check the key isn't already used
            if (this._timingPoints.Any(tp => tp.Key.Equals(timingPoint.Key, StringComparison.OrdinalIgnoreCase) && tp.Id != timingPoint.Id))
            {
                throw new ArgumentException($"The timing point with the key '{timingPoint.Key}' already exists in this network.");
            }

            if (this._timingPoints.Any(tp => tp.Id.Equals(timingPoint.Id)))
            {
                int index = this._timingPoints.FindIndex(tp => tp.Id.Equals(timingPoint.Id));
                this._timingPoints[index] = timingPoint;
                return;
            }

            this._timingPoints.Add(timingPoint);
            this.TimingPointGraph.AddNode(timingPoint);
        }

        public void AddSignalBlock(SignalBlock signalBlock)
        {
            if (this._signalBlocks.Any(sb => sb.Id.Equals(signalBlock.Id)))
            {
                int index = this._regions.FindIndex(sb => sb.Id.Equals(signalBlock.Id));
                this._signalBlocks[index] = signalBlock;
                return;
            }

            this._signalBlocks.Add(signalBlock);
            this.SignalBlockGraph.AddNode(signalBlock);
        }

        public void AddRegion(BRRegion region)
        {
            if (this._regions.Any(r => r.Id.Equals(region.Id)))
            {
                int index = this._regions.FindIndex(r => r.Id.Equals(region.Id));
                this._regions[index] = region;
                return;
            }

            this._regions.Add(region);
        }

        public Place AddPlace(string name, string key, DateTime startDate, PlaceType type, BRRegion region)
        {
            var place = new Place(name, key, startDate, type, region);
            this.AddPlace(place);
            return place;
        }

        public Place GetPlaceByKey(string key)
        {
            int index = this._places.FindIndex(tp => tp.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

            if (index == -1)
            {
                return null;
            }

            return this._places[index];
        }

        public void AddPlace(Place place)
        {
            //check the name isn't already used
            if (this._places.Any(p => p.Name.Equals(place.Name, StringComparison.OrdinalIgnoreCase) && p.Id != place.Id))
            {
                throw new ArgumentException($"The place with the name '{place.Name}' already exists in this network.");
            }

            //check the key isn't already used
            if (this._places.Any(p => p.Key.Equals(place.Key, StringComparison.OrdinalIgnoreCase) && p.Id != place.Id))
            {
                throw new ArgumentException($"The place with the key '{place.Key}' already exists in this network.");
            }

            if (this._places.Any(p => p.Id.Equals(place.Id)))
            {
                int index = this._regions.FindIndex(p => p.Id.Equals(place.Id));
                this._places[index] = place;
                return;
            }

            this._places.Add(place);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
