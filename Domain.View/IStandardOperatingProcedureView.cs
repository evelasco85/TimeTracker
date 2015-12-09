using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IStandardOperatingProcedureView : IView<StandardOperatingProcedure>
    {
        Action<IEnumerable<StandardOperatingProcedure>> View_GetSOPs { get; set; }
        Action<dynamic, DateTime> View_OnGetSOPsCompletion { get; set; }
    }
}
