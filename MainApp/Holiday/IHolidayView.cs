using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IHolidayView : IView<Holiday, IHolidayRequests>, IHolidayEvents
    {
    }

    public interface IHolidayEvents
    {
        void OnGetHolidayDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IHolidayRequests
    {
        void GetHolidayData(IEnumerable<Holiday> holidays);
    }
}
