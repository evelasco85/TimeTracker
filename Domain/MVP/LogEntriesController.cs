using Domain.Helpers;
using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MVP
{
    public class LogEntriesController : BaseController<LogEntry>
    {
        public const int ID_INDEX = 0;
        public const int CREATED_INDEX = 1;
        public const int TIME_INDEX = 2;
        public const int DAY_INDEX = 3;
        public const int CATEGORY_INDEX = 4;
        public const int DESCRIPTION_INDEX = 5;
        public const int SYSTEM_CREATED_INDEX = 6;
        public const int SYSTEM_UPDATED_INDEX = 7;
        public const string NO_CATEGORY = "No Category";
        public const string NO_DESCRIPTION = "No Description";
        public const string WEEKEND = "Weekend";

        ILogView _logView;
        IDateHelper _helper;

        public LogEntriesController(IEFRepository repository, ILogView view)
            : base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._logView = view;
            this._logView.GetLogStatistics = this.GetLogStatistics;
            this._logView.GetCalendarData = this.GetCalendarData;
        }

        void GetCalendarData(IEnumerable<LogEntry> logs, DateTime selectedMonth)
        {
            IList<LogEntry> availableLogEntries = this._helper.GetMonthLogs(logs, selectedMonth);
            IList<LogEntry> missingLogEntries = this._helper.GenerateMissingEntriesForMissingDates(availableLogEntries, selectedMonth);

            List<LogEntry> logEntries = new List<LogEntry>();

            logEntries.AddRange(availableLogEntries);
            logEntries.AddRange(missingLogEntries);

            var displayColumns = logEntries
                .Select(x => new
                {
                    x.Id,
                    x.Created,
                    Time = x.Created,
                    Day = x.Created,
                    x.Category,
                    x.Description,
                    x.System_Created,
                    x.SystemUpdateDateTime,
                })
                .OrderByDescending(x => x.Created)
                .ToList();

            DateTime lastUpdatedDate = availableLogEntries
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._logView.OnGetCalendarDataCompletion(displayColumns, lastUpdatedDate);
        }

        void GetLogStatistics(IEnumerable<LogEntry> logs, DateTime selectedMonth)
        {
            int year = selectedMonth.Year;
            int month = selectedMonth.Month;
            IList<LogEntry> logEntries = this._helper.GetMonthLogs(logs, selectedMonth);
            int uniqueLogEntriesPerDate = logEntries.GroupBy(x => x.Created.Date).Distinct().Count();
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime startDate = this._helper.GetStartDate(selectedMonth);
            DateTime endDate = this._helper.GetEndDate(startDate);
            int saturdayCount = this._helper.CountDaysByDayName(DayOfWeek.Saturday, startDate, endDate);
            int sundayCount = this._helper.CountDaysByDayName(DayOfWeek.Sunday, startDate, endDate);

            Func<DateTime, bool> betweenMonthDates = (currentDate) => ((currentDate.Ticks > startDate.Ticks) && (currentDate.Ticks < endDate.AddDays(1).Ticks));

            //Excluding Saturdays/Sundays
            int holidayCount = this.QueryHolidays(holiday =>
                betweenMonthDates(holiday.Date) && (!this._helper.WeekendDate(holiday.Date))
                )
                .Count();

            int leaveCount = this.QueryLeaves(leave =>
                betweenMonthDates(leave.Date) && (!this._helper.WeekendDate(leave.Date))
                )
                .Count();
            int workdaysCount = (daysInMonth - (saturdayCount + sundayCount + holidayCount + leaveCount));

            this._logView.OnGetLogStatisticsCompletion(holidayCount, leaveCount, saturdayCount, sundayCount, workdaysCount, daysInMonth, uniqueLogEntriesPerDate);
        }

        public override void GetData(Func<LogEntry, bool> criteria)
        {
            IQueryable<LogEntry> logQuery = this._repository
                .GetEntityQuery<LogEntry>();

            if (criteria == null)
                this._view.ViewQueryResult = logQuery.Select(x => x);
            else
                this._view.ViewQueryResult = logQuery.Where(criteria);

            this._view.OnQueryViewRecordsCompletion();
        }

        IEnumerable<Holiday> QueryHolidays(Func<Holiday, bool> criteria)
        {
            IQueryable<Holiday> holidayQuery = this._repository
                .GetEntityQuery<Holiday>();
            IEnumerable<Holiday> results = new List<Holiday>();

            if (criteria == null)
                results = holidayQuery.Select(x => x);
            else
                results = holidayQuery.Where(criteria);

            return results;
        }

        IEnumerable<Leave> QueryLeaves(Func<Leave, bool> criteria)
        {
            IQueryable<Leave> leaveQuery = this._repository
                .GetEntityQuery<Leave>();
            IEnumerable<Leave> results = new List<Leave>();

            if (criteria == null)
                results = leaveQuery.Select(x => x);
            else
                results = leaveQuery.Where(criteria);

            return results;
        }
 
        public override void SaveData(LogEntry data)
        {
            this._repository.Save<LogEntry>(item => item.Id, data);
        }

        public override void DeleteData(Func<LogEntry, bool> criteria)
        {
            throw new NotImplementedException("No implementation for Delete of log");
        }
    }
}
