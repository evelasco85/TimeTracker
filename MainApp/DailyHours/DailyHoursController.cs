using Domain;
using Domain.Controllers;
using Domain.Helpers;
using Domain.Infrastructure;
using ModelViewPresenter.MessageDispatcher;

namespace MainApp.DailyHours
{
    public class DailyHoursController : BaseController<LogEntry>, IDailyHoursRequests
    {
        public const int cID = 1 << 14;
        public override int ID { get { return cID; } }

        IDailyHoursView _dailyHoursView;
        IDateHelper _helper;

        public override bool HandleRequest(ModelViewPresenter.MessageDispatcher.Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._dailyHoursView.OnViewReady(telegram.Data);
                this._dailyHoursView.OnShow();
            }

            return true;
        }

        public DailyHoursController(IEFRepository repository, IDailyHoursView view)
            : base(repository)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._dailyHoursView = view;
        }

        public override void GetData(System.Func<LogEntry, bool> criteria)
        {
            throw new System.NotImplementedException();
        }

        public override void SaveData(LogEntry data)
        {
            throw new System.NotImplementedException();
        }

        public override void DeleteData(System.Func<LogEntry, bool> criteria)
        {
            throw new System.NotImplementedException();
        }
    }
}
