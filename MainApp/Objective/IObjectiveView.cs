using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public interface IObjectiveView : IView<Objective>
    {
        Action<IEnumerable<Objective>> View_GetObjectiveData { get; set; }
        Action<dynamic, DateTime> View_OnGetObjectiveDataCompletion { get; set; }
    }
}
