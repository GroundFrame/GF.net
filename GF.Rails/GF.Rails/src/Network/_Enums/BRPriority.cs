using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    /// <summary>
    /// Enum containing the BR headcode classification / priortiy.
    /// </summary>
    public enum BRPriority
    {
        LightLoco = 0,
        /// <summary>
        /// Express passenger, breakdown or snowplough duty
        /// </summary>
        ExpressPassenger = 1,
        /// <summary>
        /// Ordinary, suburban, branch passenger or breakdown train not on duty
        /// </summary>
        OrdinaryPassenger = 2,
        /// <summary>
        /// Parels or perishable goods train composed of bogie fitted vehicles
        /// </summary>
        Parcels = 3,
        /// <summary>
        /// Freight timed at 75MPH
        /// </summary>
        Freight75 = 4,
        /// <summary>
        /// Empty coaching stock
        /// </summary>
        ECS = 5,
        /// <summary>
        /// Freight timed over 50MPH
        /// </summary>
        Freight50 = 6,
        /// <summary>
        /// Freight timed less than 45MPH
        /// </summary>
        Freight45 = 7,
        /// <summary>
        /// Freight timed less than 45MPH
        /// </summary>
        Freight35 = 8,
        /// <summary>
        /// Train not all fitted with continuous breaks
        /// </summary>
        Unfitted = 9
    }
}
