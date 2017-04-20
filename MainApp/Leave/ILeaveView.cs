using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ILeaveView : IView<Leave, ILeaveRequests>, IViewControllerEvents<Leave>
    {
        void OnGetLeaveDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface ILeaveRequests : IViewControllerRequests<Leave>
    {
        void GetLeaveData(IEnumerable<Leave> leaves);
    }
}
