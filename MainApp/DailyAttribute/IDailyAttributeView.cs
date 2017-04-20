using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IDailyAttributeView : IView<DayAttribute, IDailyAttributeRequests>, IViewControllerEvents<DayAttribute>
    {
        void OnGetPresetAttributeDataCompletion(IEnumerable<Domain.Attribute> attributes);
        void OnGetDailyAttributeDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IDailyAttributeRequests : IViewControllerRequests<DayAttribute>
    {
        void GetPresetAttributeData();
        void GetDailyAttributeData(IEnumerable<DayAttribute> dailyAttribute);
    }
}
