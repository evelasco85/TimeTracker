using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.FrontBase
{
    public interface IControllerManager
    {

    }

    public class ControllerManager : IControllerManager
    {
        static IControllerManager _instance;
        static readonly object _threadsafeLock = new object();

        private ControllerManager() { }

        public static IControllerManager GetInstance()
        {
            lock (_threadsafeLock)
            {
                if (_instance == null)
                    _instance = new ControllerManager();

                return _instance;
            }
        }
    }
}
