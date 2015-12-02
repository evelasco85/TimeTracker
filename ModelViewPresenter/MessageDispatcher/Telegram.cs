using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.MessageDispatcher
{
    public struct Telegram
    {
        public int _sender;
        public int _receiver;
        public dynamic _data;

        public Telegram(int sender, int receiver, dynamic data)
        {
            this._sender = sender;
            this._receiver = receiver;
            this._data = data;
        }
    }
}
