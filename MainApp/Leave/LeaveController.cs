using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Controllers
{
    public class LeaveController : BaseController<Leave>, ILeaveRequests
    {
        public const int cID = 1 << 6;
        public override int ID { get { return cID; } }

        public const int ID_INDEX = 0;
        public const int DATE_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;
        public const int SYSTEM_CREATED_INDEX = 3;
        public const int SYSTEM_UPDATED_INDEX = 4;

        ILeaveView _leaveView;
        IDateHelper _helper;

        public override bool HandleRequest(Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._leaveView.OnViewReady(telegram.Data);
                this._leaveView.OnShow();
            }

            return true;
        }

        public LeaveController(IEFRepository repository, ILeaveView view)
            : base(repository)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._leaveView = view;
        }

        public void GetLeaveData(IEnumerable<Leave> leaves)
        {
            var displayColumns = this._helper.GetLeaves(leaves);
            DateTime lastUpdatedDate = displayColumns
                .Select(x => x.SystemUpdated)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._leaveView
                .OnGetLeaveDataCompletion(displayColumns, lastUpdatedDate);
        }

        public override void GetData(Func<Leave, bool> criteria)
        {
            IQueryable<Leave> holidayQuery = this._repository
                .GetEntityQuery<Leave>();

            if (criteria == null)
                this._leaveView.QueryResults = holidayQuery.Select(x => x);
            else
                this._leaveView.QueryResults = holidayQuery.Where(criteria);

            this._leaveView.OnQueryRecordsCompletion();
        }

        public override void SaveData(Leave data)
        {
            this._repository.Save<Leave>(item => item.Id, data);
        }

        public override void DeleteData(Func<Leave, bool> criteria)
        {
            this._repository.Delete(criteria);
        }
    }
}
