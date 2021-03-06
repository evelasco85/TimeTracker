﻿using Domain.Infrastructure;
using Domain.Views;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Controllers
{
    public interface IController<TModel> : IController, IViewControllerRequests<TModel>
    {
        void GetData(Func<TModel, bool> criteria);
        void SaveData(TModel model);
        void DeleteData(Func<TModel, bool> criteria);
    }

    public abstract class BaseController<TEntity> : IController<TEntity>
    {
        protected IEFRepository _repository;

        public abstract int ID { get; }

        public BaseController(IEFRepository repository)
        {
            this._repository = repository;
        }

        public abstract void GetData(Func<TEntity, bool> criteria);
        public abstract void SaveData(TEntity data);
        public abstract void DeleteData(Func<TEntity, bool> criteria);
        public abstract bool HandleRequest(Telegram telegram);
    }
}
