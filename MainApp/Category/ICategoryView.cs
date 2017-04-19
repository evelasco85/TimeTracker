using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ICategoryView : IView<Category, ICategoryRequests, ICategoryEvents>, ICategoryEvents
    {
    }

    public interface ICategoryEvents
    {
        void OnGetCategoryDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface ICategoryRequests
    {
        void GetCategoryData(IEnumerable<Category> categories);
    }
}
