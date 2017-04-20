using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Controllers
{
    public class ObjectiveController : BaseControllerDeprecated<Objective>, IObjectiveRequests
    {
        public const int cID = 1 << 4;
        public override int ID { get { return cID; } }

        public const int ID_INDEX = 0;
        public const int DATE_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;
        public const int SYSTEM_CREATED_INDEX = 3;
        public const int SYSTEM_UPDATED_INDEX = 4;

        IObjectiveView _objectiveView;
        IDateHelper _helper;

        public override bool HandleRequest(Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._objectiveView.View_ViewReady(telegram.Data);
                this._objectiveView.View_OnShow();
            }

            return true;
        }

        public ObjectiveController(IEFRepository repository, IObjectiveView view)
            : base(repository, view)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._objectiveView = view;
            this._objectiveView.View_ViewReady = ViewReady;
        }

        void ViewReady(dynamic data)
        {
            this._objectiveView.View_OnViewReady(data);
        }

        public void GetObjectiveData(IEnumerable<Objective> objectives)
        {
            var displayColumns = objectives.OrderByDescending(x => x.Date).ToList(); 
            DateTime lastUpdatedDate = displayColumns
                .Select(x => x.SystemUpdated)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._objectiveView.OnGetObjectiveDataCompletion(displayColumns, lastUpdatedDate);
        }

        public override void GetData(Func<Objective, bool> criteria)
        {
            IQueryable<Objective> objectiveQuery = this._repository
                .GetEntityQuery<Objective>();

            if (criteria == null)
                this._view.View_QueryResults = objectiveQuery.Select(x => x);
            else
                this._view.View_QueryResults = objectiveQuery.Where(criteria);

            this._view.View_OnQueryRecordsCompletion();
        }

        public override void SaveData(Objective data)
        {
            this._repository.Save<Objective>(item => item.Id, data);
        }

        public override void DeleteData(Func<Objective, bool> criteria)
        {
            this._repository.Delete(criteria);
        }
    }
}
