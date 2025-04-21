using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Common
{
    /// <summary>
    /// Interface which allows implemented classes to implement a reciprocal value
    /// </summary>
    public interface IReciprocal<T>
    {
        /// <summary>
        /// Gets the original value
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the reciprocal value
        /// </summary>
        public T ReciprocalValue { get; }

        /// <summary>
        /// Calculates the reciprocal value from the supplied value
        /// </summary>
        /// <param name="value">The initial value to reciprocate</param>
        /// <returns><see cref="T"/> containing the reciprocal value</returns>
        //public static abstract T CalculateReciprocalValue(T value);
    }
}
