using Domain;
using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Controllers
{
    public class DailyAttributeController : BaseController<DayAttribute>
    {
        public const int cID = 1 << 8;
        public override int ID { get { return cID; } }

        public const int ID_INDEX = 0;
        public const int DATE_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;
        public const int LINK_INDEX = 3;
        public const int SYSTEM_CREATED_INDEX = 4;
        public const int SYSTEM_UPDATED_INDEX = 5;

        IDailyAttributeView _dayAttributeView;
        IDateHelper _helper;

        public override bool HandleRequest(ModelViewPresenter.MessageDispatcher.Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._dayAttributeView.View_ViewReady(telegram.Data);
                this._dayAttributeView.View_OnShow();
            }

            return true;
        }

        public DailyAttributeController(IEFRepository repository, IDailyAttributeView view)
            : base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._dayAttributeView = view;
            this._dayAttributeView.View_GetPresetAttributeData = GetPresetAttributeData;
            this._dayAttributeView.View_GetDailyAttributeData = GetDailyAttributeData;
            this._dayAttributeView.View_ViewReady = ViewReady;
        }

        void ViewReady(dynamic data)
        {
            this._dayAttributeView.View_OnViewReady(data);
        }

        void GetDailyAttributeData(IEnumerable<DayAttribute> dailyAttribute)
        {
            DateTime lastUpdatedDate = dailyAttribute
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._dayAttributeView.View_OnGetDailyAttributeDataCompletion(dailyAttribute.ToList(), lastUpdatedDate);
        }

        void GetPresetAttributeData()
        {
            IEnumerable<Domain.Attribute> attributes = this._repository
                .GetEntityQuery<Domain.Attribute>();

            this._dayAttributeView.View_OnGetPresetAttributeDataCompletion(attributes);
        }

        public override void DeleteData(Func<DayAttribute, bool> criteria)
        {
            this._repository.Delete<DayAttribute>(criteria);
        }

        public override void GetData(Func<DayAttribute, bool> criteria)
        {
            IQueryable<DayAttribute> dayAttributeQuery = this._repository
                .GetEntityQuery<DayAttribute>();

            if (criteria == null)
                this._view.View_QueryResults = dayAttributeQuery.Select(x => x);
            else
                this._view.View_QueryResults = dayAttributeQuery.Where(criteria);

            this._view.View_OnQueryRecordsCompletion();
        }

        public override void SaveData(DayAttribute data)
        {
            this._repository.Save<DayAttribute>(item => item.Id, data);
        }
    }
}
