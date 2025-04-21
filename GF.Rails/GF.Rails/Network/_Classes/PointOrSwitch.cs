using GF.Common;
using GF.Graph;
using GF.Rails.Network;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network
{
    public class PointOrSwitch : TemporalBase<PointOrSwitchId, PointOrSwitchHistory>, ITemporal<PointOrSwitchId, PointOrSwitchHistory>
    {
        public ISpatial Coordinates { get; private set; }

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="PointOrSwitch"/>
        /// </summary>
        /// <param name="number">The point or switch number excluding the signal box code</param>
        /// <param name="coordinates">The point or switch coordinates</param>
        public PointOrSwitch(string number, ISpatial coordinates) : base(new PointOrSwitchId(Guid.NewGuid()), number)
        {
            this.Coordinates = coordinates;
        }

        public PointOrSwitch(string number, ISpatial coordinates, DateTime startDate,ISignalBoxPanelOrWorkstation panelWorkstation) : this(number, coordinates)
        {
            this.AddHistory(new PointOrSwitchHistory(this.Id, startDate, null, panelWorkstation));
        }

        #endregion Constructors
    }
}
