using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Controllers
{
    public class SummaryLogsController : BaseController<LogEntry>, ISummaryLogsRequests
    {
        public const int cID = 1 << 2;
        public override int ID { get { return cID; } }

        public const int ID_INDEX = 0;
        public const int CREATED_INDEX = 1;
        public const int DAY_INDEX = 2;
        public const int CATEGORY_INDEX = 3;
        public const int DESCRIPTION_INDEX = 4;

        ISummaryLogsView _summaryView;
        IDateHelper _helper;

        public override bool HandleRequest(Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._summaryView.OnViewReady(telegram.Data);
                this._summaryView.OnShow();
            }

            return true;
        }

        public SummaryLogsController(IEFRepository repository, ISummaryLogsView view)
            : base(repository)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._summaryView = view;
        }

        public void GetLogEntriesByCategory(IEnumerable<LogEntry> logs, DateTime startDate, DateTime endDate, string category)
        {
            IList<LogEntry> logEntries = this._helper
                .GetMonthSummaryLogs(logs, startDate, endDate)
                .Where(x => x.Category == category)
                .ToList();
            IList<DateTime> allLogDates = _helper.GetDateRange(startDate, endDate);

            var preparedLogEntries = allLogDates
                .Select(x =>
                        logEntries
                            .Where(y => y.Created == x)
                            .Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    Created = x,
                                    @Day = x,
                                    Category = y.Category,
                                    Description = y.Description,
                                    Hour = y.HoursRendered
                                })
                            .ToList()
                )
                .ToList()
                ;

            var summarizedLogEntries = preparedLogEntries
                .SelectMany(x => x)
                .ToList();

            this._summaryView.OnGetLogEntriesByCategoryCompletion(
                summarizedLogEntries.OrderByDescending(x => x.Created).ToList()
                );
        }

        public void GetLogEntries(IEnumerable<LogEntry> logs, DateTime startDate, DateTime endDate)
        {
            IList<LogEntry> logEntries = this._helper.GetMonthSummaryLogs(logs, startDate, endDate);
            IList<DateTime> allLogDates = _helper.GetDateRange(startDate, endDate);
            IList<DateTime> workingDates = logEntries
                .Select(x => x.Created).ToList();
            IList<DateTime> datesWithoutLogs = allLogDates                          //allLogDates contains logs which includes invalid categories
                .Where(logDate => !(workingDates.Any(workDate => workDate.Date == logDate.Date)))
                .ToList();

            var preparedLogEntries = allLogDates
                .Select(x =>
                        logEntries
                            .Where(y => y.Created == x)
                            .Select(y => 
                                new {
                                    Id = y.Id,
                                    Created = x,
                                    @Day = x,
                                    Category = y.Category,
                                    Description =  y.Description,
                                    Hour = y.HoursRendered
                                })
                            .ToList()
                )
                .ToList()
                ;

            preparedLogEntries.Add(
                allLogDates
                    .Where(x => (datesWithoutLogs.Any(z => z.Date == x.Date.Date)))
                    .Select(x => new { Id = 0, Created = x.Date.Date, @Day = x.Date.Date, Category = "N/A", Description = "N/A", Hour = 0.0 })
                    .ToList()
                );

            DateTime endDate24HourResolution = endDate.AddDays(1);
            IQueryable<Leave> leaveQuery = this._repository.GetEntityQuery<Leave>()
                .Where(x => (x.Date >= startDate.Date) && (x.Date < endDate24HourResolution));
            preparedLogEntries.Add(leaveQuery.Select(x => new { Id = 0, Created = x.Date, @Day = x.Date, Category = "LEAVE", Description = "**LEAVE**   " + x.Description, Hour = 0.0 }).ToList());

            IQueryable<Holiday> holidayQuery = this._repository.GetEntityQuery<Holiday>()
                .Where(x => (x.Date >= startDate.Date) && (x.Date < endDate24HourResolution));
            preparedLogEntries.Add(holidayQuery.Select(x => new { Id = 0, Created = x.Date, @Day = x.Date, Category = "HOLIDAY", Description = "**HOLIDAY**   " + x.Description, Hour = 0.0 }).ToList());

            var summarizedLogEntries = preparedLogEntries
                .SelectMany(x => x)
                .ToList();
            var summarizedLogHoursEntries = logEntries
                .GroupBy(x => x.Category)
                .Select(x =>
                    new
                    {
                        Category = x.Key,
                        Total_Hours = x.Sum(entry => entry.HoursRendered)
                    });
            double totalHours = logEntries.Sum(x => x.HoursRendered);

            this._summaryView.OnGetLogEntriesCompletion(
                summarizedLogHoursEntries.ToList(),
                summarizedLogEntries.OrderByDescending(x => x.Created).ToList(),
                totalHours
                );
        }

        public override void GetData(Func<LogEntry, bool> criteria)
        {
            IQueryable<LogEntry> logQuery = this._repository
                .GetEntityQuery<LogEntry>();

            if (criteria == null)
                this._summaryView.QueryResults = logQuery.Select(x => x);
            else
                this._summaryView.QueryResults = logQuery.Where(criteria);

            this._summaryView.OnQueryRecordsCompletion();
        }

        public override void SaveData(LogEntry data)
        {
            throw new NotImplementedException("No implementation for saving of log summary");
        }

        public override void DeleteData(Func<LogEntry, bool> criteria)
        {
            throw new NotImplementedException("No implementation for Delete of log summary");
        }
    }
}
