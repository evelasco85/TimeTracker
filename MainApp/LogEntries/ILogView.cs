using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ILogView : IViewDeprecated<LogEntry, ILogRequests>
    {
        void OnGetLogStatisticsCompletion(int holidayCount, int leaveCount, int saturdayCount, int sundayCount, int workdaysCount, int daysInMonth, int uniqueLogEntriesPerDate, int daysCountWithoutLogs, double hoursRendered);
        void OnGetCalendarDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
        void OnGetObjectiveDataCompletion(string objectives);
    }

    public interface ILogRequests
    {
        void GetLogStatistics(IEnumerable<LogEntry> logs, DateTime selectedMonth);
        void GetCalendarData(IEnumerable<LogEntry> logs, DateTime selectedMonth);
        bool GetRememberedSetting();
        void SetRememberedSetting(bool rememberSetting);
        DateTime GetRememberedDate();
        void SetRememberedDate(DateTime date);
        IEnumerable<Category> GetCategories();
        void GetObjectiveData(DateTime dateTime);
    }
}
