using Domain.Helpers;
using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Controller
{
    public class SummaryLogsController : BaseController<LogEntry>
    {
        public const int CREATED_INDEX = 0;
        public const int DAY_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;

        ISummaryLogsView _summaryView;
        IDateHelper _helper;

        public SummaryLogsController(IEFRepository repository, ISummaryLogsView view)
            : base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._summaryView = view;
            this._summaryView.GetLogEntries = this.GetLogEntries;
        }
        
        void GetLogEntries(IEnumerable<LogEntry> logs, DateTime selectedMonth)
        {
            IList<LogEntry> logEntries = this._helper.GetMonthSummaryLogs(logs, selectedMonth);
            IEnumerable<IGrouping<string, string>> perDateLogs =
                logEntries
                .Where(x => !(x.Category.ToLower() == "others"))
                .GroupBy(log => log.Created.ToString("yyyy-MM-dd"), log => log.Description);

            var officialLogEntries =
                perDateLogs
                .Select(x => new
                {
                    Created = DateTime.Parse(x.Key),
                    Description = string.Join(Environment.NewLine, x.ToList())
                })
                .ToList();

            IList<DateTime> allDates = logEntries
                .Select(x => x.Created)
                .Distinct()
                .ToList();
            IList<DateTime> officialWorkDates = officialLogEntries
                .Select(x => x.Created).ToList();
            IList<DateTime> unofficialDates = allDates
                .Where(x => !(officialWorkDates.Any(y => y.Date == x.Date)))
                .ToList();

            Func<DateTime, string> getDescription = (date) =>
            {
                return (unofficialDates.Any(x => x.Date == date)) ? "N/A" :
                    string.Join(Environment.NewLine,
                    officialLogEntries
                        .Where(x => x.Created == date)
                        .Select(x => x.Description)
                        .ToList()
                        )
                    ;
            };

            var summarizedLogEntries = allDates
                .Select(x => new
                {
                    Created = x,
                    @Day = x,
                    Description = getDescription(x)
                })
                .ToList();

            this._summaryView.OnGetLogEntriesCompletion(summarizedLogEntries);
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
