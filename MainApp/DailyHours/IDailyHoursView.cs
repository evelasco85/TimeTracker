using System;
using System.Collections.Generic;
using Domain;
using Domain.Views;

namespace MainApp.DailyHours
{
    public interface IDailyHoursView : IView<LogEntry, IDailyHoursRequests>,
        IViewControllerEvents<LogEntry>
    {
        void OnGetLogsForDateCompletion(dynamic displayColumns, dynamic categorySummaryColumns, double hoursRecorded);
    }

    public interface IDailyHoursRequests : IViewControllerRequests<LogEntry>
    {
        void GetLogsForDate(IEnumerable<LogEntry> logEnumerables, DateTime selectedDate);
    }
}
