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
        int _sender;
        int _receiver;
        dynamic _data;
        Operation _operation;

        public int Sender
        {
            get { return _sender; }
        }

        public int Receiver
        {
            get { return _receiver; }
        }

        public dynamic Data
        {
            get { return _data; }
        }

        public Operation Operation
        {
            get { return _operation; }
        }

        public Telegram(int sender, int receiver, Operation operation, dynamic data)
        {
            this._sender = sender;
            this._receiver = receiver;
            this._operation = operation;
            this._data = data;
        }
    }
}
