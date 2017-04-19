using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public interface IAttributeView : IView<Domain.Attribute>
    {
        Action<IEnumerable<Domain.Attribute>> View_GetAttributeData { get; set; }
        Action<dynamic, DateTime> View_OnGetAttributeDataCompletion { get; set; }
    }
}
