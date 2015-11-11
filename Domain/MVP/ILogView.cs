using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MVP
{
    public interface ILogView : IView<LogEntry>
    {
        Action<IEnumerable<LogEntry>, DateTime> GetLogStatistics { get; set; }
        Action<int,int,int,int,int,int,int> OnGetLogStatisticsCompletion { get; set; }
        Action<IEnumerable<LogEntry>, DateTime> GetCalendarData { get; set; }
        Action<dynamic, DateTime> OnGetCalendarDataCompletion { get; set; }
    }
}
