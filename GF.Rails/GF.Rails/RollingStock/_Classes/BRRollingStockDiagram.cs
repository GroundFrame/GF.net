using GF.Rails.Network;
using System.Collections.Generic;

namespace GF.Rails.RollingStock
{
    public class BRRollingStockDiagram
    {
        #region Fields

        List<BRRollingStockDiagramTrain> _trainAllocation = new List<BRRollingStockDiagramTrain>();

        #endregion Fields

        public TemporalId<DiagramId> Id {get;}

        public string Depot { get; set; }

        public string Name { get; set; }

        public IEnumerable<BRRollingStockDiagramTrain> TrainAllocation { get { return this._trainAllocation; } }


    }
}
