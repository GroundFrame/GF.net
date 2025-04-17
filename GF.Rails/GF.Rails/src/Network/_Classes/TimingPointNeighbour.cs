using System;
using System.Collections.Generic;
using System.Text;
using GF.Common;
using GF.Graph;

namespace GF.Rails.Network
{
    public class TimingPointNeighbour : NeighbourBase<TimingPointId, TimingPointNeighbourProperty>, INeighbour<TimingPointId, TimingPointNeighbourProperty>
    {
        public TemporalId<TimingPointId> TemporalId { get; set; }

        public TimingPointNeighbour(ITimingPoint toNode, DirectionType directionType, Length distance, DateTime startDate, DateTime? endDate = null) : base((TimingPoint)toNode)
        {
            this.AddProperty(TimingPointNeighbourProperty.Direction, new Direction(directionType));
            this.AddProperty(TimingPointNeighbourProperty.Distance, distance);
            this.TemporalId = new TemporalId<TimingPointId>(toNode.Id, startDate, endDate);
        }

        public TimingPointNeighbour(ITimingPoint toNode, DirectionType directionType, ITimingPoint fromNode, DateTime startDate, DateTime? endDate = null) : this (toNode, directionType, fromNode.DistanceTo((TimingPoint)toNode), startDate, endDate)
        {
        }
    }
}
