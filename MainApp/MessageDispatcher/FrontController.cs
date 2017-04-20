using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelViewPresenter.MessageDispatcher
{
    public interface IFrontController
    {
        void Process(int sender, int receiver, Operation operation, object data);
    }

    public class FrontController : IFrontController
    {
        static IFrontController _instance = new FrontController();

        private FrontController() { }

        public static IFrontController GetInstance()
        {
            return _instance;
        }

        public void Process(int sender, int receiver, Operation operation, object data)
        {
            RequestDispatcher.GetInstance().Dispatch(sender, receiver, operation, data);
        }
    }
}
