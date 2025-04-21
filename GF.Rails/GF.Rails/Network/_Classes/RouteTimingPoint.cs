using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Network
{
    public struct RouteTimingPoint
    {
        public TimingPointId TimingPointId { get; }

        public RouteTimingPointType Type { get; }

        public RouteTimingPoint(TimingPointId timingPointId, RouteTimingPointType type)
        {
            this.TimingPointId = timingPointId;
            this.Type = type;
        }
    }
}
