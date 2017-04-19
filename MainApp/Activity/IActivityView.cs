using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IActivityView : 
        //IView<Activity, IActivityRequests, IActivityEvents>
                IView<Activity>
    {
        Action<IEnumerable<Activity>> View_GetActivityData { get; set; }
        Action<dynamic, DateTime> View_OnGetActivityDataCompletion { get; set; }
    }

    public interface IActivityEvents
    {

    }

    public interface IActivityRequests
    {
    }
}
