using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public interface IDateHelper
    {
        bool WeekendDate(DateTime date);
        int CountDaysByDayName(DayOfWeek dayOfWeek, DateTime startDate, DateTime endDate);
        IList<DateTime> GetDateRange(DateTime startDate, DateTime endDate);
        DateTime GetStartDate(DateTime date);
        DateTime GetEndDate(DateTime date);
    }

    public class DateHelper : IDateHelper
    {
        static IDateHelper _instance;

        private DateHelper() { }

        public static IDateHelper GetInstance()
        {
            if (_instance == null)
                _instance = new DateHelper();

            return _instance;
        }

        public bool WeekendDate(DateTime date)
        {
            bool isWeekend =
                ((date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday));

            return isWeekend;
        }

        public int CountDaysByDayName(DayOfWeek dayOfWeek, DateTime startDate, DateTime endDate)
        {
            IList<DateTime> dateRange = this.GetDateRange(startDate, endDate);
            int daysCount = dateRange.Where(day => day.DayOfWeek == dayOfWeek).Count();

            return daysCount;
        }

        public IList<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
        {
            IList<DateTime> dateRange = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dateRange.Add(date);
            }

            return dateRange;
        }

        public DateTime GetStartDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public DateTime GetEndDate(DateTime date)
        {
            return date.AddMonths(1).AddDays(-1);
        }
    }
}
