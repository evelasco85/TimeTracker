using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ILogView : IView<LogEntry, ILogRequests>, IViewControllerEvents<LogEntry>
    {
        void OnGetLogStatisticsCompletion(int holidayCount, int leaveCount, int saturdayCount, int sundayCount, int workdaysCount, int daysInMonth, int uniqueLogEntriesPerDate, int daysCountWithoutLogs, double hoursRendered);
        void OnGetCalendarDataCompletion(IList<string> categories, dynamic displayColumns, DateTime lastUpdatedDate);
        void OnGetObjectiveDataCompletion(string objectives);
    }

    public interface ILogRequests : IViewControllerRequests<LogEntry>
    {
        void GetLogStatistics(IEnumerable<LogEntry> logs, DateTime selectedMonth);
        void GetCalendarData(IEnumerable<LogEntry> logs, DateTime selectedMonth, string category);
        bool GetRememberedSetting();
        void SetRememberedSetting(bool rememberSetting);
        DateTime GetRememberedDate();
        void SetRememberedDate(DateTime date);
        IEnumerable<Category> GetCategories();
        void GetObjectiveData(DateTime dateTime);
    }
}
