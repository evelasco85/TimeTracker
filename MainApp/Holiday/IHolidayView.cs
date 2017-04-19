using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public interface IHolidayView : IView<Holiday>
    {
        Action<IEnumerable<Holiday>> View_GetHolidayData { get; set; }
        Action<dynamic, DateTime> View_OnGetHolidayDataCompletion { get; set; }
    }
}
