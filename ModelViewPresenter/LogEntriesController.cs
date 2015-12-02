using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Controllers
{
    public class LogEntriesController : BaseController<LogEntry>
    {
        public const int cID = 16;
        public override int ID { get { return cID; } }

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
            : base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._logView = view;
            this._logView.View_GetLogStatistics = this.GetLogStatistics;
            this._logView.View_GetCalendarData = this.GetCalendarData;
            this._logView.View_GetCategories = this.GetCategories;
            this._logView.View_GetRememberedSetting = this.GetRememberedSetting;
            this._logView.View_SetRememberedSetting = this.SetRememberedSetting;
            this._logView.View_GetRememberedDate = this.GetRememberedDate;
            this._logView.View_SetRememberedDate = this.SetRememberedDate;
            this._logView.View_GetObjectiveData = this.GetObjectiveData;

            this._manager.RegisterController(this);
        }

        IEnumerable<Category> GetCategories()
        {
            IQueryable<Category> categoryQuery = this._repository.GetEntityQuery<Category>();

            return categoryQuery;
        }
        bool GetRememberedSetting() { return this._rememberSetting; }
        void SetRememberedSetting(bool rememberSetting) { this._rememberSetting = rememberSetting; }

        DateTime GetRememberedDate() { return this._rememberedCreatedDateTime; }
        void SetRememberedDate(DateTime date) { this._rememberedCreatedDateTime = date; }        
        
        void GetObjectiveData(DateTime dateTime)
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

            this._logView.View_OnGetObjectiveDataCompletion(objectives);
        }


        void GetCalendarData(IEnumerable<LogEntry> logs, DateTime selectedMonth)
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

            this._logView.View_OnGetCalendarDataCompletion(displayColumns, lastUpdatedDate);
        }

        void GetLogStatistics(IEnumerable<LogEntry> logs, DateTime selectedMonth)
        {
            int year = selectedMonth.Year;
            int month = selectedMonth.Month;
            IList<LogEntry> logEntries = this._helper.GetMonthLogs(logs, selectedMonth);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime startDate = this._helper.GetStartDate(selectedMonth);
            DateTime endDate = this._helper.GetEndDate(startDate);
            int saturdayCount = this._helper.CountDaysByDayName(DayOfWeek.Saturday, startDate, endDate);
            int sundayCount = this._helper.CountDaysByDayName(DayOfWeek.Sunday, startDate, endDate);

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
            
            this._logView.View_OnGetLogStatisticsCompletion(holidayCountExcludingWeekend, leaveCountExcludingWeekend, saturdayCount, sundayCount, workdaysCount, daysInMonth, uniqueLogEntriesPerDate, daysCountWithoutLogs);
        }

        public override void GetData(Func<LogEntry, bool> criteria)
        {
            IQueryable<LogEntry> logQuery = this._repository
                .GetEntityQuery<LogEntry>();

            if (criteria == null)
                this._view.View_QueryResults = logQuery.Select(x => x);
            else
                this._view.View_QueryResults = logQuery.Where(criteria);

            this._view.View_OnQueryRecordsCompletion();
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
