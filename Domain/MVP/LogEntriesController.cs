using Domain.Helpers;
using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MVP
{
    public class LogEntriesController : BaseController<LogEntry>
    {
        public const int ID_INDEX = 0;
        public const int CREATED_INDEX = 1;
        public const int TIME_INDEX = 2;
        public const int DAY_INDEX = 3;
        public const int CATEGORY_INDEX = 4;
        public const int DESCRIPTION_INDEX = 5;
        public const int SYSTEM_CREATED_INDEX = 6;
        public const int SYSTEM_UPDATED_INDEX = 7;
        public const string NO_CATEGORY = "No Category";
        public const string NO_DESCRIPTION = "No Description";
        public const string WEEKEND = "Weekend";

        public LogEntriesController(IEFRepository repository, IView<LogEntry> view) : base(repository, view)
        {
        }

        public override void GetData(Func<LogEntry, bool> criteria)
        {
            IQueryable<LogEntry> logQuery = this._repository
                .GetEntityQuery<LogEntry>();

            if (criteria == null)
                this._view.ViewQueryResult = logQuery.Select(x => x);
            else
                this._view.ViewQueryResult = logQuery.Where(criteria);
        }

        public override void SaveData(LogEntry data)
        {
            this._repository.Save<LogEntry>(item => item.Id, data);
        }

        public override void DeleteData(Func<LogEntry, bool> criteria)
        {
            throw new NotImplementedException("No implementation for Delete of log");
        }
    }
}
