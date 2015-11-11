using Domain.Helpers;
using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MVP
{
    public class LeaveController : BaseController<Leave>
    {
        public const int ID_INDEX = 0;
        public const int DATE_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;
        public const int SYSTEM_CREATED_INDEX = 3;
        public const int SYSTEM_UPDATED_INDEX = 4;

        public LeaveController(IEFRepository repository, IView<Leave> view)
            : base(repository, view)
        {
        }

        public override void GetData(Func<Leave, bool> criteria)
        {
            IQueryable<Leave> holidayQuery = this._repository
                .GetEntityQuery<Leave>();

            if (criteria == null)
                this._view.ViewQueryResult = holidayQuery.Select(x => x);
            else
                this._view.ViewQueryResult = holidayQuery.Where(criteria);

            this._view.OnQueryViewRecordsCompletion();
        }

        public override void SaveData(Leave data)
        {
            this._repository.Save<Leave>(item => item.Id, data);
        }

        public override void DeleteData(Func<Leave, bool> criteria)
        {
            this._repository.Delete(criteria);
        }
    }
}
