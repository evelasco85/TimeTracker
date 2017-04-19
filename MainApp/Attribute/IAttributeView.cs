using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IAttributeView : IView<Domain.Attribute, IAttributeRequests, IAttributeEvents>, IAttributeEvents
    {
    }

    public interface IAttributeEvents
    {
        void OnGetAttributeDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IAttributeRequests
    {
        void GetAttributeData(IEnumerable<Domain.Attribute> attributes);
    }
}
