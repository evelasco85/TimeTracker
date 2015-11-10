using Domain.Infrastructure;
using Domain.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public interface ILogEntriesHelper
    {
        IList<LogEntry> GetMonthLogs(IEnumerable<LogEntry> logEntries, DateTime selectedMonth);
        IList<LogEntry> GetMonthSummaryLogs(IEnumerable<LogEntry> logEntries, DateTime selectedMonth);
        IList<LogEntry> GenerateMissingEntriesForMissingDates(IEnumerable<LogEntry> entries, DateTime selectedMonth);
    }

    public class LogEntriesHelper : ILogEntriesHelper
    {
        static ILogEntriesHelper _instance;
        IDateHelper _dateHelper;

        private LogEntriesHelper()
        {
            this._dateHelper = DateHelper.GetInstance();
        }

        public static ILogEntriesHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LogEntriesHelper();
            }

            return _instance;
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

        public IList<LogEntry> GenerateMissingEntriesForMissingDates(IEnumerable<LogEntry> entries, DateTime selectedMonth)
        {
            DateTime startDate = this._dateHelper.GetStartDate(selectedMonth);
            DateTime endDate = this._dateHelper.GetEndDate(startDate);
            IList<DateTime> dateRange = this._dateHelper.GetDateRange(startDate, endDate);


            IList<DateTime> missingDates = dateRange
                .Where(x => !entries.Any(entry => entry.Created.Date == x.Date))
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
