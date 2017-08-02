using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Controllers;
using Domain.Helpers;
using Domain.Infrastructure;
using ModelViewPresenter.MessageDispatcher;

namespace MainApp.DailyHours
{
    public class DailyHoursController : BaseController<LogEntry>, IDailyHoursRequests
    {
        public const int cID = 1 << 14;
        public override int ID { get { return cID; } }

        IDailyHoursView _dailyHoursView;
        IDateHelper _helper;

        public override bool HandleRequest(ModelViewPresenter.MessageDispatcher.Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._dailyHoursView.OnViewReady(telegram.Data);
                this._dailyHoursView.OnShow();
            }

            return true;
        }

        public DailyHoursController(IEFRepository repository, IDailyHoursView view)
            : base(repository)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._dailyHoursView = view;
        }

        public override void GetData(System.Func<LogEntry, bool> criteria)
        {
            IQueryable<LogEntry> dailyHoursQuery = this._repository
                .GetEntityQuery<LogEntry>();

            if (criteria == null)
                this._dailyHoursView.QueryResults = dailyHoursQuery.Select(x => x);
            else
                this._dailyHoursView.QueryResults = dailyHoursQuery.Where(criteria);

            this._dailyHoursView.OnQueryRecordsCompletion();
        }

        public override void SaveData(LogEntry data)
        {
            throw new System.NotImplementedException();
        }

        public override void DeleteData(System.Func<LogEntry, bool> criteria)
        {
            throw new System.NotImplementedException();
        }

        public void GetDailyRecordData(IEnumerable<LogEntry> logEntries, DateTime selectedDate)
        {
            var displayColumns = logEntries
                .Select(LogEntriesController.GetDisplayColumns)
                .Where(log =>DateHelper.GetInstance().DateEquivalent(log.Created, selectedDate) )
                .OrderBy(x => x.Id)
                .ToList();

            this._dailyHoursView.OnGetDailyRecordDataCompletion(displayColumns);
        }

        public void GetLogsForDate(IEnumerable<LogEntry> logEntries, DateTime selectedDate)
        {
            var displayColumns = logEntries
                .Select(LogEntriesController.GetDisplayColumns)
                .Where(log => DateHelper.GetInstance().DateEquivalent(log.Created, selectedDate))
                .OrderBy(x => x.Id)
                .ToList();

            double hoursRecorded = displayColumns
                .Select(x => (double) x.HoursRendered)
                .ToList()
                .Sum();

            this._dailyHoursView.OnGetLogsForDateCompletion(displayColumns, hoursRecorded);
        }
    }
}
