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
        Action GetPresetAttributeData { get; set; }
        Action<IEnumerable<Domain.Attribute>> OnGetPresetAttributeDataCompletion { get; set; }
        Action<IEnumerable<DayAttribute>> GetDailyAttributeData { get; set; }
        Action<dynamic, DateTime> OnGetDailyAttributeDataCompletion { get; set; }
    }
}
