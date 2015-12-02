using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.FrontBase
{
    public interface IFrontCommand
    {
        void Initialize(int sender, int receiver, object data);
        void Dispatch();
    }
    
    public class FrontCommand : IFrontCommand
    {
        IRequestDispatcher _dispatcher;
        int _sender, _receiver;
        object _data;

        public FrontCommand()
        {
            _dispatcher = RequestDispatcher.GetInstance();
        }

        public void Initialize(int sender, int receiver, object data)
        {
            this._sender = sender;
            this._receiver = receiver;
            this._data = data;
        }

        public void Dispatch()
        {
            _dispatcher.Dispatch(_sender, _receiver, _data);
        }
    }
}
