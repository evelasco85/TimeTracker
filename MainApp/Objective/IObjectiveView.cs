using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IObjectiveView : IView<Objective, IObjectiveRequests>, IViewControllerEvents<Objective>
    {
        void OnGetObjectiveDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IObjectiveRequests : IViewControllerRequests<Objective>
    {
        void GetObjectiveData(IEnumerable<Objective> objectives);
    }
}
