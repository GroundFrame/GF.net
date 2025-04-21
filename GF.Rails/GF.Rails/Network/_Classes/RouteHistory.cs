using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    public class RouteHistory : TemporalHistoryBase<RouteId>, ITemporalHistory<RouteId>
    {
        private SortedDictionary<int, RouteTimingPoint> _timingPoints { get; }

        private DirectionType _direction { get; }

        public RouteHistory(RouteId routeId, DateTime startDate, DateTime? endDate, DirectionType direction, params RouteTimingPoint[] routeTimingPoints) : base (routeId, startDate, endDate)
        {
            this._timingPoints = new SortedDictionary<int, RouteTimingPoint>();
            this._direction = direction;

            for (int i = 0; i < routeTimingPoints.Length; i++)
            {
                this._timingPoints.Add(i, routeTimingPoints[i]);
            }
        }

        public IEnumerable<RouteTimingPoint> GetTimingPoints(DirectionType direction)
        {
            if (direction.Equals(this._direction))
            {
                return this._timingPoints.Select(tp => tp.Value);
            }

            return this._timingPoints.Reverse().Select(tp => tp.Value);
        }

        public void AddTimingPoint(RouteTimingPoint routeTimingPoint, int ordinal)
        {
            var keyExists = this._timingPoints.TryGetValue(ordinal, out RouteTimingPoint value);

            if (!keyExists)
            {
                this._timingPoints.Add(ordinal, routeTimingPoint);
                return;
            }

            //declare a temporary timing point list
            SortedDictionary<int, RouteTimingPoint> tempTimingPoints = new SortedDictionary<int, RouteTimingPoint>();

            //copy any timing points after the ordinal in the existing list incrementing the key by 1
            foreach (var t in this._timingPoints.Where(tp => tp.Key >= ordinal))
            {              
                tempTimingPoints.Add(t.Key + 1, t.Value);
            }

            //remove any timing points after the ordinal
            foreach (var t in tempTimingPoints)
            {
                this._timingPoints.Remove(t.Key - 1);
            }

            //add the new timing point
            this._timingPoints.Add(ordinal, routeTimingPoint);

            //copy back the incremented timing points from the temp list
            foreach (var t in tempTimingPoints)
            {
                this._timingPoints.Add(t.Key, t.Value);
            }
        }
    }
}
