using Domain;
using Domain.Helpers;
using Domain.Infrastructure;
using Domain.Views;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Controllers
{
    public class PersonalNoteController : BaseController<PersonalNote>, IPersonalNoteRequests
    {
        public const int cID = 1 << 3;
        public override int ID { get { return cID; } }

        public const int ID_INDEX = 0;
        public const int SUBJECT_INDEX = 1;
        public const int DESCRIPTION_INDEX = 2;
        public const int SYSTEM_CREATED_INDEX = 3;
        public const int SYSTEM_UPDATED_INDEX = 4;

        IPersonalNoteView _view;
        IDateHelper _helper;

        public override bool HandleRequest(Telegram telegram)
        {
            if (telegram.Operation == Operation.OpenView)
            {
                this._view.OnViewReady(telegram.Data);
                this._view.OnShow();
            }

            return true;
        }

        public PersonalNoteController(IEFRepository repository, IPersonalNoteView view)
            : base(repository)
        {
            view.ViewRequest = this;

            this._helper = DateHelper.GetInstance();
            this._view = view;
        }

        public void GetPersonalNotes(IEnumerable<PersonalNote> notes)
        {
            DateTime lastUpdatedDate = notes
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._view.OnGetPersonalNotesCompletion(
                notes
                .OrderByDescending(x => x.SystemUpdateDateTime)
                .ToList()
                ,
                lastUpdatedDate);
        }

        public override void GetData(Func<PersonalNote, bool> criteria)
        {
            IQueryable<PersonalNote> attributeQuery = this._repository
                .GetEntityQuery<PersonalNote>();

            if (criteria == null)
                this._view.QueryResults = attributeQuery.Select(x => x);
            else
                this._view.QueryResults = attributeQuery.Where(criteria);

            this._view.OnQueryRecordsCompletion();
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
