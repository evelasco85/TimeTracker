using Domain;
using Domain.Helpers;
using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Views;
using ModelViewPresenter.MessageDispatcher;

namespace Domain.Controllers
{
    public class CategoryController : BaseController<Category>, ICategoryRequests
    {
        ICategoryView _categoryView;
        IDateHelper _helper;

        public const int cID = 1 << 10;
        public override int ID { get { return cID; } }

        public const int ID_INDEX = 0;
        public const int NAME_INDEX = 0;
        public const int SHOW_IN_SUMMARY_INDEX = 0;
        public const int SYSTEMCREATED_INDEX = 0;
        public const int SYSTEMUPDATED_INDEX = 0;

        public override bool HandleRequest(Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._categoryView.OnViewReady(telegram.Data);
                this._categoryView.OnShow();
            }

            return true;
        }
        public CategoryController(IEFRepository repository, ICategoryView view)
            :base(repository)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._categoryView = view;
        }

        public void GetCategoryData(IEnumerable<Category> categories)
        {
            DateTime lastUpdatedDate = categories
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._categoryView
                .OnGetCategoryDataCompletion(
                    categories
                        .OrderBy(x => x.Id)
                        .ToList(),
                        lastUpdatedDate);
        }

        public override void GetData(Func<Category, bool> criteria)
        {
            IQueryable<Category> categoryQuery = this._repository
                .GetEntityQuery<Category>();

            if (criteria == null)
                this._categoryView.QueryResults = categoryQuery.Select(x => x);
            else
                this._categoryView.QueryResults = categoryQuery.Where(criteria);

            this._categoryView.OnQueryRecordsCompletion();
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
