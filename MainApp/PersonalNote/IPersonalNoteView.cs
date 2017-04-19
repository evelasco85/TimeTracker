using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IPersonalNoteView : IView<PersonalNote>
    {
        Action<IEnumerable<PersonalNote>> View_GetPersonalNotes { get; set; }
        Action<dynamic, DateTime> View_OnGetPersonalNotesCompletion { get; set; }
    }
}
