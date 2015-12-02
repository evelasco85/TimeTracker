using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.FrontBase
{
    public interface IFrontController
    {
    }

    public class FrontController : IFrontController
    {
        public void Get(int sender, int receiver, object data)
        {
            FrontCommand command = new FrontCommand();

            command.Initialize(sender, receiver, data);
            command.Dispatch();
        }
    }
}
