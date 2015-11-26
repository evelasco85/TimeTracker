using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface ICategoryView : IView<Category>
    {
        Action<IEnumerable<Category>> GetCategoryData { get; set; }
        Action<dynamic, DateTime> OnGetCategoryDataCompletion { get; set; }
    }
}
