using Domain.Controllers;
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
        IList<Holiday> GetHolidays(IEnumerable<Holiday> entries);
        Holiday GetHoliday(IEnumerable<Holiday> entries, int id);
        IList<Leave> GetLeaves(IEnumerable<Leave> entries);
        Leave GetLeave(IEnumerable<Leave> entries, int id);
        IList<LogEntry> GetMonthLogs(IEnumerable<LogEntry> logEntries, DateTime selectedMonth);
        IList<LogEntry> GetMonthSummaryLogs(IEnumerable<LogEntry> logEntries, DateTime selectedMonth);
        IList<LogEntry> GenerateMissingEntriesForMissingDates(IEnumerable<LogEntry> entries, DateTime selectedMonth,
            IList<LogEntry> holidayLogEntries, IList<LogEntry> leaveLogEntries);
        bool DateEquivalent(DateTime date1, DateTime date2);
    }

    public class DateHelper : IDateHelper
    {
        static IDateHelper _instance;
        static readonly object _threadsafeLock = new object();

        private DateHelper() { }

        public static IDateHelper GetInstance()
        {
            lock (_threadsafeLock)
            {
                if (_instance == null)
                    _instance = new DateHelper();

                return _instance;
            }
        }

        public bool DateEquivalent(DateTime date1, DateTime date2)
        {
            return _dateEquivalent(date1, date2);
        }

        Func<DateTime, DateTime, bool> _dateEquivalent = (dateTime, dateTime2) => (
               (dateTime.Date.Day == dateTime2.Date.Day) &&
               (dateTime.Date.Month == dateTime2.Date.Month) &&
               (dateTime.Date.Year == dateTime2.Date.Year)
               );

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

        public IList<Holiday> GetHolidays(IEnumerable<Holiday> entries)
        {
            IList<Holiday> holidayList = entries
                .OrderBy(x => x.Date)
                .ToList();

            return holidayList;
        }

        public Holiday GetHoliday(IEnumerable<Holiday> entries, int id)
        {
            Holiday holiday = entries
                .Where(x => x.Id == id)
                .DefaultIfEmpty(null)
                .FirstOrDefault();

            return holiday;
        }

        public IList<Leave> GetLeaves(IEnumerable<Leave> entries)
        {
            IList<Leave> leaveList = entries
                .OrderBy(x => x.Date)
                .ToList();

            return leaveList;
        }

        public Leave GetLeave(IEnumerable<Leave> entries, int id)
        {
            Leave leave = entries
                .Where(x => x.Id == id)
                .DefaultIfEmpty(null)
                .FirstOrDefault();

            return leave;
        }

        public bool RecordExists(IEnumerable<Holiday> entries, int id)
        {
            bool exists = entries.Any(x => x.Id == id);

            return exists;
        }

        public IList<LogEntry> GetMonthLogs(IEnumerable<LogEntry> logEntries, DateTime selectedMonth)
        {
            IList<LogEntry> availableEntries = logEntries
                .Select(x => x)
                .Where(x => (x.Created.Month == selectedMonth.Month) &&
                    (x.Created.Year == selectedMonth.Year))
                .OrderByDescending(x => x.Created)
                .OrderByDescending(x => x.Id)
                .ToList();

            return availableEntries;
        }

        public IList<LogEntry> GetMonthSummaryLogs(IEnumerable<LogEntry> logEntries, DateTime selectedMonth)
        {
            IList<LogEntry> availableEntries = logEntries
                .Where(x => (x.Created.Month == selectedMonth.Month) &&
                    (x.Created.Year == selectedMonth.Year))
                .OrderBy(x => x.Created)
                .ToList()
                .Select(x =>
                {
                    x.Created = x.Created.Date;

                    return x;
                })
                 .ToList();

            return availableEntries;
        }

        public IList<LogEntry> GenerateMissingEntriesForMissingDates(IEnumerable<LogEntry> availableLogEntries, DateTime selectedMonth,
            IList<LogEntry> holidayLogEntries, IList<LogEntry> leaveLogEntries)
        {
            DateTime startDate = this.GetStartDate(selectedMonth);
            DateTime endDate = this.GetEndDate(startDate);
            IList<DateTime> dateRange = this.GetDateRange(startDate, endDate);

            Func<DateTime, DateTime, bool> DateEquivalent = (date1, date2) =>
                (
                    (date1.Day == date2.Day) &&
                    (date1.Month == date2.Month) &&
                    (date1.Year == date2.Year)
                );

            List<LogEntry> entries = new List<LogEntry>();

            entries.AddRange(availableLogEntries);
            entries.AddRange(holidayLogEntries);
            entries.AddRange(leaveLogEntries);

            IList<DateTime> missingDates = dateRange
                .Where(x => !entries.Any(entry =>
                    (entry.Created.Date.Day == x.Date.Day) &&
                    (entry.Created.Date.Month == x.Date.Month) &&
                    (entry.Created.Date.Year == x.Date.Year)
                    ))
                .ToList();

            IList<LogEntry> missingLogEntries = missingDates
                .Select(x => new LogEntry
                {
                    Created = x,
                    System_Created = DateTime.MinValue,
                    Category = LogEntriesController.NO_CATEGORY,
                    Description = LogEntriesController.NO_DESCRIPTION
                })
                .ToList();

            return missingLogEntries;
        }
    }
}
