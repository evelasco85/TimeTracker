using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelViewPresenter.MessageDispatcher
{
    public interface IRequestDispatcher
    {
        void Dispatch(int sender, int receiver, Operation operation, dynamic data);

    }
    public class RequestDispatcher : IRequestDispatcher
    {
        static IRequestDispatcher _instance = new RequestDispatcher();

        private RequestDispatcher() { }

        public static IRequestDispatcher GetInstance()
        {
            return _instance;
        }

        public void Dispatch(int sender, int receiver, Operation operation, dynamic data)
        {
            IControllerManager manager = ControllerManager.GetInstance();
            IController targetController = manager.GetControllerFromId(receiver);
            Telegram telegram = new Telegram(sender, receiver, operation, data);

            targetController.HandleRequest(telegram);
        }
    }
}
