using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ISummaryHoursByCategoriesView : IView<LogEntry, ISummaryHoursByCategoriesRequests, ISummaryHoursByCategoriesEvents>, ISummaryHoursByCategoriesEvents
    {
    }

    public interface ISummaryHoursByCategoriesEvents
    {
        void OnGetLogEntriesCompletion(dynamic summarizedLogEntries);
    }

    public interface ISummaryHoursByCategoriesRequests
    {
        void GetLogEntries(IEnumerable<LogEntry> logs, DateTime selectedMonth);
    }
}
