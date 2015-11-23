using Domain;
using Domain.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter
{
    public interface ICategoryView : IView<Category>
    {
        Action<IEnumerable<Category>> GetCategoryData { get; set; }
        Action<dynamic, DateTime> OnGetCategoryDataCompletion { get; set; }
    }
}
