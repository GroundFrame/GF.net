using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    [Flags]
    public enum TrainPublicationStatus
    {
        /// <summary>
        /// The train is published in public time tables
        /// </summary>
        PublicTimeTable = 1,
        /// <summary>
        /// The train is published in suppliment(s) only (such as Motorail)
        /// </summary>
        Suppliment = 2
    }
}
