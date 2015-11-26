using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public interface IDailyActivityView : IView<DayActivity>
    {
        Action View_GetPresetActivityData { get; set; }
        Action<IEnumerable<Activity>> View_OnGetPresetActivityDataCompletion { get; set; }
        Action<IEnumerable<DayActivity>> View_GetDailyActivityData { get; set; }
        Action<dynamic, DateTime> View_OnGetDailyActivityDataCompletion { get; set; }
        Action<DateTime> View_GetDatesForCurrentPeriod { get; set; }
        Action<IEnumerable<DateTime>> View_OnGetDatesForCurrentPeriodCompletion { get; set; }

    }
}
