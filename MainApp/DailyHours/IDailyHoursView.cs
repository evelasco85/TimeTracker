using System;
using System.Collections.Generic;
using Domain;
using Domain.Views;

namespace MainApp.DailyHours
{
    public interface IDailyHoursView : IView<LogEntry, IDailyHoursRequests>,
        IViewControllerEvents<LogEntry>
    {
        void OnGetDailyRecordDataCompletion(dynamic displayColumns);
        void OnGetLogsForDateCompletion(dynamic displayColumns);
    }

    public interface IDailyHoursRequests : IViewControllerRequests<LogEntry>
    {
        void GetDailyRecordData(IEnumerable<LogEntry> logEntries, DateTime selectedDate);

        void GetLogsForDate(IEnumerable<LogEntry> logEntries, DateTime selectedDate);
    }
}
