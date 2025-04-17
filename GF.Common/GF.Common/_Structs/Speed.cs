using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Common
{
    public struct Speed : IComparable<Speed>, IGraphCost<Speed>
    {
        #region Constants

        const double mphToKph = 1.60934f;
        const double kphToMph = 0.621371f;

        #endregion Constants

        #region static Properties

        /// <summary>
        /// Gets the speed max speed
        /// </summary>
        public static Speed MaxValue { get { return new Speed(double.MaxValue); } }

        /// <summary>
        /// Gets the speed min speed
        /// </summary>
        public static Speed MinValue { get { return new Speed(0); } }

        #endregion static Properties

        #region Properties

        /// <summary>
        /// Gets the speed in miles per hour
        /// </summary>
        public int MPH { get; }

        /// <summary>
        /// Gets the speed in kilometres per hour
        /// </summary>
        public double KPH { get; }

        #endregion Properties

        /// <summary>
        /// Instantiates a new <see cref="Speed"/> struct from the supplied the miles per hour
        /// </summary>
        /// <param name="mph">Miles per hour</param>
        public Speed (int mph)
        {
            this.MPH = mph;
            this.KPH = mph * mphToKph;
        }

        /// <summary>
        /// Instantiates a new <see cref="Speed"/> struct from the supplied the kilometres per hour
        /// </summary>
        /// <param name="mph">Kilometres per hour</param>
        public Speed(double kph)
        {
            this.KPH = kph;
            this.MPH = Convert.ToInt32(Math.Round(kph * kphToMph,0));
        }

        /// <summary>
        /// Compares this speed to another speed
        /// </summary>
        /// <param name="other">The other <see cref="Speed"/> to compare</param>
        /// <returns><see cref="int"/>. 1 if this speed is greater than the other speed, -1 if this speed is less than the other speed, 0 if both speeds are the same</returns>
        public int CompareTo(Speed other)
        {
            return this.KPH.CompareTo(other.KPH);
        }

        /// <summary>
        /// Adds this speed to another speed and returns a new speed.
        /// </summary>
        /// <param name="other">The other <see cref="Speed"/> to add to this speed</param>
        /// <returns><see cref="Speed"/> representing the two speeds added together</returns>
        public Speed Add(Speed other)
        {
            return new Speed(this.KPH + other.KPH);
        }
    }
}
