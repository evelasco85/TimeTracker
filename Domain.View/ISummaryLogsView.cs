using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ISummaryLogsView : IView<LogEntry>
    {
        Action<IEnumerable<LogEntry>, DateTime> GetLogEntries { get; set; }
        Action<dynamic> OnGetLogEntriesCompletion { get; set; }
    }
}
