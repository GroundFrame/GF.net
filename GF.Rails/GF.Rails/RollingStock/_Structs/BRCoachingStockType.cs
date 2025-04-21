using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GF.Rails.RollingStock
{
    public struct BRCoachingStockType
    {
        #region Fields

        private string _code;

        #endregion Fields

        /// <summary>
        /// Gets or sets the code
        /// </summary>
        [JsonProperty("code")]
        [Required]
        public string Code { get { return this._code; } set { this._code = value.ToUpper(); } }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Determines whether this <see cref="BRCoachingStockType"/> and the supplied <see cref="BRCoachingStockType"/> object have the same value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see cref="bool"/>. Returns <see cref="true"/> if supplied <paramref name="value"/> is the same as this <see cref="BRCoachingStockType"/> otherwise <see cref="false"/>.</returns>
        public override bool Equals(object value) => this.Equals(value as BRCoachingStockType?);

        /// <summary>
        /// Returns the hashcode for this <see cref="BRCoachingStockType"/>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => (this.Code).GetHashCode();

        /// <summary>
        /// Overrides the == operator
        /// </summary>
        /// <param name="lhs">The first <see cref="BRCoachingStockType"/> to compare</param>
        /// <param name="rhs">The second <see cref="BRCoachingStockType"/> to compare</param>
        /// <returns><see cref="bool"/>. Returns <see cref="true"/> if supplied <paramref name="lhs"/> is equal as <paramref name="rhs"/> otherwise <see cref="false"/>.</returns>
        public static bool operator ==(BRCoachingStockType lhs, BRCoachingStockType rhs) => lhs.Equals(rhs);

        /// <summary>
        /// Overrides the != operator
        /// </summary>
        /// <param name="lhs">The first <see cref="BRCoachingStockType"/> to compare</param>
        /// <param name="rhs">The second <see cref="BRCoachingStockType"/> to compare</param>
        /// <returns><see cref="bool"/>. Returns <see cref="true"/> if supplied <paramref name="lhs"/> is not equal <paramref name="rhs"/> otherwise <see cref="false"/>.</returns>
        public static bool operator !=(BRCoachingStockType lhs, BRCoachingStockType rhs) => !lhs.Equals(rhs);

        /// <summary>
        /// Determines whether this <see cref="BRCoachingStockType"/> and the supplied <see cref="BRCoachingStockType"/> object have the same value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see cref="bool"/>. Returns <see cref="true"/> if supplied <paramref name="value"/> is the same as this <see cref="BRCoachingStockType"/> otherwise <see cref="false"/>.</returns>
        private bool Equals(BRCoachingStockType? brCarriageType)
        {
            if (brCarriageType == null)
            {
                return false;
            }

            BRCoachingStockType item = (BRCoachingStockType)brCarriageType;

            return this.Code.Equals(item.Code, StringComparison.OrdinalIgnoreCase);
        }
    }
}
