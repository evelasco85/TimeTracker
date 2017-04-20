using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelViewPresenter.MessageDispatcher
{
    public interface IController
    {
        int ID { get; }
        bool HandleRequest(Telegram telegram);
    }
}
