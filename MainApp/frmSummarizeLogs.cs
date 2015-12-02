using Domain;
using Domain.Controllers;
using Domain.Infrastructure;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MainApp
{
    public partial class frmSummarizeLogs : Form, ISummaryLogsView
    {
        public Action<Func<LogEntry, bool>> View_QueryRecords { get; set; }
        public Action View_OnQueryRecordsCompletion { get; set; }
        public Action<Func<LogEntry, bool>> View_DeleteRecords { get; set; }
        public Action<LogEntry> View_SaveRecord { get; set; }
        public IEnumerable<LogEntry> View_QueryResults { get; set; }
        public Action<IEnumerable<LogEntry>, DateTime> View_GetLogEntries { get; set; }
        public Action<dynamic> View_OnGetLogEntriesCompletion { get; set; }
        public Action<object> View_ViewReady { get; set; }
        public Action<object> View_OnViewReady { get; set; }

        public frmSummarizeLogs()
        {
            this.View_OnGetLogEntriesCompletion = this.UpdateSummaryLogs;
            this.View_OnViewReady = OnViewReady;
            
            InitializeComponent();
        }

        void OnViewReady(object data)
        {
             DateTime selectedMonth =(DateTime) data.GetType().GetProperty("selectedMonth").GetValue(data, null);

            this.View_OnQueryRecordsCompletion = () => DisplayLogEntries(selectedMonth);

            this.View_QueryRecords(null);
        }

        void DisplayLogEntries(DateTime selectedMonth)
        {
            IEnumerable<LogEntry> logs = this.View_QueryResults;

            this.View_GetLogEntries(logs, selectedMonth);
        }

        void UpdateSummaryLogs(dynamic summarizedLogEntries)
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
