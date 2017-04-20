using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ILeaveView : IView<Leave, ILeaveRequests>, ILeaveEvents
    {
    }

    public interface ILeaveEvents
    {
        void OnGetLeaveDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface ILeaveRequests
    {
        void GetLeaveData(IEnumerable<Leave> leaves);
    }
}
