using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.MessageDispatcher
{
    public interface IFrontCommand
    {
        void Initialize(int sender, object data, Operation operation);
        void Process();
    }

    public class FrontCommand : IFrontCommand
    {
        object _data;
        Operation _operation;
        int _sender, _receiver;

        public FrontCommand(int receiver)
        {
            this._receiver = receiver;
        }

        public void Initialize(int sender, object data, Operation operation)
        {
            this._sender = sender;
            this._data = data;
            this._operation = operation;
        }

        public void Process()
        {
            RequestDispatcher.GetInstance().Dispatch(this._sender, this._receiver, this._operation, this._data);
        }
    }
}
