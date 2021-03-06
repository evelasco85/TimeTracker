﻿using Domain;
using Domain.Controllers;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MainApp
{
    public partial class frmDailyActivity : frmCommonByDateDataEditor, IFormCommonOperation, IDailyActivityView 
    {
        public IDailyActivityRequests ViewRequest { get; set; }
        public IEnumerable<DayActivity> QueryResults { get; set; }

        Form _parentForm;

        IEnumerable<Activity> _presetdActivity;

        public frmDailyActivity()
            : base()
        {
            InitializeComponent();
            this.RegisterCommonOperation(this);
        }

        public void OnShow()
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

        public void OnViewReady(object data)
        {
            this._parentForm = (Form)data.GetType().GetProperty("parentForm").GetValue(data, null);

            this.ViewRequest.GetData(null);
            this.ViewRequest.GetPresetActivityData();
            this.ViewRequest.GetDatesForCurrentPeriod(this.periodPicker.Value.Date);
        }

        public void OnGetDatesForCurrentPeriodCompletion(IEnumerable<DateTime> uniqueDates)
        {
            this.lstUniqueDates.DataSource = uniqueDates.ToList();
        }

        public void OnGetPresetActivityDataCompletion(IEnumerable<Activity> attributes)
        {
            this._presetdActivity = attributes;

            List<string> comboBoxItems = new List<string>();

            comboBoxItems.Add("N/A");
            comboBoxItems.AddRange(this._presetdActivity.Select(x => x.Name).ToList());

            this.cboPresetActivities.DataSource = comboBoxItems;
        }

        public void OnQueryRecordsCompletion()
        {
            IEnumerable<DayActivity> dailyActivities = this.QueryResults;

            this.UpdateSummarizedDailyActivityHours(dailyActivities.Sum(x => x.Duration_Hours));
            this.ViewRequest.GetDailyActivityData(dailyActivities);
        }

        public void UpdateWindow(int rowIndex)
        {
            try
            {
                int id = int.Parse(this.recordGrid.Rows[rowIndex].Cells[DailyActivityController.ID_INDEX].Value.ToString());
                DayActivity dailyActivity = this.QueryResults
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

        public void OnGetDailyActivityDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate)
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
            this.txtName.ReadOnly = !enable;
            this.txtDescription.ReadOnly = !enable;
            this.txtDuration.ReadOnly = !enable;
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
                    this.ViewRequest.DeleteData(x => x.Id == id);
                    this.ViewRequest.GetDatesForCurrentPeriod(this.periodPicker.Value.Date);
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
                    activity = this.QueryResults
                        .Where(x => x.Id == int.Parse(this.lblId.Text))
                        .FirstOrDefault();

                activity.Date = this.date.Value.Date;
                activity.Name = this.txtName.Text;
                activity.Description = this.txtDescription.Text;
                activity.Duration_Hours = decimal.Parse(this.txtDuration.Text);
                activity.SystemUpdateDateTime = DateTime.Now;


                this.ViewRequest.SaveData(activity);
                this.ViewRequest.GetDatesForCurrentPeriod(this.periodPicker.Value.Date);
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

            this.ViewRequest.GetData(x => x.Date == date);
        }

        private void periodPicker_ValueChanged(object sender, EventArgs e)
        {
            this.ViewRequest.GetDatesForCurrentPeriod(this.periodPicker.Value.Date);
        }

        public void DecorateGrid()
        {
            IDataGridHelper helper = DataGridHelper.GetInstance();

            helper.SetAutoResizeCells(ref this.recordGrid);
        }
    }
}
