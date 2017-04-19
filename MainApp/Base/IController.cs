using Domain.Views;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Controllers
{
    public interface IController<TModel> : IController
    {
        IView<TModel> View { get; set; }
        void GetData(Func<TModel, bool> criteria);
        void SaveData(TModel model);
        void DeleteData(Func<TModel, bool> criteria);
    }
}
