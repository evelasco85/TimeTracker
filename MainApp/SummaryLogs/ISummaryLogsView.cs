using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ISummaryLogsView : IView<LogEntry, ISummaryLogsRequests>
    {
        void OnGetLogEntriesCompletion(dynamic summarizedLogEntries);
    }

    public interface ISummaryLogsRequests
    {
        void GetLogEntries(IEnumerable<LogEntry> logs, DateTime selectedMonth);
    }
}
