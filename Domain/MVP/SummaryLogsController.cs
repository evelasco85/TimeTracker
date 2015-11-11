using Domain.Helpers;
using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MVP
{
    public class SummaryLogsController : BaseController<LogEntry>
    {
        public const int CREATED_INDEX = 0;
        public const int DAY_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;

        public SummaryLogsController(IEFRepository repository, IView<LogEntry> view) : base(repository, view)
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

            this._view.OnQueryViewRecordsCompletion();
        }

        public override void SaveData(LogEntry data)
        {
            throw new NotImplementedException("No implementation for saving of log summary");
        }

        public override void DeleteData(Func<LogEntry, bool> criteria)
        {
            throw new NotImplementedException("No implementation for Delete of log summary");
        }
    }
}
