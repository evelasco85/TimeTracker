﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Domain.Helpers;

namespace MainApp.DailyHours
{
    public partial class frmDailyHours : Form, IDailyHoursView
    {
        Form _parentForm;

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
            this._parentForm = (Form)data.GetType().GetProperty("parentForm").GetValue(data, null);

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


        public void OnGetLogsForDateCompletion(dynamic displayColumns)
        {
            this.dGridLogs.DataSource = displayColumns;

            this.dGridLogs.Refresh();
        }
    }
}
