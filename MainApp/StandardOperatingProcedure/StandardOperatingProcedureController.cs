using Domain;
using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Controllers
{
    public class StandardOperatingProcedureController : BaseController<StandardOperatingProcedure>, IStandardOperatingProcedureRequests
    {
        public const int cID = 1 << 12;
        public override int ID { get { return cID; } }

        public const int ID_INDEX = 0;
        public const int SUBJECT_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;
        public const int SYSTEM_CREATED_INDEX = 3;
        public const int SYSTEM_UPDATED_INDEX = 4;

        IStandardOperatingProcedureView _view;
        IDateHelper _helper;

        public override bool HandleRequest(Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._view.OnViewReady(telegram.Data);
                this._view.OnShow();
            }

            return true;
        }

        public StandardOperatingProcedureController(IEFRepository repository, IStandardOperatingProcedureView view)
            : base(repository)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._view = view;
        }

        public void GetSOP(IEnumerable<StandardOperatingProcedure> sops)
        {
            DateTime lastUpdatedDate = sops
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._view.OnGetSOPsCompletion(
                sops
                .OrderByDescending(x => x.SystemUpdateDateTime)
                .ToList()
                ,
                lastUpdatedDate);
        }

        public override void GetData(Func<StandardOperatingProcedure, bool> criteria)
        {
            IQueryable<StandardOperatingProcedure> sopQuery = this._repository
                .GetEntityQuery<StandardOperatingProcedure>();

            if (criteria == null)
                this._view.QueryResults = sopQuery.Select(x => x);
            else
                this._view.QueryResults = sopQuery.Where(criteria);

            this._view.OnQueryRecordsCompletion();
        }

        public override void SaveData(StandardOperatingProcedure data)
        {
            this._repository.Save<StandardOperatingProcedure>(item => item.Id, data);
        }

        public override void DeleteData(Func<StandardOperatingProcedure, bool> criteria)
        {
            this._repository.Delete(criteria);
        }
    }
}
