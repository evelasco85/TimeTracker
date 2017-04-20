using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IAttributeView : IView<Domain.Attribute, IAttributeRequests>, IViewControllerEvents<Domain.Attribute>
    {
        void OnGetAttributeDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IAttributeRequests : IViewControllerRequests<Domain.Attribute>
    {
        void GetAttributeData(IEnumerable<Domain.Attribute> attributes);
    }
}
