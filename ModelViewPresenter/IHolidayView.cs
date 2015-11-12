using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Controller
{
    public interface IHolidayView : IView<Holiday>
    {
        Action<IEnumerable<Holiday>> GetHolidayData { get; set; }
        Action<dynamic, DateTime> OnGetHolidayDataCompletion { get; set; }
    }
}
