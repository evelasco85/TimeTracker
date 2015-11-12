using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Controller
{
    public interface ISummaryLogsView : IView<LogEntry>
    {
        Action<IEnumerable<LogEntry>, DateTime> GetLogEntries { get; set; }
        Action<dynamic> OnGetLogEntriesCompletion { get; set; }
    }
}
