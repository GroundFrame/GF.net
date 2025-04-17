using GF.Common;
using GF.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    public class SignalBox : TemporalBase<SignalBoxId, SignalBoxHistory>
    {
        /// <summary>
        /// Gets the coordinates of the signal box
        /// </summary>
        public ISpatial Coordinates { get; private set; }

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="SignalBox"/>
        /// </summary>
        /// <param name="name">The place name</param>
        /// <param name="key">The place key</param>
        public SignalBox(string name, string key, ISpatial coordinates) : base(new SignalBoxId(Guid.NewGuid()), name, key)
        {
            this.Coordinates = coordinates;
        }

        #endregion Constructors
    }
}
