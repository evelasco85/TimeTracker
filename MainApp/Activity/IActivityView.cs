using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IActivityView : IView<Activity, IActivityRequests>
    {
        void OnGetActivityDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IActivityRequests
    {
        void GetActivityData(IEnumerable<Activity> attributes);
    }
}
