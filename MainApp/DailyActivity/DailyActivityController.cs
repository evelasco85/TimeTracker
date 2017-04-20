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
    public class DailyActivityController : BaseControllerDeprecated<DayActivity>, IDailyActivityRequests
    {
        public const int cID = 1 << 9;
        public override int ID { get { return cID; } }

        public const int ID_INDEX = 0;
        public const int DATE_INDEX = 1;
        public const int NAME_INDEX = 2;
        public const int DESCRIPTION_INDEX = 3;
        public const int DURATION_INDEX = 4;
        public const int SYSTEM_CREATED_INDEX = 5;
        public const int SYSTEM_UPDATED_INDEX = 6;

        IDailyActivityView _dayActivityView;
        IDateHelper _helper;

        public override bool HandleRequest(Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._dayActivityView.View_ViewReady(telegram.Data);
                this._dayActivityView.View_OnShow();
            }

            return true;
        }

        public DailyActivityController(IEFRepository repository, IDailyActivityView view)
            : base(repository, view)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._dayActivityView = view;
            this._dayActivityView.View_ViewReady = ViewReady;
        }

        void ViewReady(dynamic data)
        {
            this._dayActivityView.View_OnViewReady(data);
        }

        public void GetDatesForCurrentPeriod(DateTime selectedMonth)
        {
            DateTime startDate = this._helper.GetStartDate(selectedMonth);
            DateTime endDate = this._helper.GetEndDate(startDate);
            Func<DateTime, bool> betweenMonthDates = (currentDate) => ((currentDate.Ticks > startDate.Ticks) && (currentDate.Ticks < endDate.AddDays(1).Ticks));

            IEnumerable<DayActivity> dailyActivities = this.QueryDailyActivities(daily => betweenMonthDates(daily.Date));
            IEnumerable<DateTime> uniqueDailyActivityDates = dailyActivities
                .GroupBy(x => x.Date.Date)
                .Distinct()
                .OrderBy(x => x.Key)
                .Select(x => x.Key);

            this._dayActivityView
                .OnGetDatesForCurrentPeriodCompletion(uniqueDailyActivityDates);
        }

        IEnumerable<DayActivity> QueryDailyActivities(Func<DayActivity, bool> criteria)
        {
            IQueryable<DayActivity> dailyActivityQuery = this._repository
               .GetEntityQuery<DayActivity>();
            IEnumerable<DayActivity> results = new List<DayActivity>();

            if (criteria == null)
                results = dailyActivityQuery.Select(x => x);
            else
                results = dailyActivityQuery.Where(criteria);

            return results;
        }

        public void GetDailyActivityData(IEnumerable<DayActivity> dailyActivity)
        {
            DateTime lastUpdatedDate = dailyActivity
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._dayActivityView
                .OnGetDailyActivityDataCompletion(dailyActivity.ToList(), lastUpdatedDate);
        }

        public void GetPresetActivityData()
        {
            IEnumerable<Activity> attributes = this._repository
                .GetEntityQuery<Activity>();

            this._dayActivityView
                .OnGetPresetActivityDataCompletion(attributes);
        }

        public override void DeleteData(Func<DayActivity, bool> criteria)
        {
            this._repository.Delete<DayActivity>(criteria);
        }

        public override void GetData(Func<DayActivity, bool> criteria)
        {
            IQueryable<DayActivity> dayAttributeQuery = this._repository
                .GetEntityQuery<DayActivity>();

            if (criteria == null)
                this._view.View_QueryResults = dayAttributeQuery.Select(x => x);
            else
                this._view.View_QueryResults = dayAttributeQuery.Where(criteria);

            this._view.View_OnQueryRecordsCompletion();
        }

        public override void SaveData(DayActivity data)
        {
            this._repository.Save<DayActivity>(item => item.Id, data);
        }
    }
}
