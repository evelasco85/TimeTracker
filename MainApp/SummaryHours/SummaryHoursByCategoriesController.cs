using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Controllers
{
    public class SummaryHoursByCategoriesController : BaseControllerDeprecated<LogEntry>, ISummaryHoursByCategoriesRequests
    {
        public const int cID = 1 << 13;
        public override int ID { get { return cID; } }

        public const int CATEGORY_INDEX = 0;
        public const int HOURS_INDEX = 1;

        ISummaryHoursByCategoriesView _summaryView;
        IDateHelper _helper;

        public override bool HandleRequest(Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._summaryView.View_ViewReady(telegram.Data);
                this._summaryView.View_OnShow();
            }

            return true;
        }

        public SummaryHoursByCategoriesController(IEFRepository repository, ISummaryHoursByCategoriesView view)
            : base(repository, view)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._summaryView = view;
            this._summaryView.View_ViewReady = ViewReady;
        }
        
        void ViewReady(dynamic data)
        {
            this._summaryView.View_OnViewReady(data);
        }

        public void GetLogEntries(IEnumerable<LogEntry> logs, DateTime selectedMonth)
        {
            IList<LogEntry> logEntries = this._helper.GetMonthSummaryLogs(logs, selectedMonth);

            var summarizedLogEntries = logEntries
                .GroupBy(x => x.Category)
                .Select(x =>
                    new
                    {
                        Category = x.Key,
                        Total_Hours = x.Sum(entry => entry.HoursRendered)
                    });

            this._summaryView.OnGetLogEntriesCompletion(summarizedLogEntries.ToList());
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
