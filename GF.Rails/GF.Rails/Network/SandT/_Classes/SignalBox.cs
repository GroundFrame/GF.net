using GF.Common;
using GF.Graph;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GF.Rails.Network.SandT
{
    public class SignalBox : TemporalBase<SignalBoxId, SignalBoxHistory>, ISignalBox, ITemporal<SignalBoxId, SignalBoxHistory>
    {
        #region Fields
        private List<ISignalBoxPanelOrWorkstation> _panelOrWorkstations { get; set; }

        #endregion Fields

        /// <summary>
        /// Gets the coordinates of the signal box
        /// </summary>
        public ISpatial Coordinates { get; private set; }

        /// <summary>
        /// Gets the panel or workstations associated with this signal box history
        /// </summary>
        public IEnumerable<ISignalBoxPanelOrWorkstation> PanelOrWorkstations { get { return this._panelOrWorkstations; } }

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="SignalBox"/>
        /// </summary>
        /// <param name="name">The place name</param>
        /// <param name="coordinates">The coordiates of the signal box</param>
        public SignalBox(string name, ISpatial coordinates) : base(new SignalBoxId(Guid.NewGuid()), name)
        {
            this.Coordinates = coordinates;
            this._panelOrWorkstations = new List<ISignalBoxPanelOrWorkstation>();
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

        /// <summary>
        /// Adds or updates a panel or workstation to this signal box
        /// </summary>
        /// <param name="panelOrWorkstation">The panel or workstation to update</param>
        public void AddOrUpdatePanelOrWorkstation(ISignalBoxPanelOrWorkstation panelOrWorkstation)
        {
            int existingIndex = this._panelOrWorkstations.FindIndex(pw => pw.Id.Equals(panelOrWorkstation.Id));

            if (existingIndex == -1)
            {
                this._panelOrWorkstations.Add(panelOrWorkstation);
                return;
            }

            this._panelOrWorkstations[existingIndex] = panelOrWorkstation;
        }

        /// <summary>
        /// Adds a panel or workstation to this signal box and return the new <see cref="SignalBoxPanelOrWorkstation"/>
        /// </summary>
        /// <param name="startDate">The date the panel or workstation  was commissioned</param>
        /// <param name="endDate">The date panel or workstation was decomissioned (leave blank if still active)</param>
        /// <param name="name">The panel or workstation name</param>
        /// <param name="code">The panel or workstation (optional)</param>
        public SignalBoxPanelOrWorkstation AddPanelOrWorkstation(DateTime startDate, DateTime? endDate, string name, string code)
        {
            if (this._panelOrWorkstations.Any(pw => pw.Name.Equals(name, StringComparison.Ordinal)))
            {
                throw new ArgumentException("A panel called '{name}' already exists in the signal box");
            }

            var newSignalBoxPanelOrWorkstation = new SignalBoxPanelOrWorkstation(startDate, endDate, this, name, code);
            this.AddOrUpdatePanelOrWorkstation(newSignalBoxPanelOrWorkstation);
            return newSignalBoxPanelOrWorkstation;
        }


        public bool PanelOrWorkstationsOverlapPeriod(DateTime startDate, DateTime? endDate)
        {
            return this.PanelOrWorkstations.Any(pws => pws.Period.Overlaps(startDate, endDate));
        }

        #endregion Constructors
    }
}
