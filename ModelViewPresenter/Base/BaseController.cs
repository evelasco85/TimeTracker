using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Controller
{
    public abstract class BaseController<TEntity> : IController<TEntity>
    {
        protected IView<TEntity> _view;
        protected IEFRepository _repository;

        public IView<TEntity> View
        {
            get { return this._view; }
            set { this._view = value; }
        }

        public BaseController(IEFRepository repository, IView<TEntity> view)
        {
            this.Map(repository, view);
        }

        void Map(IEFRepository repository, IView<TEntity> view)
        {
            this._view = view;
            this._view.QueryViewRecords = GetData;
            this._view.SaveViewRecord = SaveData;
            this._view.DeleteViewRecords = DeleteData;
            this._repository = repository;
        }

        public abstract void GetData(Func<TEntity, bool> criteria);
        public abstract void SaveData(TEntity data);
        public abstract void DeleteData(Func<TEntity, bool> criteria);
    }
}
