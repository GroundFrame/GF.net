using GF.Rails.Network;
using GF.Rails.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    public struct Action
    {
        public ActionType ActionType { get; set; }

        public TemporalId<TrainId>? ActionTrain { get; set; }

        public ITimingPoint TimingPoint { get; set; }

        public Action (ActionType actionType, ITimingPoint timingPoint, TemporalId<TrainId>? actionTrain)
        {
            this.ActionType = actionType;
            this.TimingPoint = timingPoint;
            this.ActionTrain = actionTrain;
        }


    }
}
