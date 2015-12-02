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
    public class ObjectiveController : BaseController<Objective>
    {
        public const int cID = 8;
        public override int ID { get { return cID; } }

        public const int ID_INDEX = 0;
        public const int DATE_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;
        public const int SYSTEM_CREATED_INDEX = 3;
        public const int SYSTEM_UPDATED_INDEX = 4;

        IObjectiveView _objectiveView;
        IDateHelper _helper;

        public override bool HandleRequest(ModelViewPresenter.MessageDispatcher.Telegram telegram)
        {
            throw new NotImplementedException();
        }

        public ObjectiveController(IEFRepository repository, IObjectiveView view)
            : base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._objectiveView = view;
            this._objectiveView.View_GetObjectiveData = this.GetObjectiveData;

            this._manager.RegisterController(this);
        }

        void GetObjectiveData(IEnumerable<Objective> objectives)
        {
            var displayColumns = objectives.ToList(); 
            DateTime lastUpdatedDate = displayColumns
                .Select(x => x.SystemUpdated)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._objectiveView.View_OnGetObjectiveDataCompletion(displayColumns, lastUpdatedDate);
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
