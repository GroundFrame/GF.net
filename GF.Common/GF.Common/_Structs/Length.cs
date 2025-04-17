using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GF.Common
{
    /// <summary>
    /// Struct representing a length (or distance)
    /// </summary>
    public struct Length : IComparable<Length>, IGraphCost<Length>
    {
        #region Constants

        const double metersToInches = 39.3701f; //convert meters to inches
        const double inchesToMeters = 0.0254f; //convert inches to meters
        const int chainsToInches = 792; //convert chains to inches
        const int milesToChains = 80; //convert miles to chains

        #endregion Constants

        #region Properties

        /// <summary>
        /// Returns the maximum possible length.
        /// </summary>
        public static Length MaxValue { get { return new Length(double.MaxValue); } }

        /// <summary>
        /// Returns the minimum possible length
        /// </summary>
        public static Length MinValue { get { return new Length(0); } }

        /// <summary>
        /// Gets the length in meters
        /// </summary>
        public double Meters { get; private set; }

        /// <summary>
        /// Gets the length in inches
        /// </summary>
        public double Inches { get; private set; }

        /// <summary>
        /// Gets the length formatted as miles and chaines
        /// </summary>
        public string MilesAndChains { get { return this.ConvertToMilesAndChains(); } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="Length"/> from the supplied meters
        /// </summary>
        /// <param name="meters">The number of meters</param>
        public Length (double meters)
        {
            if (meters < 0)
            {
                throw new ArgumentException("The meters argument must be greater or equal to zero.", nameof(meters));
            }

            this.Meters = meters;
            this.Inches = meters * metersToInches;
        }

        /// <summary>
        /// Instantiates a new <see cref="Length"/> from the supplied miles and chains
        /// </summary>
        /// <param name="miles">The number of miles</param>
        /// <param name="chains">The number of chains</param>
        public Length (int miles, byte chains)
        {
            if (miles < 0)
            {
                throw new ArgumentException("The miles argument must be greater or equal to zero.", nameof(miles));
            }

            if (chains < 0 || chains > 80)
            {
                throw new ArgumentException("The chains argument must be between 0 and 80.", nameof(chains));
            }


            this.Inches = ((miles * milesToChains) + chains) * chainsToInches;
            this.Meters = this.Inches * inchesToMeters;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Compares this <see cref="Length"/> to another <see cref="Length"/>
        /// </summary>
        /// <param name="length">The <see cref="Length"/> to compare with this length</param>
        /// <returns><see cref="int"/>. If less than zero then this <see cref="Length"/> is less than <paramref name="length"/>. If greater than zero then this <see cref="Length"/> is greater than <paramref name="length"/> otherwise zero.</returns>
        public readonly int CompareTo(Length length)
        {
            return this.Meters.CompareTo(length.Meters);
        }

        /// <summary>
        /// Adds a <see cref="Length"/> to this length and returns a new <see cref="Length"/>
        /// </summary>
        /// <param name="other">The other <see cref="Length"/> to add to this <see cref="Length"/></param>
        /// <returns>A <see cref="Length"/> containing the result of adding this <see cref="Length"/> to <paramref name="other"/></returns>
        public readonly Length Add(Length other)
        {
            return new Length(this.Meters + other.Meters);
        }

        /// <summary>
        /// Converts this length to a string (formatted as miles and chains)
        /// </summary>
        /// <returns>A <see cref="string"/> containing the length as miles and chains</returns>
        public override readonly string ToString()
        {
            return ConvertToMilesAndChains();
        }

        /// <summary>
        /// Converts this length as miles and chains
        /// </summary>
        /// <returns>A <see cref="string"/> containing the length as miles and chains</returns>
        private readonly string ConvertToMilesAndChains()
        {
            var chains = Math.Floor(this.Inches / chainsToInches);
            var miles = Math.Floor(chains / milesToChains);
            return $"{miles}m {chains - (miles * milesToChains)}ch";
        }

        #endregion Methods
    }
}
