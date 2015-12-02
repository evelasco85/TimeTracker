using Domain.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.FrontBase
{
    public interface IControllerManager
    {
        IController GetControllerFromId(int id);
    }

    public class ControllerManager : IControllerManager
    {
        static IControllerManager _instance;
        static readonly object _threadsafeLock = new object();
        Dictionary<int, IController> _controllerMap = new Dictionary<int, IController>();

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

        public void RegisterController(IController controller)
        {
            this._controllerMap.Add(controller.ID, controller);
        }

        public object GetControllerFromId(int id)
        {
            return this._controllerMap[id];
        }

        public void RemoveController(IController controller)
        {
            this._controllerMap.Remove(controller.ID);
        }
    }
}
