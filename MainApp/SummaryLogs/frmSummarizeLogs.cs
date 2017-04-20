using Domain;
using Domain.Controllers;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MainApp
{
    public partial class frmSummarizeLogs : Form, ISummaryLogsView
    {
        public ISummaryLogsRequests ViewRequest { get; set; }
        public IEnumerable<LogEntry> QueryResults { get; set; }

        Form _parentForm;
        DateTime _selectedMonth;

        public frmSummarizeLogs()
        {
            InitializeComponent();
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
            this._selectedMonth = (DateTime)data.GetType().GetProperty("selectedMonth").GetValue(data, null);

            this.ViewRequest.GetData(null);
        }

        public void OnQueryRecordsCompletion()
        {
            DisplayLogEntries(_selectedMonth);
        }

        void DisplayLogEntries(DateTime selectedMonth)
        {
            IEnumerable<LogEntry> logs = this.QueryResults;

            this.ViewRequest.GetLogEntries(logs, selectedMonth);
        }

        public void OnGetLogEntriesCompletion(dynamic summarizedLogEntries)
        {
            this.dGridLogs.DataSource = summarizedLogEntries;
        }

        void DecorateGrid()
        {
            this.dGridLogs.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dGridLogs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            IDataGridHelper helper = DataGridHelper.GetInstance();

            helper.SetAutoResizeCells(ref this.dGridLogs);
            helper.SetColumnToDateFormat(this.dGridLogs.Columns[SummaryLogsController.CREATED_INDEX], "yyyy-MM-dd");
            helper.SetColumnToDayFormat(this.dGridLogs.Columns[SummaryLogsController.DAY_INDEX]);

            this.dGridLogs
                .Columns[SummaryLogsController.DESCRIPTION_INDEX]
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void dGridLogs_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.DecorateGrid();
        }

        private void dGridLogs_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int index = 0; index < this.dGridLogs.Rows.Count; index++)
            {
                DataGridViewRow row = this.dGridLogs.Rows[index];
                DateTime created = DateTime.Parse(row.Cells[SummaryLogsController.CREATED_INDEX].Value.ToString());
                bool evenValue = ((created.DayOfYear % 2) == 0);

                if (evenValue)
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                else
                    row.DefaultCellStyle.BackColor = Color.GhostWhite;
            }
        }
    }
}
