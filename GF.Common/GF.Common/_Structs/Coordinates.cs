using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Common
{
    /// <summary>
    /// Struct representing a set of coordinates
    /// </summary>
    public struct Coordinates : ISpatial
    {
        /// <summary>
        /// Gets the latitude
        /// </summary>
        public float Latitude { get; private set; }

        /// <summary>
        /// Gets the longitude
        /// </summary>
        public float Longitude { get; private set; }

        /// <summary>
        /// Gets the X coordinate.
        /// </summary>
        public readonly float X { get { return this.Longitude; } }

        /// <summary>
        /// Gets the Y coordinate
        /// </summary>
        public readonly float Y { get { return this.Latitude; } }

        /// <summary>
        /// Instantiates a new <see cref="Coordinates"/>
        /// </summary>
        /// <param name="latitude">The latitude</param>
        /// <param name="longitude">The longitude</param>
        public Coordinates(float latitude, float longitude)
        {
            if (latitude < -90 || latitude > 90)
            {
                throw new ArgumentException("The supplied latitude is not valid. Must be between -90 & 90.", nameof(latitude));
            }

            if (longitude < -180 || longitude > 180)
            {
                throw new ArgumentException("The supplied latitude is not valid. Must be between -180 & 180.", nameof(longitude));
            }

            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Calculates the distance between this <see cref="Coordinates"/> and the supplied <see cref="Coordinates"/>
        /// </summary>
        /// <param name="target">The target <see cref="Coordinates"/></param>
        /// <returns></returns>
        public readonly Length DistanceTo(ISpatial target)
        {
            var d1 = this.X * (Math.PI / 180.0);
            var num1 = this.Y * (Math.PI / 180.0);
            var d2 = target.X * (Math.PI / 180.0);
            var num2 = target.Y * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return new Length(6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))));
        }
    }
}
