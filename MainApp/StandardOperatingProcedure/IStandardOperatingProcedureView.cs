using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IStandardOperatingProcedureView : IView<StandardOperatingProcedure, IStandardOperatingProcedureRequests, IStandardOperatingProcedureEvents>, IStandardOperatingProcedureEvents
    {
    }

    public interface IStandardOperatingProcedureEvents
    {
        void OnGetSOPsCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IStandardOperatingProcedureRequests
    {
        void GetSOP(IEnumerable<StandardOperatingProcedure> sops);
    }
}
