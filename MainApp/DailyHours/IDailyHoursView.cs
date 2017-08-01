using Domain;
using Domain.Views;

namespace MainApp.DailyHours
{
    public interface IDailyHoursView : IView<LogEntry, IDailyHoursRequests>,
        IViewControllerEvents<LogEntry>
    {
    }

    public interface IDailyHoursRequests : IViewControllerRequests<LogEntry>
    {
    }
}
