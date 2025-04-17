using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GF.Rails.Operations
{
    /// <summary>
    /// A struct represent a BR headcode.
    /// </summary>
    public struct BRHeadcode : IHeadcode
    {
        [JsonProperty("headcode")]
        private string _headcode; //stores the headcode.

        /// <summary>
        /// Gets the priority (0 - 255; The lower the number the higher the priority)
        /// </summary>
        [JsonProperty("priority")]
        public byte Priority { get; }

        /// <summary>
        /// Instantiates a new <see cref="BRHeadcode"/>
        /// </summary>
        /// <param name="headcode">The 4 digit headcode</param>
        /// <param name="priority">The priority of the train (the lower number the higher the priority</param>
        private BRHeadcode(string headcode, byte priority)
        {
            if (!BRHeadcode.IsHeadcodeValid(headcode))
            {
                throw new ArgumentNullException(nameof(headcode),"Please supply a valid BR Headcode.");
            }

            this._headcode = headcode.ToUpper();
            this.Priority = priority;
        }

        /// <summary>
        /// Intialises a new <see cref="BRHeadcode"/> from supplied BR 4 digit headcode
        /// </summary>
        /// <param name="headcode">The 4 digit headcode</param>
        public static BRHeadcode Initialise(string headcode)
        {
            string trainClass = headcode.Substring(0, 1);
            byte priority = Convert.ToByte(trainClass);
            return new BRHeadcode(headcode, priority == 0 ? (byte)BRPriority.Freight75 : priority);
        }

        /// <summary>
        /// Intialises a new <see cref="BRHeadcode"/> from supplied BR 4 digit headcode and an priority override
        /// </summary>
        /// <param name="headcode">The 4 digit headcode</param>
        /// <param name="priority">The priority override</param>
        public static BRHeadcode Initialise(string headcode, BRPriority priority)
        {
            return new BRHeadcode(headcode, Convert.ToByte(priority));
        }

        /// <summary>
        /// Returns the headcode a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this._headcode;
        }

        /// <summary>
        /// Determines whether the suppled <paramref name="headcode"/> is a valid BR Headcode
        /// </summary>
        /// <param name="headcode">The headcode to validate</param>
        /// <returns><see cref="bool"/>. True if the headcode is valid otherwise false.</returns>
        private static bool IsHeadcodeValid(string headcode)
        {
            Regex regex = new Regex("1[A-Za-z]\\d\\d", RegexOptions.IgnoreCase);
            return regex.IsMatch(headcode);
        }
    }
}
