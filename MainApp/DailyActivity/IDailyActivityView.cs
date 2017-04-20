using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IDailyActivityView : IView<DayActivity, IDailyActivityRequests>, IDailyActivityEvents
    {
    }

    public interface IDailyActivityEvents
    {
        void OnGetPresetActivityDataCompletion(IEnumerable<Activity> attributes);
        void OnGetDailyActivityDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
        void OnGetDatesForCurrentPeriodCompletion(IEnumerable<DateTime> uniqueDates);
    }

    public interface IDailyActivityRequests
    {
        void GetPresetActivityData();
        void GetDailyActivityData(IEnumerable<DayActivity> dailyActivity);
        void GetDatesForCurrentPeriod(DateTime selectedMonth);
    }
}
