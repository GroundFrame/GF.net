using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    public interface ITemporalHistory<TId>
    {
        public TemporalId<TId> Id { get; }

        public void UpdateId(TemporalId<TId> newId);
    }
}
