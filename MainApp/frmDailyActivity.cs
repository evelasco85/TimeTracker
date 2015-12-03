using Domain;
using Domain.Controllers;
using Domain.Infrastructure;
using Domain.Views;
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
    public partial class frmDailyActivity : frmCommonByDateDataEditor, IFormCommonOperation, IDailyActivityView 
    {
        public Action<Func<DayActivity, bool>> View_QueryRecords { get; set; }
        public Action View_OnQueryRecordsCompletion { get; set; }
        public Action<DayActivity> View_SaveRecord { get; set; }
        public Action<Func<DayActivity, bool>> View_DeleteRecords { get; set; }
        public IEnumerable<DayActivity> View_QueryResults { get; set; }
        public Action View_GetPresetActivityData { get; set; }
        public Action<IEnumerable<Activity>> View_OnGetPresetActivityDataCompletion { get; set; }
        public Action<IEnumerable<DayActivity>> View_GetDailyActivityData { get; set; }
        public Action<dynamic, DateTime> View_OnGetDailyActivityDataCompletion { get; set; }
        public Action<DateTime> View_GetDatesForCurrentPeriod { get; set; }
        public Action<IEnumerable<DateTime>> View_OnGetDatesForCurrentPeriodCompletion { get; set; }
        public Action<object> View_ViewReady { get; set; }
        public Action<object> View_OnViewReady { get; set; }
        public Action View_OnShow { get; set; }

        Form _parentForm;

        IEnumerable<Activity> _presetdActivity;

        public frmDailyActivity()
            : base()
        {
            InitializeComponent();
            this.RegisterCommonOperation(this);

            this.View_OnQueryRecordsCompletion = this.RefreshGridData;
            this.View_OnGetPresetActivityDataCompletion = this.PopulateActivityPresets;
            this.View_OnGetDailyActivityDataCompletion = this.UpdateDailyActivityData;
            this.View_OnGetDatesForCurrentPeriodCompletion = this.PopulateUniqueDates;
            this.View_OnViewReady = OnViewReady;
            this.View_OnShow = OnShow;
        }

        void OnShow()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   try
                   {
                       this.ShowDialog(this._parentForm);
                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }
               }
           );

            if (this.InvokeRequired)
                this.Invoke(invokeFromUI);
            else
                invokeFromUI.Invoke();
        }

        void OnViewReady(object data)
        {
            this._parentForm = (Form)data.GetType().GetProperty("parentForm").GetValue(data, null);

            this.View_QueryRecords(null);
            this.View_GetPresetActivityData();
            this.View_GetDatesForCurrentPeriod(this.periodPicker.Value.Date);
        }

        void PopulateUniqueDates(IEnumerable<DateTime> uniqueDates)
        {
            this.lstUniqueDates.DataSource = uniqueDates.ToList();
        }

        void PopulateActivityPresets(IEnumerable<Activity> attributes)
        {
            this._presetdActivity = attributes;

            List<string> comboBoxItems = new List<string>();

            comboBoxItems.Add("N/A");
            comboBoxItems.AddRange(this._presetdActivity.Select(x => x.Name).ToList());

            this.cboPresetActivities.DataSource = comboBoxItems;
        }

        void RefreshGridData()
        {
            IEnumerable<DayActivity> dailyActivities = this.View_QueryResults;

            this.UpdateSummarizedDailyActivityHours(dailyActivities.Sum(x => x.Duration_Hours));
            this.View_GetDailyActivityData(dailyActivities);
        }

        public void UpdateWindow(int rowIndex)
        {
            try
            {
                int id = int.Parse(this.recordGrid.Rows[rowIndex].Cells[DailyActivityController.ID_INDEX].Value.ToString());
                DayActivity dailyActivity = this.View_QueryResults
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                this.lblId.Text = dailyActivity.Id.ToString();
                this.date.Value = dailyActivity.Date.Date;
                this.txtName.Text = dailyActivity.Name;
                this.txtDescription.Text = dailyActivity.Description;
                this.txtDuration.Text = dailyActivity.Duration_Hours.ToString();
            }
            catch (ArgumentOutOfRangeException) { /*Skip*/}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void UpdateDailyActivityData(dynamic displayColumns, DateTime lastUpdatedDate)
        {
            this.recordGrid.DataSource = displayColumns;

            this.recordGrid.Refresh();
            this.HighlightRecordByDate(lastUpdatedDate);
        }

        void HighlightRecordByDate(DateTime recordDate)
        {
            for (int index = 0; index < this.recordGrid.Rows.Count; index++)
            {
                try
                {
                    DateTime systemDate = DateTime.Parse(this.recordGrid.Rows[index].Cells[DailyActivityController.SYSTEM_UPDATED_INDEX].Value.ToString());
                    bool identicalTime = recordDate.ToLongTimeString() == systemDate.ToLongTimeString();

                    if ((recordDate.Date == systemDate.Date) && identicalTime)
                    {
                        this.recordGrid.CurrentCell = this.recordGrid[DailyActivityController.ID_INDEX, index];
                        this.recordGrid.Rows[index].Selected = true;
                        this.recordGrid.Rows[index].Cells[DailyActivityController.ID_INDEX].Selected = true;
                        this.recordGrid.FirstDisplayedScrollingRowIndex = index;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            this.recordGrid.Update();
        }

        public void EnableInputWindow(bool enable)
        {
            this.cboPresetActivities.Enabled = enable;
            this.periodPicker.Enabled = !enable;
            this.lstUniqueDates.Enabled = !enable;

            this.date.Enabled = enable;
            this.txtName.Enabled = enable;
            this.txtDescription.Enabled = enable;
            this.txtDuration.Enabled = enable;
        }

        public void ResetInputWindow()
        {
            this.cboPresetActivities.SelectedIndex = 0;
            this.lblId.Text = string.Empty;
            this.date.Value = DateTime.Now;
            this.txtName.Clear();
            this.txtDescription.Clear();
            this.txtDuration.Clear();
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
                    this.View_DeleteRecords(x => x.Id == id);
                    this.View_GetDatesForCurrentPeriod(this.periodPicker.Value.Date);
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
            try
            {
                DayActivity activity = new DayActivity
                {
                    System_Created = DateTime.Now,
                };

                if (!string.IsNullOrEmpty(this.lblId.Text))
                    activity = this.View_QueryResults
                        .Where(x => x.Id == int.Parse(this.lblId.Text))
                        .FirstOrDefault();

                activity.Date = this.date.Value.Date;
                activity.Name = this.txtName.Text;
                activity.Description = this.txtDescription.Text;
                activity.Duration_Hours = decimal.Parse(this.txtDuration.Text);
                activity.SystemUpdateDateTime = DateTime.Now;


                this.View_SaveRecord(activity);
                this.View_GetDatesForCurrentPeriod(this.periodPicker.Value.Date);
                this.WindowInputChanges(ModifierState.Save);
            }
            catch(System.FormatException)
            {
                MessageBox.Show("Duration should contain a valid decimal number",
                    "Invalid input, Please try again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Add);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Cancel);
        }

        void SetControlValues(Activity activity)
        {
            if (activity == null)
                return;

            this.txtName.Text = activity.Name;
            this.txtDescription.Text = activity.Description;
        }

        private void cboPresetActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            Activity activity = this._presetdActivity
                .Where(x => x.Name == (string) this.cboPresetActivities.SelectedItem)
                .FirstOrDefault();

            this.SetControlValues(activity);
        }

        private void lstUniqueDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDate = this.lstUniqueDates.SelectedItem.ToString();
            DateTime date = DateTime.Parse(selectedDate);

            this.View_QueryRecords(x => x.Date == date);
        }

        private void periodPicker_ValueChanged(object sender, EventArgs e)
        {
            this.View_GetDatesForCurrentPeriod(this.periodPicker.Value.Date);
        }

        public void DecorateGrid()
        {
            IDataGridHelper helper = DataGridHelper.GetInstance();

            helper.SetAutoResizeCells(ref this.recordGrid);
        }
    }
}
