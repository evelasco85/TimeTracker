using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IView<TModel, TRequest> : IView<TModel>
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

//Operations available to views (and Forms)
    public interface IView<TModel>
    {
        Action<Func<TModel, bool>> View_QueryRecords { get; set; }
        Action View_OnQueryRecordsCompletion { get; set; }
        Action<TModel> View_SaveRecord { get; set; }
        Action<Func<TModel, bool>> View_DeleteRecords { get; set; }
        IEnumerable<TModel> View_QueryResults { get; set; }
        Action<dynamic> View_ViewReady { get; set; }
        Action<dynamic> View_OnViewReady { get; set; }
        Action View_OnShow { get; set; }
    } 
}
