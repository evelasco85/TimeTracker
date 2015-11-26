using Domain;
using Domain.Helpers;
using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Views;

namespace Domain.Controllers
{
    public class CategoryController : BaseController<Category>
    {
        ICategoryView _categoryView;
        IDateHelper _helper;

        public const int ID_INDEX = 0;
        public const int NAME_INDEX = 0;
        public const int SHOW_IN_SUMMARY_INDEX = 0;
        public const int SYSTEMCREATED_INDEX = 0;
        public const int SYSTEMUPDATED_INDEX = 0;

        public CategoryController(IEFRepository repository, ICategoryView view)
            :base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._categoryView = view;
            this._categoryView.GetCategoryData = this.GetCategoryData;
        }

        void GetCategoryData(IEnumerable<Category> categories)
        {
            DateTime lastUpdatedDate = categories
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._categoryView.OnGetCategoryDataCompletion(
                categories
                .OrderBy(x => x.Id)
                .ToList()
                ,
                lastUpdatedDate);
        }

        public override void GetData(Func<Category, bool> criteria)
        {
            IQueryable<Category> categoryQuery = this._repository
                .GetEntityQuery<Category>();

            if (criteria == null)
                this._view.ViewQueryResult = categoryQuery.Select(x => x);
            else
                this._view.ViewQueryResult = categoryQuery.Where(criteria);

            this._view.OnQueryViewRecordsCompletion();
        }

        public override void SaveData(Category data)
        {
            this._repository.Save<Category>(item => item.Id, data);
        }

        public override void DeleteData(Func<Category, bool> criteria)
        {
            this._repository.Delete(criteria);
        }
    }
}
