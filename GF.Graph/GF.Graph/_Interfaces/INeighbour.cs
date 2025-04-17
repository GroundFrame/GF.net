using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Graph
{
    public interface INeighbour<TNodeID, TPropertyKey>
    {
        public TNodeID NeighbourID { get; }

        public T GetProperty<T>(TPropertyKey propertyKey);
    }
}
