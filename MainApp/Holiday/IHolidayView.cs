using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IHolidayView : 
        IView<Holiday, IHolidayRequests>,
        IViewControllerEvents<Holiday>
    {
        void OnGetHolidayDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IHolidayRequests : IViewControllerRequests<Holiday>
    {
        void GetHolidayData(IEnumerable<Holiday> holidays);
    }
}
