using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.RollingStock
{
    public class BRRollingStockDiagramTrain
    {
        #region Fields 

        private readonly List<BRRollingStockFormation> _formation = new List<BRRollingStockFormation>();

        #endregion Fields

        public TemporalId<DiagramId> DiagramId { get; }

        public TemporalId<TrainId> TrainId { get; }

        public IEnumerable<BRRollingStockFormation> Formation { get { return this._formation; } }

        public BRRollingStockDiagramTrain(BRRollingStockDiagram diagram, Train train)
        {
            this.DiagramId = diagram.Id;
            this.TrainId = train.Id;
        }

        public void AddFormation(BRRollingStockFormation formation)
        {
            this._formation.Add(formation);
        }
    }
}
