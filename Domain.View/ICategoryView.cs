using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ICategoryView : IView<Category>
    {
        Action<IEnumerable<Category>> View_GetCategoryData { get; set; }
        Action<dynamic, DateTime> View_OnGetCategoryDataCompletion { get; set; }
    }
}
