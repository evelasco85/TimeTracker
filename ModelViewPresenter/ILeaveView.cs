using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Controller
{
    public interface ILeaveView : IView<Leave>
    {
        Action<IEnumerable<Leave>> GetLeaveData { get; set; }
        Action<dynamic, DateTime> OnGetLeaveDataCompletion { get; set; }
    }
}
