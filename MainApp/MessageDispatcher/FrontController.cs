using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.MessageDispatcher
{
    public interface IFrontController
    {
        void Process(int sender, int receiver, Operation operation, object data);
    }

    public class FrontController : IFrontController
    {
        static IFrontController _instance;
        static readonly object _threadsafeLock = new object();

        private FrontController() { }

        public static IFrontController GetInstance()
        {
            lock (_threadsafeLock)
            {
                if (_instance == null)
                    _instance = new FrontController();

                return _instance;
            }
        }

        IFrontCommand GetCommand(int receiver)
        {
            IFrontCommand command = new FrontCommand(receiver);

            return command;
        }

        public void Process(int sender, int receiver, Operation operation, object data)
        {
            IFrontCommand command = this.GetCommand(receiver);

            command.Initialize(sender, data, operation);
            command.Process();
        }
    }
}
