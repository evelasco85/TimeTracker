﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ISummaryLogsView : IView<LogEntry, ISummaryLogsRequests>, IViewControllerEvents<LogEntry>
    {
        void OnGetLogEntriesCompletion(dynamic summarizedLogHours, dynamic summarizedLogEntries, double totalHours);
        void OnGetLogEntriesByCategoryCompletion(dynamic summarizedLogEntries);
    }

    public interface ISummaryLogsRequests : IViewControllerRequests<LogEntry>
    {
        void GetLogEntries(IEnumerable<LogEntry> logs, DateTime startDate, DateTime endDate);
        void GetLogEntriesByCategory(IEnumerable<LogEntry> logs, DateTime startDate, DateTime endDate, string category);
    }
}
