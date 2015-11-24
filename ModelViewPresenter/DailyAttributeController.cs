using Domain;
using Domain.Controller;
using Domain.Helpers;
using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter
{
    public class DailyAttributeController : BaseController<DayAttribute>
    {
        IDailyAttributeView _view;
        IDateHelper _helper;

        public DailyAttributeController(IEFRepository repository, IDailyAttributeView view)
            : base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._view = view;
        }

        public override void DeleteData(Func<DayAttribute, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public override void GetData(Func<DayAttribute, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public override void SaveData(DayAttribute data)
        {
            throw new NotImplementedException();
        }
    }
}
