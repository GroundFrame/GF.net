using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    public enum TrainScheduleType
    {
        /// <summary>
        /// The train runs on a regular schedule
        /// </summary>
        Scheduled = 0,
        /// <summary>
        /// The train only runs when required
        /// </summary>
        RunsAsRequired = 1,
        /// <summary>
        /// The train is a variation on an existing train
        /// </summary>
        Variation = 2,
        /// <summary>
        /// The train is short notice (longer than 24 hours notice)
        /// </summary>
        ShortTermPlanning = 3,
        /// <summary>
        /// The train is short notice (less than 24 hours notice)
        /// </summary>
        VeryShortTermPlanning = 4
    }
}
