using System;
using System.Collections.Generic;

namespace GF.Rails.Operations
{
    public struct TimeTableDays
    {
        private const DayOfWeek everyday = DayOfWeek.Sunday | DayOfWeek.Monday | DayOfWeek.Tuesday | DayOfWeek.Wednesday | DayOfWeek.Thursday | DayOfWeek.Friday | DayOfWeek.Saturday;
        private const DayOfWeek weekdays = DayOfWeek.Monday | DayOfWeek.Tuesday | DayOfWeek.Wednesday | DayOfWeek.Thursday | DayOfWeek.Friday;

        private DayOfWeek _applicableDays;
        private BankHolidayOptions _bankHolidayOption;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private List<DateTime> _specificDates;
        private enum BankHolidayOptions
        {
            Ignore = 0,
            Include = 1,
            Exclude = 2
        }

        private TimeTableDays (DayOfWeek applicableDays, BankHolidayOptions bankHolidayOption = BankHolidayOptions.Ignore, DateTime? startDate = null, DateTime? endDate = null)
        {
            this._applicableDays = applicableDays;
            this._bankHolidayOption = bankHolidayOption;
            this._startDate = startDate;
            this._endDate = endDate;
            this._specificDates = new List<DateTime>();
        }

        public static TimeTableDays Everyday()
        {
            return new TimeTableDays(everyday);
        }

        public static TimeTableDays SUN()
        {
            return new TimeTableDays(DayOfWeek.Sunday);
        }

        public static TimeTableDays MO()
        {
            return new TimeTableDays(DayOfWeek.Monday);
        }

        public static TimeTableDays TO()
        {
            return new TimeTableDays(DayOfWeek.Tuesday);
        }

        public static TimeTableDays WO()
        {
            return new TimeTableDays(DayOfWeek.Wednesday);
        }

        public static TimeTableDays THO()
        {
            return new TimeTableDays(DayOfWeek.Thursday);
        }

        public static TimeTableDays FO()
        {
            return new TimeTableDays(DayOfWeek.Friday);
        }

        public static TimeTableDays SO()
        {
            return new TimeTableDays(DayOfWeek.Friday);
        }

        public static TimeTableDays BHX()
        {
            return new TimeTableDays(everyday, BankHolidayOptions.Exclude);
        }
        public static TimeTableDays SSUX()
        {
            return new TimeTableDays(weekdays, BankHolidayOptions.Exclude);
        }
    }
}
