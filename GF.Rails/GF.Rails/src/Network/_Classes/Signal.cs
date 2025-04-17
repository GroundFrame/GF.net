using GF.Common;
using GF.Graph;
using GF.Rails.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.src.Network
{
    public class Signal : TemporalBase<SignalId, SignalHistory>
    {
        public ISpatial Coordinates { get; private set; }

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="Place"/>
        /// </summary>
        /// <param name="name">The place name</param>
        /// <param name="key">The place key</param>
        public Signal(string name, string key, ISpatial coordinates) : base(new SignalId(Guid.NewGuid()), name, key)
        {
            this.Coordinates = coordinates;
        }

        #endregion Constructors
    }
}
