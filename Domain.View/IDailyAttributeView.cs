using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public  interface IDailyAttributeView: IView<DayAttribute>
    {
        Action View_GetPresetAttributeData { get; set; }
        Action<IEnumerable<Domain.Attribute>> View_OnGetPresetAttributeDataCompletion { get; set; }
        Action<IEnumerable<DayAttribute>> View_GetDailyAttributeData { get; set; }
        Action<dynamic, DateTime> View_OnGetDailyAttributeDataCompletion { get; set; }
    }
}
