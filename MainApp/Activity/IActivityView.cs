using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IActivityView : IView<Activity, IActivityRequests>, IViewControllerEvents<Activity>
    {
        void OnGetActivityDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IActivityRequests : IViewControllerRequests<Activity>
    {
        void GetActivityData(IEnumerable<Activity> attributes);
    }
}
