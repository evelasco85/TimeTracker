using Domain;
using Domain.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter
{
    public interface IActivityView : IView<Activity>
    {
        Action<IEnumerable<Activity>> GetActivityData { get; set; }
        Action<dynamic, DateTime> OnGetActivityDataCompletion { get; set; }
    }
}
