using Domain.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.MessageDispatcher
{
    public interface IRequestDispatcher
    {
        void Dispatch(int sender, int receiver, Operation operation, dynamic data);

    }
    public class RequestDispatcher : IRequestDispatcher
    {
        static IRequestDispatcher _instance;
        static readonly object _threadsafeLock = new object();

        private RequestDispatcher() { }

        public static IRequestDispatcher GetInstance()
        {
            lock (_threadsafeLock)
            {
                if (_instance == null)
                    _instance = new RequestDispatcher();

                return _instance;
            }
        }

        public void Dispatch(int sender, int receiver, Operation operation, dynamic data)
        {
            IControllerManager manager = ControllerManager.GetInstance();
            IController targetController = manager.GetControllerFromId(receiver);
            Telegram telegram = new Telegram(sender, receiver, operation, data);

            this.Discharge(targetController, telegram);
        }

        void Discharge(IController receiver, Telegram telegram)
        {
            receiver.HandleRequest(telegram);
        }
    }
}
