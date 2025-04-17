using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Common
{
    public struct YMDV
    {
        #region Fields

        [JsonProperty("year")]
        private ushort _year;
        [JsonProperty("month")]
        private byte _month;
        [JsonProperty("day")]
        private byte _day;

        #endregion

        /// <summary>
        /// Gets the YMDV value
        /// </summary>
        [JsonIgnore]
        public uint Value { get; private set; }

        public YMDV (DateTime? date)
        {
            if (date == null)
            {
                this._year = 0;
                this._month = 0;
                this._day = 0;
            }
            else
            {
                this._year = (ushort)((DateTime)date).Year;
                this._month = (byte)((DateTime)date).Month;
                this._day = (byte)((DateTime)date).Day;
            }

            this.Value = (uint)(this._year * 10000) + (ushort)(this._month * 100) + this._day;
        }

        public YMDV (ushort year, byte month, byte day)
        {
            this._year = year;
            this._month = month;
            this._day = day;
            this.Value = (uint)(this._year * 10000) + (ushort)(this._month * 100) + this._day;
        }

        public DateTime? ConvertToDateTime()
        {
            if (this.Value == 0)
            {
                return null;
            }

            return new DateTime(this._year, this._month, this._day);
        }


        public bool Equals(YMDV other)
        {
            return this.Value.Equals(other.Value);
        }
    }
}
