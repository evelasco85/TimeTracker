using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public interface ILeaveView : IView<Leave>
    {
        Action<IEnumerable<Leave>> View_GetLeaveData { get; set; }
        Action<dynamic, DateTime> View_OnGetLeaveDataCompletion { get; set; }
    }
}
