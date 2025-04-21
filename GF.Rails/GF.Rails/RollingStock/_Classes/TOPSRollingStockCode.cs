using System;

namespace GF.Rails.RollingStock
{
    public class TOPSRollingStockCode : IRollingStockCategory<TemporalId<RollingStockCategoryId>>
    {
        public TemporalId<RollingStockCategoryId> Id { get; }

        /// <summary>
        /// Gets or sets the code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the coach type
        /// </summary>
        public BRCoachingStockType? CoachType { get; set; }

        /// <summary>
        /// Instantiates a new <see cref="TOPSRollingStockCode"/>
        /// </summary>
        /// <param name="startDate">The date the code took effect.</param>
        /// <param name="endDate">The date the code ceased (leave null if current)</param>
        /// <param name="code">The TOPS code (usually 3 letters)</param>
        /// <param name="description">A description</param>
        private TOPSRollingStockCode (DateTime startDate, DateTime? endDate, string code, string description, BRCoachingStockType? coachType = null)
        {
            this.Id = new TemporalId<RollingStockCategoryId>(new RollingStockCategoryId(Guid.NewGuid()), startDate, endDate);
            this.Code = code.ToUpper();
            this.Description = description;
            this.CoachType = coachType;
        }

        /// <summary>
        /// Instantiates a new <see cref="TOPSRollingStockCode"/> for a wagon
        /// </summary>
        /// <param name="startDate">The date the code took effect.</param>
        /// <param name="endDate">The date the code ceased (leave null if current)</param>
        /// <param name="code">The TOPS code (usually 3 letters)</param>
        /// <param name="description">A description</param>
        public TOPSRollingStockCode(DateTime startDate, DateTime? endDate, string code, string description) : this(startDate, endDate, code, description, null) { }

        /// <summary>
        /// Instantiates a new <see cref="TOPSRollingStockCode"/> for a coach
        /// </summary>
        /// <param name="startDate">The date the code took effect.</param>
        /// <param name="endDate">The date the code ceased (leave null if current)</param>
        /// <param name="code">The TOPS code (usually 3 letters)</param>
        /// <param name="description">A description</param>
        /// <param name="coachType">The coach type</param>
        public TOPSRollingStockCode(DateTime startDate, DateTime? endDate, string code, string description, BRCoachingStockType coachType) : this(startDate, endDate, code, description, (BRCoachingStockType?)coachType) { }
    }
}
