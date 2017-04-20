using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IDailyAttributeView : IViewDeprecated<DayAttribute, IDailyAttributeRequests>
    {
        void OnGetPresetAttributeDataCompletion(IEnumerable<Domain.Attribute> attributes);
        void OnGetDailyAttributeDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IDailyAttributeRequests
    {
        void GetPresetAttributeData();
        void GetDailyAttributeData(IEnumerable<DayAttribute> dailyAttribute);
    }
}
