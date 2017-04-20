using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ICategoryView : IView<Category, ICategoryRequests>, IViewControllerEvents<Category>
    {
        void OnGetCategoryDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface ICategoryRequests : IViewControllerRequests<Category>
    {
        void GetCategoryData(IEnumerable<Category> categories);
    }
}
