using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Common
{
    public struct Gradient
    {
        /// <summary>
        /// Get the gradient ratio
        /// </summary>
        public int Ratio { get; }

        /// <summary>
        /// Get the gradient percentage
        /// </summary>
        public float Percentage { get; }

        /// <summary>
        /// Instantiates a new gradient from the supplied ratio. For example if the gradient is 1/200 uphill then pass 200. If 1/75 downhill then supply -75
        /// </summary>
        /// <param name="ratio"></param>
        public Gradient (int ratio)
        {
            this.Ratio = ratio;
            this.Percentage = ratio == 0 ? 0 :(1.00f / Convert.ToSingle(ratio)) * 100;
        }

        /// <summary>
        /// Instantiates a new gradient from the supplied ratio. For example if the gradient is 1/200 uphill then pass 200. If 1/75 downhill then supply -75
        /// </summary>
        /// <param name="ratio"></param>
        public Gradient(float percentage)
        {
            this.Percentage = percentage;
            this.Ratio = percentage == 0 ? 0 : Convert.ToInt32(Math.Round(((100/percentage) * 1),0));
        }
    }
}
