using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IPersonalNoteView : IView<PersonalNote, IPersonalNoteRequests>, IViewControllerEvents<PersonalNote>
    {
        void OnGetPersonalNotesCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IPersonalNoteRequests : IViewControllerRequests<PersonalNote>
    {
        void GetPersonalNotes(IEnumerable<PersonalNote> notes);
    }
}
