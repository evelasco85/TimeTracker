using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IHolidayView : IView<Holiday, IHolidayRequests>
    {
        void OnGetHolidayDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IHolidayEvents
    {
        
    }

    public interface IHolidayRequests
    {
        void GetHolidayData(IEnumerable<Holiday> holidays);
    }
}
