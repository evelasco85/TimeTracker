using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Controllers
{
    public class LogEntriesController : BaseController<LogEntry>, ILogRequests
    {
        public const int cID = 1 << 5;
        public override int ID { get { return cID; } }

        public const int ID_INDEX = 0;
        public const int CREATED_INDEX = 1;
        public const int TIME_INDEX = 2;
        public const int DAY_INDEX = 3;
        public const int CATEGORY_INDEX = 4;
        public const int DESCRIPTION_INDEX = 5;
        public const int SYSTEM_CREATED_INDEX = 6;
        public const int SYSTEM_UPDATED_INDEX = 7;
        public const int HOURS_RENDERED_INDEX = 8;
        public const string NO_CATEGORY = "No Category";
        public const string NO_DESCRIPTION = "No Description";
        public const string WEEKEND = "Weekend";
        public const string LEAVE = "LEAVE";
        public const string HOLIDAY = "HOLIDAY";

        ILogView _logView;
        IDateHelper _helper;
        bool _rememberSetting = true;
        DateTime _rememberedCreatedDateTime;

        public override bool HandleRequest(ModelViewPresenter.MessageDispatcher.Telegram telegram)
        {
            throw new NotImplementedException();
        }

        public LogEntriesController(IEFRepository repository, ILogView view)
            : base(repository)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._logView = view;
        }

        public IEnumerable<Category> GetCategories()
        {
            IQueryable<Category> categoryQuery = this._repository.GetEntityQuery<Category>();

            return categoryQuery;
        }

        public IEnumerable<Category> GetTaskEntryCategories()
        {
            IQueryable<Category> categoryQuery = this._repository
                .GetEntityQuery<Category>()
                .Where(category => category.ShowInTaskEntry);

            return categoryQuery;
        }

        public bool GetRememberedSetting() { return this._rememberSetting; }
        public void SetRememberedSetting(bool rememberSetting) { this._rememberSetting = rememberSetting; }

        public DateTime GetRememberedDate() { return this._rememberedCreatedDateTime; }
        public void SetRememberedDate(DateTime date) { this._rememberedCreatedDateTime = date; }        
        
        public void GetObjectiveData(DateTime dateTime)
        {
            DateTime dateOnly = dateTime.Date;

            IQueryable<Objective> objectiveQuery = this._repository.GetEntityQuery<Objective>();

            
            string objectives = string.Join(
                Environment.NewLine,
                objectiveQuery
                .Where(x =>
                    (x.Date.Day == dateOnly.Day) &&
                    (x.Date.Month == dateOnly.Month) &&
                    (x.Date.Year == dateOnly.Year)
                )
                .Select(x => x.Description)
                .ToArray()
                );

            this._logView.OnGetObjectiveDataCompletion(objectives);
        }


        public void GetCalendarData(IEnumerable<LogEntry> logs, DateTime selectedMonth, string category)
        {
            IEnumerable<Leave> leaves = this._repository
                .GetEntityQuery<Leave>()
                .Where(x => (x.Date.Month == selectedMonth.Month) && (x.Date.Year == selectedMonth.Year));
            IEnumerable<Holiday> holidays = this._repository
                .GetEntityQuery<Holiday>()
                .Where(x => (x.Date.Month == selectedMonth.Month) && (x.Date.Year == selectedMonth.Year));

            IList<LogEntry> leaveLogEntries = leaves.Select(x =>
                new LogEntry
                {
                    Category = LogEntriesController.LEAVE,
                    Created = x.Date,
                    System_Created = x.SystemCreated,
                    SystemUpdateDateTime = x.SystemUpdated,
                    Id = 0,
                    Description = x.Description
                })
                .ToList();

            IList<LogEntry> holidayLogEntries = holidays.Select(x =>
                new LogEntry
                {
                    Category = LogEntriesController.HOLIDAY,
                    Created = x.Date,
                    System_Created = x.SystemCreated,
                    SystemUpdateDateTime = x.SystemUpdated,
                    Id = 0,
                    Description = x.Description
                })
                .ToList();

            IList<LogEntry> availableLogEntries = this._helper
                .GetMonthLogs(logs, selectedMonth);

            IList<LogEntry> missingLogEntries = this._helper
                .GenerateMissingEntriesForMissingDates(availableLogEntries, selectedMonth, holidayLogEntries, leaveLogEntries);

            List<LogEntry> logEntries = new List<LogEntry>();

            logEntries.AddRange(availableLogEntries);
            logEntries.AddRange(missingLogEntries);
            logEntries.AddRange(leaveLogEntries);
            logEntries.AddRange(holidayLogEntries);

            if(!string.IsNullOrEmpty(category))
            {
                logEntries = logEntries
                    .Where(x => x.Category.ToUpper() == category.ToUpper())
                    .ToList();
            }

            var displayColumns = logEntries
                .Select(LogEntriesController.GetDisplayColumns)
                .OrderByDescending(x => x.Created)
                .ToList();

            DateTime lastUpdatedDate = availableLogEntries
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            IList<string> categories = logEntries.Select(x => x.Category).Distinct().ToList();

            this._logView.OnGetCalendarDataCompletion(categories, displayColumns, lastUpdatedDate);
            this.GetLogStatistics(logEntries, selectedMonth);
        }

        public static dynamic GetDisplayColumns(LogEntry log)
        {
            return new
            {
                log.Id,
                log.Created,
                Time = log.Created,
                Day = log.Created,
                log.Category,
                log.Description,
                log.System_Created,
                log.SystemUpdateDateTime,
                log.HoursRendered
            };
        }

        public void GetLogStatistics(IEnumerable<LogEntry> logs, DateTime selectedMonth)
        {
            int year = selectedMonth.Year;
            int month = selectedMonth.Month;
            IList<LogEntry> logEntries = this._helper.GetMonthLogs(logs, selectedMonth);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime startDate = this._helper.GetStartDate(selectedMonth);
            DateTime endDate = this._helper.GetEndDate(startDate);
            int saturdayCount = this._helper.CountDaysByDayName(DayOfWeek.Saturday, startDate, endDate);
            int sundayCount = this._helper.CountDaysByDayName(DayOfWeek.Sunday, startDate, endDate);
            double totalHours = this._helper.GetHours(logs, selectedMonth);

            Func<DateTime, bool> betweenMonthDates = (currentDate) => ((currentDate.Ticks > startDate.Ticks) && (currentDate.Ticks < endDate.AddDays(1).Ticks));
            IEnumerable<Holiday> holidays = this.QueryHolidays(holiday => betweenMonthDates(holiday.Date));
            IEnumerable<Leave> leaves = this.QueryLeaves(leave => betweenMonthDates(leave.Date));

            //Excluding Saturdays/Sundays
            int holidayCountExcludingWeekend = holidays.Where(holiday => !this._helper.WeekendDate(holiday.Date)).Count();
            int leaveCountExcludingWeekend = leaves.Where(leave => !this._helper.WeekendDate(leave.Date)).Count();
            IEnumerable<DateTime> uniqueLogDates = logEntries
                .GroupBy(x => x.Created.Date)
                .Distinct()
                .Select(x => x.Key);
           

            int uniqueLogEntriesPerDate = uniqueLogDates
                .Where(x =>
                    //Remove collisions (exclude partial or half-day log messages from counting)
                    !(
                        (holidays.Any(holiday => this._helper.DateEquivalent(holiday.Date, x.Date))) ||
                        (leaves.Any(leave => this._helper.DateEquivalent(leave.Date, x.Date))))
                    )
                .Count();

            int workdaysCount = (daysInMonth - (saturdayCount + sundayCount + holidayCountExcludingWeekend + leaveCountExcludingWeekend));
            int daysCountWithoutLogs = workdaysCount - uniqueLogEntriesPerDate;

            this._logView.OnGetLogStatisticsCompletion(holidayCountExcludingWeekend, leaveCountExcludingWeekend, saturdayCount, sundayCount, workdaysCount, daysInMonth, uniqueLogEntriesPerDate, daysCountWithoutLogs, totalHours);
        }

        public override void GetData(Func<LogEntry, bool> criteria)
        {
            IQueryable<LogEntry> logQuery = this._repository
                .GetEntityQuery<LogEntry>();

            if (criteria == null)
                this._logView.QueryResults = logQuery.Select(x => x);
            else
                this._logView.QueryResults = logQuery.Where(criteria);

            this._logView.OnQueryRecordsCompletion();
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
