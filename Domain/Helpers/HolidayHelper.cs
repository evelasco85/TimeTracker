using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public interface IHolidayHelper
    {
        IList<Holiday> GetHolidays(IEnumerable<Holiday> entries);
        Holiday GetHoliday(IEnumerable<Holiday> entries, int id);
    }

    public class HolidayHelper : IHolidayHelper
    {
        static IHolidayHelper _instance;

        private HolidayHelper()
        {
        }

        public static IHolidayHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new HolidayHelper();
            }

            return _instance;
        }

        public IList<Holiday> GetHolidays(IEnumerable<Holiday> entries)
        {
            IList<Holiday> holidayList = entries
                .OrderBy(x => x.Date)
                .ToList();

            return holidayList;
        }

        public Holiday GetHoliday(IEnumerable<Holiday> entries, int id)
        {
            Holiday holiday = entries
                .Where(x => x.Id == id)
                .DefaultIfEmpty(null)
                .FirstOrDefault();

            return holiday;
        }

        public bool RecordExists(IEnumerable<Holiday> entries, int id)
        {
            bool exists = entries.Any(x => x.Id == id);

            return exists;
        }
    }
}
