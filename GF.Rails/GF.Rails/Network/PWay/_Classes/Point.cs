using GF.Common;
using GF.Graph;
using GF.Rails.Network;
using GF.Rails.Network.SandT;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network.PWay
{
    public class Point : TemporalBase<PointOrSwitchId, PointHistory>, ITemporal<PointOrSwitchId, PointHistory>, ISignalBoxPanelOrWorkstationItem
    {
        #region Properties

        /// <summary>
        /// Gets the coordinates of the points
        /// </summary>
        public ISpatial Coordinates { get; private set; }

        /// <summary>
        /// Gets the panel id
        /// </summary>
        public Guid PanelItemId { get {  return this.Id.Value; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="Point"/>
        /// </summary>
        /// <param name="number">The point or switch number excluding the signal box code</param>
        /// <param name="coordinates">The point or switch coordinates</param>
        public Point(string number, ISpatial coordinates) : base(new PointOrSwitchId(Guid.NewGuid()), number)
        {
            this.Coordinates = coordinates;
        }

        public Point(string number, ISpatial coordinates, DateTime startDate, ISignalBoxPanelOrWorkstation signalBoxPanelOrWorkstation) : this(number, coordinates)
        {
            this.AddHistory(new PointHistory(this.Id, startDate, null, signalBoxPanelOrWorkstation));
            signalBoxPanelOrWorkstation.AddPanelItem(startDate, this);
        }

        /// <summary>
        /// Determines whether 2 <see cref="Point"/> items match
        /// </summary>
        /// <param name="obj">The other object to compare</param>
        /// <returns>True if the items are equal otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Point))
            {
                return false;
            }

            return this.PanelItemId.Equals((obj as Point).PanelItemId);
        }

        /// <summary>
        /// Returns the hashcode for this isntance
        /// </summary>
        public override int GetHashCode()
        {
            return (this.PanelItemId).GetHashCode();
        }

        #endregion Constructors
    }
}
