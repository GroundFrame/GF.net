using GF.Common;
using GF.Graph;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network
{
    public class SignalBox : TemporalBase<SignalBoxId, SignalBoxHistory>, ISignalBox, ITemporal<SignalBoxId, SignalBoxHistory>
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
        /// <param name="coordinates">The coordiates of the signal box</param>
        public SignalBox(string name, ISpatial coordinates) : base(new SignalBoxId(Guid.NewGuid()), name)
        {
            this.Coordinates = coordinates;
        }

        /// <summary>
        /// Instantiates a new <see cref="SignalBox"/>
        /// </summary>
        /// <param name="name">The place name</param>
        /// <param name="coordinates">The coordiates of the signal box</param>
        /// <param name="startDate">The date the signal box opened</param>
        /// <param name="businessUnit">The business unit the signal box was initally assigned to</param>
        /// <param name="type">The signal box type</param>
        /// <param name="code">The signal box code</param>
        public SignalBox(string name, ISpatial coordinates, DateTime startDate, IBusinessUnit businessUnit, SignalBoxType type, string code) : this (name, coordinates)
        {
            this.AddHistory(new SignalBoxHistory(this.Id, startDate, null, businessUnit, type, code));
        }

        #endregion Constructors
    }
}
