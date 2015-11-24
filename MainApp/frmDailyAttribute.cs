using Domain;
using Domain.Infrastructure;
using ModelViewPresenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp
{
    public partial class frmDailyAttribute : frmCommonByDateDataEditor, IFormCommonOperation, IDailyAttributeView
    {
        public Action<Func<DayAttribute, bool>> QueryViewRecords { get; set; }
        public Action OnQueryViewRecordsCompletion { get; set; }
        public Action<DayAttribute> SaveViewRecord { get; set; }
        public Action<Func<DayAttribute, bool>> DeleteViewRecords { get; set; }
        public IEnumerable<DayAttribute> ViewQueryResult { get; set; }

        public frmDailyAttribute(IEFRepository repository)
        {
            InitializeComponent();
            Action RegisterController = () => new DailyAttributeController(repository, this);

            RegisterController();
            this.RegisterCommonOperation(this);
            //this.QueryViewRecords(null);  select first date here!
        }

        public void UpdateWindow(int rowIndex)
        {
        }

        public void EnableInputWindow(bool enable)
        {
        }

        public void ResetInputWindow()
        {
        }
    }
}
