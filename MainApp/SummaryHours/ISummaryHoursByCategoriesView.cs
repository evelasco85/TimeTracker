using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ISummaryHoursByCategoriesView : IView<LogEntry, ISummaryHoursByCategoriesRequests>
        , IViewControllerEvents<LogEntry>
    {
        void OnGetLogEntriesCompletion(dynamic summarizedLogEntries);
    }

    public interface ISummaryHoursByCategoriesRequests : IViewControllerRequests<LogEntry>
    {
        void GetLogEntries(IEnumerable<LogEntry> logs, DateTime selectedMonth);
    }
}
