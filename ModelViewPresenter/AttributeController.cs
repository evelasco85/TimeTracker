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
    public class AttributeController : BaseController<Domain.Attribute>
    {
        IAttributeView _view;
        IDateHelper _helper;

        public AttributeController(IEFRepository repository, IAttributeView view)
            : base(repository, view)
        {
            this._helper = DateHelper.GetInstance();
            this._view = view;
            this._view.GetAttributeData = this.GetAttributeData;
        }

        void GetAttributeData(IEnumerable<Domain.Attribute> attributes)
        {
            DateTime lastUpdatedDate = attributes
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this._view.OnGetAttributeDataCompletion(
                attributes
                .OrderBy(x => x.Id)
                .ToList()
                ,
                lastUpdatedDate);
        }

        public override void GetData(Func<Domain.Attribute, bool> criteria)
        {
            IQueryable<Domain.Attribute> attributeQuery = this._repository
                .GetEntityQuery<Domain.Attribute>();

            if (criteria == null)
                this._view.ViewQueryResult = attributeQuery.Select(x => x);
            else
                this._view.ViewQueryResult = attributeQuery.Where(criteria);

            this._view.OnQueryViewRecordsCompletion();
        }

        public override void SaveData(Domain.Attribute data)
        {
            this._repository.Save<Domain.Attribute>(item => item.Id, data);
        }

        public override void DeleteData(Func<Domain.Attribute, bool> criteria)
        {
            this._repository.Delete(criteria);
        }
    }
}
