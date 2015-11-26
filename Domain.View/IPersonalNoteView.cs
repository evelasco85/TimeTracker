using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IPersonalNoteView : IView<PersonalNote>
    {
        Action<IEnumerable<PersonalNote>> GetPersonalNoteData { get; set; }
        Action<dynamic, DateTime> OnGetPersonalNoteDataCompletion { get; set; }
    }
}
