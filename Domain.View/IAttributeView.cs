using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public interface IAttributeView : IView<Domain.Attribute>
    {
        Action<IEnumerable<Domain.Attribute>> GetAttributeData { get; set; }
        Action<dynamic, DateTime> OnGetAttributeDataCompletion { get; set; }
    }
}
