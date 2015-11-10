using Domain.Helpers;
using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MVP
{
    public class HolidayController : BaseController<Holiday>
    {
        public const int ID_INDEX = 0;
        public const int DATE_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;
        public const int SYSTEM_CREATED_INDEX = 3;
        public const int SYSTEM_UPDATED_INDEX = 4;

        public HolidayController(IEFRepository repository, IView<Holiday> view) : base(repository, view)
        {
        }

        public override void GetData(Func<Holiday, bool> criteria)
        {
            IQueryable<Holiday> holidayQuery = this._repository
                .GetEntityQuery<Holiday>();

            if (criteria == null)
                this._view.ViewQueryResult = holidayQuery.Select(x => x);
            else
                this._view.ViewQueryResult = holidayQuery.Where(criteria);
        }

        public override void SaveData(Holiday data)
        {
            this._repository.Save<Holiday>(item => item.Id, data);
        }

        public override void DeleteData(Func<Holiday, bool> criteria)
        {
            this._repository.Delete(criteria);
        }
    }
}
