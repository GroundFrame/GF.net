using System;
using System.Collections.Generic;
using System.Text;
using GF.Common;
using GF.Graph;

namespace GF.Rails.Network.SandT
{
    public class SignalBlockNeighbour : NeighbourBase<SignalBlockId, SignalBlockNeighbourProperty>, INeighbour<SignalBlockId, SignalBlockNeighbourProperty>
    {
        public SignalBlockNeighbour(SignalBlock toNode, DirectionType directionType, SignallingType signallingType) : base(toNode)
        {
            this.AddProperty(SignalBlockNeighbourProperty.Direction, new Direction(directionType));
            this.AddProperty(SignalBlockNeighbourProperty.SignalType, signallingType);
        }
    }
}
