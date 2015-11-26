using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public interface ILogView : IView<LogEntry>
    {
        Action<IEnumerable<LogEntry>, DateTime> View_GetLogStatistics { get; set; }
        Action<int,int,int,int,int,int,int, int> View_OnGetLogStatisticsCompletion { get; set; }

        Action<IEnumerable<LogEntry>, DateTime> View_GetCalendarData { get; set; }
        Action<dynamic, DateTime> View_OnGetCalendarDataCompletion { get; set; }

        Func<bool> View_GetRememberedSetting { get; set; }
        Action<bool> View_SetRememberedSetting { get; set; }

        Func<DateTime> View_GetRememberedDate { get; set; }
        Action<DateTime> View_SetRememberedDate { get; set; }
        Func<IEnumerable<Category>> View_GetCategories { get; set; }
    }
}
