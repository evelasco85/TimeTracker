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
        Action<Func<TModel, bool>> QueryViewRecords { get; set; }
        Action OnQueryViewRecordsCompletion { get; set; }
        Action<TModel> SaveViewRecord { get; set; }
        Action<Func<TModel, bool>> DeleteViewRecords { get; set; }
        IEnumerable<TModel> ViewQueryResult { get; set; }
    } 
}
