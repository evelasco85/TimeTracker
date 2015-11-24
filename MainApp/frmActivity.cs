using Domain;
using Domain.Controller;
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
    public partial class frmActivity : frmCommonDataEditor, IActivityView, IFormCommonOperation
    {
        public Action<Func<Activity, bool>> QueryViewRecords { get; set; }
        public Action OnQueryViewRecordsCompletion { get; set; }
        public Action<Activity> SaveViewRecord { get; set; }
        public Action<Func<Activity, bool>> DeleteViewRecords { get; set; }
        public IEnumerable<Activity> ViewQueryResult { get; set; }
        public Action<IEnumerable<Activity>> GetActivityData { get; set; }
        public Action<dynamic, DateTime> OnGetActivityDataCompletion { get; set; }

        public frmActivity(IEFRepository repository)
        {
            InitializeComponent();

            Action RegisterController = () => new ActivityController(repository, this);

            RegisterController();
            this.RegisterCommonOperation(this);

            this.OnQueryViewRecordsCompletion = this.RefreshGridData;
            this.OnGetActivityDataCompletion = this.UpdateCategoryData;

            this.QueryViewRecords(null);
        }

        void RefreshGridData()
        {
            IEnumerable<Activity> categories = this.ViewQueryResult;

            this.GetActivityData(categories);
        }

        void UpdateCategoryData(dynamic displayColumns, DateTime lastUpdatedDate)
        {
            this.dGrid.DataSource = displayColumns;

            this.dGrid.Refresh();
            this.HighlightRecordByDate(lastUpdatedDate);
        }

        void HighlightRecordByDate(DateTime recordDate)
        {
            for (int index = 0; index < this.dGrid.Rows.Count; index++)
            {
                try
                {
                    DateTime systemDate = DateTime.Parse(this.dGrid.Rows[index].Cells[HolidayController.SYSTEM_UPDATED_INDEX].Value.ToString());
                    bool identicalTime = recordDate.ToLongTimeString() == systemDate.ToLongTimeString();

                    if ((recordDate.Date == systemDate.Date) && identicalTime)
                    {
                        this.dGrid.CurrentCell = this.dGrid[HolidayController.ID_INDEX, index];
                        this.dGrid.Rows[index].Selected = true;
                        this.dGrid.Rows[index].Cells[HolidayController.ID_INDEX].Selected = true;
                        this.dGrid.FirstDisplayedScrollingRowIndex = index;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            this.dGrid.Update();
        }

        public void UpdateWindow(int rowIndex)
        {
            try
            {
                int id = int.Parse(this.dGrid.Rows[rowIndex].Cells[CategoryController.ID_INDEX].Value.ToString());
                Activity activity = this.ViewQueryResult
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                this.lblId.Text = activity.Id.ToString();
                this.txtActivityName.Text = activity.Name;
                this.txtDescription.Text = activity.Description;
            }
            catch (ArgumentOutOfRangeException) { /*Skip*/}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnableInputWindow(bool enable)
        {
            this.txtActivityName.Enabled = enable;
            this.txtDescription.Enabled = enable;
        }

        public void ResetInputWindow()
        {
            this.lblId.Text = string.Empty;
            this.txtActivityName.Clear();
            this.txtDescription.Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(this.lblId.Text);

                if (id == 0)
                    throw new FormatException();

                this.WindowInputChanges(ModifierState.Edit);
            }
            catch (FormatException)
            {
                MessageBox.Show("A record selection is required for editing");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(this.lblId.Text);

                if (id == 0)
                    throw new FormatException();

                DialogResult result = MessageBox.Show("Delete record?", "Delete Record Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.DeleteViewRecords(x => x.Id == id);
                    this.QueryViewRecords(null);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("A record selection is required for deletion");
            }

            this.WindowInputChanges(ModifierState.Delete);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Activity attribute = new Activity
            {
                System_Created = DateTime.Now,
            };

            if (!string.IsNullOrEmpty(this.lblId.Text))
                attribute = this.ViewQueryResult
                    .Where(x => x.Id == int.Parse(this.lblId.Text))
                    .FirstOrDefault();

            attribute.Name = this.txtActivityName.Text;
            attribute.Description = this.txtDescription.Text;
            attribute.SystemUpdateDateTime = DateTime.Now;

            this.SaveViewRecord(attribute);
            this.QueryViewRecords(null);
            this.WindowInputChanges(ModifierState.Save);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Add);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Cancel);
        }
    }
}
