using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Controllers
{
    public class SummaryLogsController : BaseController<LogEntry>
    {
        public const int cID = 2;
        public override int ID { get { return cID; } }

        public const int CREATED_INDEX = 0;
        public const int DAY_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;

        ISummaryLogsView _summaryView;
        IDateHelper _helper;

        public override bool HandleRequest(ModelViewPresenter.MessageDispatcher.Telegram telegram)
        {
            throw new NotImplementedException();
        }

        public SummaryLogsController(IEFRepository repository, ISummaryLogsView view)
            : base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._summaryView = view;
            this._summaryView.View_GetLogEntries = this.GetLogEntries;
        }
        
        void GetLogEntries(IEnumerable<LogEntry> logs, DateTime selectedMonth)
        {
            IList<LogEntry> logEntries = this._helper.GetMonthSummaryLogs(logs, selectedMonth);
            IEnumerable<Category> categories = this._repository
                .GetEntityQuery<Category>()
                .Where(x => x.ShowInSummary == true);
            var perDateAndCategoryLogs =
                logEntries
                .Where(x => (categories.Any(category => category.Name.ToLower() == x.Category.ToLower())))
                .GroupBy(log => new { Created = log.Created.ToString("yyyy-MM-dd"), Category = log.Category }, log => log.Description);

            var datesWithLogEntriesOfValidCategories =
                perDateAndCategoryLogs
                .Select(x => new
                {
                    Created = DateTime.Parse(x.Key.Created),
                    Category = x.Key.Category,
                    Description = string.Join(Environment.NewLine, x.ToList())
                })
                .ToList();

            IList<DateTime> allLogDates = logEntries
                .Select(x => x.Created)
                .Distinct()
                .ToList();
            IList<DateTime> workingDates = datesWithLogEntriesOfValidCategories      //Contains only with valid categories
                .Select(x => x.Created).ToList();
            IList<DateTime> datesWithoutLogs = allLogDates                          //allLogDates contains logs which includes invalid categories
                .Where(logDate => !(workingDates.Any(workDate => workDate.Date == logDate.Date)))
                .ToList();

            IQueryable<Leave> leaveQuery = this._repository.GetEntityQuery<Leave>()
                .Where(x => (x.Date.Month == selectedMonth.Month) && (x.Date.Year == selectedMonth.Year));
            IQueryable<Holiday> holidayQuery = this._repository.GetEntityQuery<Holiday>()
                .Where(x => (x.Date.Month == selectedMonth.Month) && (x.Date.Year == selectedMonth.Year));


            var preparedLogEntries = allLogDates
                .Select(x =>
                     datesWithLogEntriesOfValidCategories
                            .Where(y => y.Created == x)
                            .Select(y => 
                                new {
                                    Created = x,
                                    @Day = x,
                                    Category = y.Category,
                                    Description =  y.Description
                                })
                            .ToList()
                )
                .ToList()
                ;

            preparedLogEntries.Add(
                allLogDates
                    .Where(x => (datesWithoutLogs.Any(z => z.Date == x.Date.Date)))
                    .Select(x => new { Created = x.Date.Date, @Day = x.Date.Date, Category = "N/A", Description = "N/A" })
                    .ToList()
                );
            preparedLogEntries.Add(leaveQuery.Select(x => new { Created = x.Date, @Day = x.Date, Category = "LEAVE", Description = "**LEAVE**   " + x.Description }).ToList());
            preparedLogEntries.Add(holidayQuery.Select(x => new { Created = x.Date, @Day = x.Date, Category = "HOLIDAY", Description = "**HOLIDAY**   " + x.Description }).ToList());

            var summarizedLogEntries = preparedLogEntries
                .SelectMany(x => x)
                .ToList();

            this._summaryView.View_OnGetLogEntriesCompletion(summarizedLogEntries.OrderByDescending(x => x.Created).ToList());
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
