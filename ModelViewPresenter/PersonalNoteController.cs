using Domain;
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
    public class PersonalNoteController : BaseController<PersonalNote>
    {
        public const int ID_INDEX = 0;
        public const int DESCRIPTION_INDEX = 1;
        public const int SYSTEM_CREATED_INDEX = 2;
        public const int SYSTEM_UPDATED_INDEX = 3;

        IPersonalNoteView _view;
        IDateHelper _helper;

        public PersonalNoteController(IEFRepository repository, IPersonalNoteView view)
            : base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._view = view;
            this._view.View_GetPersonalNotes = this.GetPersonalNoteData;
        }

        void GetPersonalNoteData(IEnumerable<PersonalNote> attributes)
        {
            DateTime lastUpdatedDate = attributes
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._view.View_OnGetPersonalNotesCompletion(
                attributes
                .OrderBy(x => x.Id)
                .ToList()
                ,
                lastUpdatedDate);
        }

        public override void GetData(Func<PersonalNote, bool> criteria)
        {
            IQueryable<PersonalNote> attributeQuery = this._repository
                .GetEntityQuery<PersonalNote>();

            if (criteria == null)
                this._view.View_QueryResults = attributeQuery.Select(x => x);
            else
                this._view.View_QueryResults = attributeQuery.Where(criteria);

            this._view.View_OnQueryRecordsCompletion();
        }

        public override void SaveData(PersonalNote data)
        {
            this._repository.Save<PersonalNote>(item => item.Id, data);
        }

        public override void DeleteData(Func<PersonalNote, bool> criteria)
        {
            this._repository.Delete(criteria);
        }
    }
}
