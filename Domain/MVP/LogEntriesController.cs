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

        public Func<Func<Holiday, bool>, IEnumerable<Holiday>> GetHolidays { get; set; }
        public Func<Func<Leave, bool>, IEnumerable<Leave>> GetLeaves { get; set; }

        public LogEntriesController(IEFRepository repository, ILogView view)
            : base(repository, view)
        {
            this.GetHolidays = this.QueryHolidays;
            this.GetLeaves = this.QueryLeaves;
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

        IEnumerable<Holiday> QueryHolidays(Func<Holiday, bool> criteria)
        {
            IQueryable<Holiday> holidayQuery = this._repository
                .GetEntityQuery<Holiday>();
            IEnumerable<Holiday> results = new List<Holiday>();

            if (criteria == null)
                results = holidayQuery.Select(x => x);
            else
                results = holidayQuery.Where(criteria);

            return results;
        }

        IEnumerable<Leave> QueryLeaves(Func<Leave, bool> criteria)
        {
            IQueryable<Leave> leaveQuery = this._repository
                .GetEntityQuery<Leave>();
            IEnumerable<Leave> results = new List<Leave>();

            if (criteria == null)
                results = leaveQuery.Select(x => x);
            else
                results = leaveQuery.Where(criteria);

            return results;
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
