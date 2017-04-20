using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IPersonalNoteView : IView<PersonalNote, IPersonalNoteRequests>
    {
        void OnGetPersonalNotesCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IPersonalNoteRequests
    {
        void GetPersonalNotes(IEnumerable<PersonalNote> notes);
    }
}
