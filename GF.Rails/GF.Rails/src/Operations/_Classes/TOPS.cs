using System;
using System.Collections.Generic;
using System.Linq;
using GF.Common;
using GF.Rails.RollingStock;
using Newtonsoft.Json;

namespace GF.Rails.Operations
{
    public class TOPS
    {
        #region Fields

        [JsonProperty("coachTypes")]
        private List<BRCoachingStockType> _coachingStockTypes = new List<BRCoachingStockType>(); //stores the list of coach types (TSO, FO etc.)
        [JsonProperty("rollingStockCodes")]
        private List<TOPSRollingStockCode> _topRollingStockCodes = new List<TOPSRollingStockCode>(); //stores the list of coach types (TSO, FO etc.)

        #endregion Fields

        /// <summary>
        /// Gets the collection of coach stock types (TSO, FO etc.)
        /// </summary>
        [JsonIgnore]
        public IEnumerable<BRCoachingStockType> CoachingStockTypes { get { return this._coachingStockTypes; } }

        /// <summary>
        /// Gets the collection of rolling stock codes
        /// </summary>
        [JsonIgnore]
        public IEnumerable<TOPSRollingStockCode> TopsRollingStockCodes { get { return this._topRollingStockCodes; } }

        /// <summary>
        /// Gets the TOPS implementation date
        /// </summary>
        [JsonIgnore]
        public static DateTime ImplementationDate { get { return new DateTime(1973, 8, 1); } }

        public void AddCoachingStockType(BRCoachingStockType coachingStockType)
        {
            //remove if already exists.
            if (this._coachingStockTypes.Any(cst => cst.Equals(coachingStockType)))
            {
                int index = this._coachingStockTypes.FindIndex(cst => cst.Equals(coachingStockType));
                this._coachingStockTypes.RemoveAt(index);
            }

            //add new.
            this._coachingStockTypes.Add(coachingStockType);
        }

        public void AddCoachingStockType(string code, string description)
        {
            this.AddCoachingStockType(new BRCoachingStockType()
            {
                Code = code,
                Description = description
            });
        }

        public void AddTOPSWagonCode(DateTime startDate, DateTime? endDate, string code, string description)
        {
            this._topRollingStockCodes.Add(new TOPSRollingStockCode(startDate, endDate, code, description));
        }

        public void AddTOPSCoachCode(DateTime startDate, DateTime? endDate, string code, string description, BRCoachingStockType brCoachingStockType)
        {
            this._topRollingStockCodes.Add(new TOPSRollingStockCode(startDate, endDate, code, description, brCoachingStockType));
        }

        public void AddTOPSCoachCode(DateTime startDate, DateTime? endDate, string code, string description, string brCoachingStockTypeCode)
        {
            int index = this._coachingStockTypes.FindIndex(cst => cst.Code.Equals(brCoachingStockTypeCode, StringComparison.OrdinalIgnoreCase));

            if (index.Equals(-1))
            {
                throw new ArgumentException($"The code {brCoachingStockTypeCode} supplied in the {nameof(brCoachingStockTypeCode)} argument is not a valid BR Coaching Stock Type code.");
            }

            this.AddTOPSCoachCode(startDate, endDate, code, description, this._coachingStockTypes[index]);
        }


        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static TOPS Build(string json)
        {
            return JsonConvert.DeserializeObject<TOPS>(json);

        }
    }
}
