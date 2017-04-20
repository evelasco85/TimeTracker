using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelViewPresenter.MessageDispatcher
{
    public interface IControllerManager
    {
        void RegisterController(IController controller);
        IController GetControllerFromId(int id);
        void RemoveController(IController controller);
    }

    public class ControllerManager : IControllerManager
    {
        static IControllerManager _instance = new ControllerManager();
        Dictionary<int, IController> _controllerMap = new Dictionary<int, IController>();

        private ControllerManager() { }

        public static IControllerManager GetInstance()
        {
            return _instance;
        }

        public void RegisterController(IController controller)
        {
            this._controllerMap.Add(controller.ID, controller);
        }

        public IController GetControllerFromId(int id)
        {
            return this._controllerMap[id];
        }

        public void RemoveController(IController controller)
        {
            this._controllerMap.Remove(controller.ID);
        }
    }
}
