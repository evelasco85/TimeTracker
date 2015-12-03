using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Domain.Views
{
    //Operations available to views (and Forms)
    public interface IView<TModel>
    {
        Action<Func<TModel, bool>> View_QueryRecords { get; set; }
        Action View_OnQueryRecordsCompletion { get; set; }
        Action<TModel> View_SaveRecord { get; set; }
        Action<Func<TModel, bool>> View_DeleteRecords { get; set; }
        IEnumerable<TModel> View_QueryResults { get; set; }
        Action<object> View_ViewReady { get; set; }
        Action<object> View_OnViewReady { get; set; }
        Action View_OnShow { get; set; }
    } 
}
