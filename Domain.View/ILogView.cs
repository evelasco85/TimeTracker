using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public interface ILogView : IView<LogEntry>
    {
        Action<IEnumerable<LogEntry>, DateTime> GetLogStatistics { get; set; }
        Action<int,int,int,int,int,int,int, int> OnGetLogStatisticsCompletion { get; set; }

        Action<IEnumerable<LogEntry>, DateTime> GetCalendarData { get; set; }
        Action<dynamic, DateTime> OnGetCalendarDataCompletion { get; set; }

        Func<bool> GetRememberedSetting { get; set; }
        Action<bool> SetRememberedSetting { get; set; }

        Func<DateTime> GetRememberedDate { get; set; }
        Action<DateTime> SetRememberedDate { get; set; }
        Func<IEnumerable<Category>> GetCategories { get; set; }
    }
}
