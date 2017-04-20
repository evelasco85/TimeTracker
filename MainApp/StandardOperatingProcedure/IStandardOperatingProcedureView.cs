using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Views
{
    public interface IStandardOperatingProcedureView : IView<StandardOperatingProcedure, IStandardOperatingProcedureRequests>,
        IViewControllerEvents<StandardOperatingProcedure>
    {
        void OnGetSOPsCompletion(dynamic displayColumns, DateTime lastUpdatedDate);
    }

    public interface IStandardOperatingProcedureRequests : IViewControllerRequests<StandardOperatingProcedure>
    {
        void GetSOP(IEnumerable<StandardOperatingProcedure> sops);
    }
}
