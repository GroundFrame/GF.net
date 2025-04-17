using GF.Common;
using GF.Rails.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Operations
{
    public class Train
    {
        #region Fields

        List<CallingPoint> _callingPoints = new List<CallingPoint>();

        #endregion Fields

        /// <summary>
        /// Gets the Train Id. The start and end dates defines the period the train operated
        /// </summary>
        public TemporalId<TrainId> Id { get; }

        /// <summary>
        /// Gets the human readable key for the train. Does not need to be unique
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets or sets the <see cref="TimingPointId"/> of the originating timing point of the train
        /// </summary>
        public TimingPointId Origin { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TimingPointId"/> destinaation timing point of the train
        /// </summary>
        public TimingPointId Destination { get; set;  }

        /// <summary>
        /// Gets or sets the headcode of the train
        /// </summary>
        public IHeadcode Headcode { get; set; }

        /// <summary>
        /// Gets or sets the origin departure time
        /// </summary>
        public Time OriginDepartureTime { get; set;  }

        /// <summary>
        /// Gets or sets the destination departure time
        /// </summary>
        public Time DestinationArrivalTime { get; set; }

        /// <summary>
        /// Gets or sets the schedule type
        /// </summary>
        public TrainScheduleType ScheduleType { get; set; }

        /// <summary>
        /// Gets or sets the flag to indicate whether this train is dedicated to specific customer
        /// </summary>
        public bool CompanyTrain { get; set;  }

        /// <summary>
        /// Gets or sets publication status
        /// </summary>
        public TrainPublicationStatus PublicationStatus { get; set;  }

        /// <summary>
        /// Gets or sets the days of the week the train is due to run over the period it operates
        /// </summary>
        public TimeTableDays Days { get; set; }

        /// <summary>
        /// Gets or sets notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets the calling points of this train
        /// </summary>
        public IEnumerable<CallingPoint> CallingPoints { get { return this._callingPoints; } }

        public Train(DateTime from, DateTime? to, ITimingPoint origin, ITimingPoint destination, IHeadcode headcode)
        {
            this.Id = new TemporalId<TrainId>(new TrainId(Guid.NewGuid()), from, to);
            this.Origin = origin.Id;
            this.Destination = destination.Id;
            this.Headcode = headcode;
            this.OriginDepartureTime = new Time(0, 0, 0);
            this.DestinationArrivalTime = new Time(0, 0, 0);
            this.ScheduleType = TrainScheduleType.Scheduled;
            this.Days = TimeTableDays.Everyday();

        }

        public void AddCallingPoint(CallingPoint callingPoint)
        {
            this._callingPoints.Add(callingPoint);
        }

        public CallingPoint AddCallingPoint(ITimingPoint timingPoint, Time departs, bool passes)
        {
            CallingPoint newCallingPoint = new CallingPoint(timingPoint, departs, passes);
            this.AddCallingPoint(newCallingPoint);
            return newCallingPoint;
        }
    }
}
