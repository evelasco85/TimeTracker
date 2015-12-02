using Domain.Infrastructure;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Controllers
{
    public abstract class BaseController<TEntity> : IController<TEntity>
    {
        protected IView<TEntity> _view;
        protected IEFRepository _repository;

        public abstract int ID { get; set; }

        public IView<TEntity> View
        {
            get { return this._view; }
            set { this._view = value; }
        }

        public BaseController(IEFRepository repository, IView<TEntity> view)
        {
            this.BaseMap(repository, view);
        }

        void BaseMap(IEFRepository repository, IView<TEntity> view)
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
    }
}
