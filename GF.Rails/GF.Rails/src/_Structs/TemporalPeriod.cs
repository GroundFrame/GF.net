using Newtonsoft.Json;
using System;
using GF.Common;

namespace GF.Rails
{
    /// <summary>
    /// Struct representing a temporal period.
    /// </summary>
    public struct TemporalPeriod
    {
        #region Fields
        [JsonProperty("startDateYMDV")]
        private readonly YMDV _StartDateYMDV; //stores the start YMDV
        [JsonProperty("endDateYMDV")]
        private YMDV _EndDateYMDV; //stores the start YMDV

        #endregion Fields

        #region Properties

        /// <summary>
        /// The temporal period start date
        /// </summary>
        [JsonIgnore]
        public DateTime StartDate { get; }

        /// <summary>
        /// The temporal period end date
        /// </summary>
        [JsonIgnore]
        public DateTime? EndDate { get; private set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="TemporalPeriod"/> from the supplied start and end date
        /// </summary>
        /// <param name="startDate">The temporal period start date</param>
        /// <param name="endDate">The temporal period end date. Pass null if the period is currently active</param>
        public TemporalPeriod (DateTime startDate, DateTime? endDate)
        {
            if (endDate != null && endDate < startDate)
            {
                throw new ArgumentException("The end date cannot be before the end date.");
            }

            this.StartDate = startDate;
            this._StartDateYMDV = new YMDV(startDate);
            this.EndDate = endDate;
            this._EndDateYMDV = new YMDV(endDate);
        }

        public TemporalPeriod (YMDV startDateYMDV, YMDV endDateYMDV)
        {
            if (startDateYMDV.Value.Equals(0))
            {
                throw new ArgumentException($"The {nameof(startDateYMDV)} argument cannot be 0.");
            }

            this._StartDateYMDV = startDateYMDV;
            this.StartDate = (DateTime)startDateYMDV.ConvertToDateTime();
            this._EndDateYMDV = endDateYMDV;
            this.EndDate = endDateYMDV.ConvertToDateTime();
        }

        #endregion Constructors

        #region Methods

        public bool Equals(TemporalPeriod other)
        {
            return this.StartDate.Equals(other.StartDate) && this.EndDate.Equals(other.EndDate);
        }

        /// <summary>
        /// Returns a flag to indicate whether this temporal period is valid on the supplied date
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to check</param>
        /// <returns>True if the date is valid otherwise false</returns>
        public bool ContainsDate(DateTime date)
        {
            YMDV dateYMV = new YMDV(date);
            YMDV cleanEndYMV = this._EndDateYMDV.Value == 0 ? dateYMV : this._EndDateYMDV;

            return dateYMV.Value >= this._StartDateYMDV.Value && dateYMV.Value <= cleanEndYMV.Value;
        }

        public bool Overlaps(TemporalPeriod period)
        {
            var maxEndPeriod = Math.Max(this._EndDateYMDV.Value, period._EndDateYMDV.Value);

            //if the end period is zero then set it today
            if (maxEndPeriod.Equals(0)) maxEndPeriod = new YMDV(DateTime.Today).Value;

            if (period._StartDateYMDV.Value <= this._StartDateYMDV.Value && Math.Max(period._EndDateYMDV.Value, maxEndPeriod) > Math.Max(this._EndDateYMDV.Value, maxEndPeriod))
            {
                return true;
            }

            if (period._StartDateYMDV.Value < this._StartDateYMDV.Value && Math.Max(period._EndDateYMDV.Value, maxEndPeriod) >= this._StartDateYMDV.Value && Math.Max(period._EndDateYMDV.Value, maxEndPeriod) <= Math.Max(this._EndDateYMDV.Value, maxEndPeriod))
            {
                return true;
            }

            if (period._StartDateYMDV.Value >= this._StartDateYMDV.Value && period._StartDateYMDV.Value < Math.Max(period._EndDateYMDV.Value, maxEndPeriod) && Math.Max(period._EndDateYMDV.Value, maxEndPeriod) >= Math.Max(this._EndDateYMDV.Value, maxEndPeriod))
            {
                return true;
            }

            if (period._StartDateYMDV.Value >= this._StartDateYMDV.Value && period._StartDateYMDV.Value <= Math.Max(period._EndDateYMDV.Value, maxEndPeriod) && Math.Max(period._EndDateYMDV.Value, maxEndPeriod) <= Math.Max(this._EndDateYMDV.Value, maxEndPeriod) && Math.Max(period._EndDateYMDV.Value, maxEndPeriod) > this._StartDateYMDV.Value)
            {
                return true;
            }

            return false;
        }

        public void SetEndDate(DateTime endDate)
        {
            if (endDate < this.StartDate)
            {
                throw new Exception("The supplied end date cannot be before the start date");
            }

            this.EndDate = endDate;
        }

        #endregion Methods
    }
}
