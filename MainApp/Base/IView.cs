using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IView<TModel, TRequest>
    {
        TRequest ViewRequest { get; set; }
    }

    public interface IViewControllerRequests<TModel>
    {
        void GetData(Func<TModel, bool> criteria);
        void SaveData(TModel data);
        void DeleteData(Func<TModel, bool> criteria);
    }

    public interface IViewControllerEvents<TModel>
    {
        IEnumerable<TModel> QueryResults { get; set; }
        void OnQueryRecordsCompletion();
        void OnViewReady(object data);
        void OnShow();
    }
}
