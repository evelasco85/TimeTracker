﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IObjectiveView : IViewDeprecated<Objective, IObjectiveRequests>
    {
        void OnGetObjectiveDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IObjectiveRequests
    {
        void GetObjectiveData(IEnumerable<Objective> objectives);
    }
}
