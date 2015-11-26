﻿using Domain;
using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Controllers
{
    public class ActivityController : BaseController<Activity>
    {
        public const int ID_INDEX = 0;
        public const int NAME_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;
        public const int SYSTEM_CREATED_INDEX = 3;
        public const int SYSTEM_UPDATED_INDEX = 4;

        IActivityView _view;
        IDateHelper _helper;

        public ActivityController(IEFRepository repository, IActivityView view)
            : base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._view = view;
            this._view.GetActivityData = this.GetActivityData;
        }

        void GetActivityData(IEnumerable<Activity> attributes)
        {
            DateTime lastUpdatedDate = attributes
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._view.OnGetActivityDataCompletion(
                attributes
                .OrderBy(x => x.Id)
                .ToList()
                ,
                lastUpdatedDate);
        }

        public override void GetData(Func<Activity, bool> criteria)
        {
            IQueryable<Activity> activityQuery = this._repository
                .GetEntityQuery<Activity>();

            if (criteria == null)
                this._view.ViewQueryResult = activityQuery.Select(x => x);
            else
                this._view.ViewQueryResult = activityQuery.Where(criteria);

            this._view.OnQueryViewRecordsCompletion();
        }

        public override void SaveData(Activity data)
        {
            this._repository.Save<Activity>(item => item.Id, data);
        }

        public override void DeleteData(Func<Activity, bool> criteria)
        {
            this._repository.Delete(criteria);
        }
    }
}