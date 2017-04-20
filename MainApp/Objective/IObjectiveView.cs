using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IObjectiveView : IView<Objective, IObjectiveRequests, IObjectiveEvents>, IObjectiveEvents
    {
    }

    public interface IObjectiveEvents
    {
        void OnGetObjectiveDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IObjectiveRequests
    {
        void GetObjectiveData(IEnumerable<Objective> objectives);
    }
}
