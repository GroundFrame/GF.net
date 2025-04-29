using GF.Common;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GF.Rails.Network.SandT
{
    public class SignalBoxPanelOrWorkstation : ISignalBoxPanelOrWorkstation
    {
        #region Fields

        private readonly SignalBoxId _signalBoxId;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the panel or workstation Id
        /// </summary>
        public SignalBoxPanelOrWorkstationId Id { get; private set; }

        /// <summary>
        /// Gets the signal box this panel or workstation period belongs to
        /// </summary>
        public ISignalBox SignalBox { get; private set; }

        /// <summary>
        /// Gets the period the panel or workstation was active
        /// </summary>

        public TemporalPeriod Period { get; private set; }

        /// <summary>
        /// Gets the panel or workstation name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the panel or workstation code (optional)
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the panel items
        /// </summary>
        public SignalBoxPanalOrWorkstationConfig PanelConfig { get; private set; }

        #endregion Properties

        #region Constructors

        public SignalBoxPanelOrWorkstation(DateTime startDate, DateTime? endDate, ISignalBox signalBox, string name, string code = null)
        {
            this.Id = new SignalBoxPanelOrWorkstationId(Guid.NewGuid());
            this.Period = new TemporalPeriod(startDate, endDate);
            this.SignalBox = signalBox;
            this._signalBoxId = signalBox.Id;
            this.Name = name;
            this.Code = code;
            this.PanelConfig = new SignalBoxPanalOrWorkstationConfig(this);
        }

        #endregion Constructors

        #region Methods

        public void AddPanelItem(DateTime startDate, ISignalBoxPanelOrWorkstationItem panelOrWorkstationItem)
        {
            this.PanelConfig.AddPanelItem(startDate, null, panelOrWorkstationItem);
        }

        public Signal AddSignal(DateTime startDate, string code, ISpatial coordinates, SignalHeadType signalHeadType, ISignalHead signalHead, ISignalBlock associatedSignalBlock, DirectionType associatedSignalBlockDirectionType)
        {
            Signal newSignal = new Signal(code, coordinates, startDate, this, signalHeadType, signalHead, associatedSignalBlock, associatedSignalBlockDirectionType);
            this.AddPanelItem(startDate, newSignal);
            return newSignal;
        }

        public IEnumerable<TPanelItem> GetPanelItems<TPanelItem>(DateTime date) where TPanelItem : ISignalBoxPanelOrWorkstationItem
        {
            return this.PanelConfig.GetPanelItems<TPanelItem>(date);
        }

        public void RemovePanelItem(ISignalBoxPanelOrWorkstationItem panelItem)
        {
            this.PanelConfig.RemovePanelItem(panelItem);
        }

        #endregion Methods
    }
}
