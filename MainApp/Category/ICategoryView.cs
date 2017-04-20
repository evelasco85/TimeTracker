using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ICategoryView : IViewDeprecated<Category, ICategoryRequests>
    {
        void OnGetCategoryDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface ICategoryRequests
    {
        void GetCategoryData(IEnumerable<Category> categories);
    }
}
