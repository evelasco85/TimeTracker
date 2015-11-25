using Domain;
using Domain.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter
{
    public interface IPersonalNoteView : IView<PersonalNote>
    {
        Action<IEnumerable<PersonalNote>> GetPersonalNoteData { get; set; }
        Action<dynamic, DateTime> OnGetPersonalNoteDataCompletion { get; set; }
    }
}
