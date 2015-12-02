using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.MessageDispatcher
{
    public enum Operation
    {
        None = 0,
        OpenView = 1
    }

    public struct Telegram
    {
        public int _sender;
        public int _receiver;
        public dynamic _data;
        public Operation _operation;

        public Telegram(int sender, int receiver, Operation operation, dynamic data)
        {
            this._sender = sender;
            this._receiver = receiver;
            this._operation = operation;
            this._data = data;
        }
    }
}
