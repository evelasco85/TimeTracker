using Domain.Infrastructure;
using Domain.Views;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Controllers
{
    public interface IController<TModel> : IController, IViewControllerRequests<TModel>
    {
//For deletion
        IView<TModel> View { get; set; }
        void GetData(Func<TModel, bool> criteria);
        void SaveData(TModel model);
        void DeleteData(Func<TModel, bool> criteria);
    }

    public abstract class BaseController<TEntity> : IController<TEntity>
    {
//For deletion
        protected IView<TEntity> _view;

        protected IEFRepository _repository;

        public abstract int ID { get; }

//For deletion
        public IView<TEntity> View
        {
            get { return this._view; }
            set { this._view = value; }
        }

        public BaseController(IEFRepository repository, IView<TEntity> view)
        {
            this._view = view;
            this._view.View_QueryRecords = GetData;
            this._view.View_SaveRecord = SaveData;
            this._view.View_DeleteRecords = DeleteData;
            this._repository = repository;
        }

       

        public abstract void GetData(Func<TEntity, bool> criteria);
        public abstract void SaveData(TEntity data);
        public abstract void DeleteData(Func<TEntity, bool> criteria);
        public abstract bool HandleRequest(Telegram telegram);
    }
}
