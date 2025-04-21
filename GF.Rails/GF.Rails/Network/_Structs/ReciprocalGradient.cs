using GF.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails
{
    public struct ReciprocalGradient : IReciprocal<Gradient>
    {
        /// <summary>
        /// Gets the gradient value
        /// </summary>
        public Gradient Value { get; }

        /// <summary>
        /// Gets the reciporcal gradient value
        /// </summary>
        public Gradient ReciprocalValue { get; }

        /// <summary>
        /// Gets the direction the <see cref="ReciprocalGradient.Value"/> applies
        /// </summary>
        public DirectionType Direction { get; }

        /// <summary>
        /// Instantiates new <see cref="ReciprocalGradient"/> from the supplied <see cref="Gradient"/> and <see cref="DirectionType"/>
        /// </summary>
        /// <param name="gradient">The source gradient</param>
        /// <param name="direction">The direction in which the source gradient applies. Must be Up or Down</param>
        public ReciprocalGradient(Gradient gradient, DirectionType direction)
        {
            if (!(direction == DirectionType.Down || direction == DirectionType.Up))
            {
                throw new ArgumentException("The direction can only be up or down");
            }

            this.Direction = direction;
            this.Value = gradient;
            this.ReciprocalValue = new Gradient(gradient.Ratio * -1);
        }
    }
}
