using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ISummaryHoursByCategoriesView : IView<LogEntry>
    {
        Action<IEnumerable<LogEntry>, DateTime> View_GetLogEntries { get; set; }
        Action<dynamic> View_OnGetLogEntriesCompletion { get; set; }
    }
}
