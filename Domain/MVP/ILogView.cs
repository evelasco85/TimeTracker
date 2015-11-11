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
    }
}
