using System;
using System.Collections.Generic;
using System.Text;
using GF.Common;
using GF.Graph;

namespace GF.Rails
{
    public abstract class NeighbourBase<TNodeId, TNeighbourProperty>
    {
        private Dictionary<TNeighbourProperty, object> _properties;

        public TNodeId NeighbourID { get; }

        public NeighbourBase(INode<TNodeId, TNeighbourProperty> neighbour)
        {
            this._properties = new Dictionary<TNeighbourProperty, object>();
            this.NeighbourID = neighbour.Id;
        }

        public void AddProperty(TNeighbourProperty propertyKey, object value)
        {
            this._properties.Add(propertyKey, value);
        }

        public T GetProperty<T>(TNeighbourProperty propertyKey)
        {
            return (T)this._properties[propertyKey];
        }
    }
}
