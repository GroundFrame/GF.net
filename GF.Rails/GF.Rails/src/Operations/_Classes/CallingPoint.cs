using GF.Common;
using GF.Rails.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Operations
{
    public class CallingPoint
    {
        public TimingPointId TimingPoint { get; }

        public Time? Arrives { get; }

        public Time Departs { get; }

        public bool Passes { get; }

        public SignalBlockId? Path { get; }

        public SignalBlockId? Platform { get; }

        public SignalBlockId? Line { get; }

        public CallingPoint (ITimingPoint timingPoint, Time departs, bool passes)
        {
            this.TimingPoint = timingPoint.Id;
            this.Arrives = null;
            this.Departs = departs;
            this.Passes = passes;
            this.Path = null;
            this.Platform = null;
            this.Line = null;
        }
    }
}
