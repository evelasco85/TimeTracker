﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Domain.Controllers;
using Domain.Helpers;

namespace MainApp.DailyHours
{
    public partial class frmDailyHours : Form, IDailyHoursView
    {
        frmMain _parentForm;

        public frmDailyHours()
        {
            InitializeComponent();
        }

        public IDailyHoursRequests ViewRequest { get; set; }

        public IEnumerable<Domain.LogEntry> QueryResults { get; set; }

        public void OnQueryRecordsCompletion()
        {
            this.ViewRequest.GetDailyRecordData(this.QueryResults,
                this.dateTimeManualEntry.Value.Date);
        }

        public void OnViewReady(object data)
        {
            this._parentForm = (frmMain)data.GetType().GetProperty("parentForm").GetValue(data, null);

            this.ViewRequest.GetData(null);
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

        public void OnGetDailyRecordDataCompletion(dynamic displayColumns)
        {
            this.dGridLogs.DataSource = displayColumns;

            this.dGridLogs.Refresh();
        }

        private void dateTimeManualEntry_ValueChanged(object sender, EventArgs e)
        {
            this.ViewRequest.GetLogsForDate(this.QueryResults, dateTimeManualEntry.Value.Date);
        }

        void UpdateHoursDisplay(double hoursRecorded)
        {
            txtHoursRecorded.Text = hoursRecorded.ToString();
        }

        public void OnGetLogsForDateCompletion(dynamic displayColumns, double hoursRecorded)
        {
            this.dGridLogs.DataSource = displayColumns;

            this.dGridLogs.Refresh();
            UpdateHoursDisplay(hoursRecorded);
        }

        void DecorateGrid()
        {
            try
            {
                IDataGridHelper helper = DataGridHelper.GetInstance();

                helper.SetAutoResizeCells(ref this.dGridLogs);
                helper.SetColumnToDateFormat(this.dGridLogs.Columns[LogEntriesController.CREATED_INDEX]);
                helper.SetColumnToTimeFormat(this.dGridLogs.Columns[LogEntriesController.TIME_INDEX]);
                helper.SetColumnToDayFormat(this.dGridLogs.Columns[LogEntriesController.DAY_INDEX]);

                this.dGridLogs
                    .Columns[LogEntriesController.DESCRIPTION_INDEX]
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            catch (Exception e)
            {
            }
        }

        private void dGridLogs_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DecorateGrid();
        }

        private void btnIncementDayByOne_Click(object sender, EventArgs e)
        {
            IncrementDayByOne();
        }

        void IncrementDayByOne()
        {
            this.dateTimeManualEntry.Value = this.dateTimeManualEntry.Value.AddDays(1);
        }

        private void btnManuaTrackerEntry_Click(object sender, EventArgs e)
        {
            if(_parentForm != null)
                _parentForm.btnManuaTrackerEntry_Click(sender, e);

            this.ViewRequest.GetLogsForDate(this.QueryResults, dateTimeManualEntry.Value.Date);
        }

        private void dGridLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            try
            {
                DataGridViewRow row = this.dGridLogs.Rows[index];
                string category = row.Cells[LogEntriesController.CATEGORY_INDEX].Value.ToString();

                if ((category == LogEntriesController.HOLIDAY) || (category == LogEntriesController.LEAVE))
                    return;

                int primaryKey = int.Parse(row.Cells[LogEntriesController.ID_INDEX].Value.ToString());
                DateTime createdDate = DateTime.Parse(row.Cells[LogEntriesController.CREATED_INDEX].Value.ToString());
                DateTime systemCreatedDate =
                    DateTime.Parse(row.Cells[LogEntriesController.SYSTEM_CREATED_INDEX].Value.ToString());
                string description = row.Cells[LogEntriesController.DESCRIPTION_INDEX].Value.ToString();
                bool rememberSetting = _parentForm.ViewRequest.GetRememberedSetting();
                DateTime rememberedCreatedDateTime = _parentForm.ViewRequest.GetRememberedDate();
                double hoursRendered = Convert.ToDouble(row.Cells[LogEntriesController.HOURS_RENDERED_INDEX].Value);

                _parentForm.SafeEditEntry(primaryKey, category, description, rememberSetting, createdDate,
                    systemCreatedDate, rememberedCreatedDateTime, hoursRendered);
            }
            catch (ArgumentOutOfRangeException)
            {
                /*Skip*/
            }
            finally
            {
                this.ViewRequest.GetLogsForDate(this.QueryResults, dateTimeManualEntry.Value.Date);
            }
        }
    }
}
