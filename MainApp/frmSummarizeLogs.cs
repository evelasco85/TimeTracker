using Domain;
using Domain.Helpers;
using Domain.Infrastructure;
using Domain.MVP;
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
        public Action<Func<LogEntry, bool>> QueryViewRecords { get; set; }
        public Action OnQueryViewRecordsCompletion { get; set; }
        public Action<Func<LogEntry, bool>> DeleteViewRecords { get; set; }
        public Action<LogEntry> SaveViewRecord { get; set; }
        public IEnumerable<LogEntry> ViewQueryResult { get; set; }
        public Action<IEnumerable<LogEntry>, DateTime> GetLogEntries { get; set; }
        public Action<dynamic> OnGetLogEntriesCompletion { get; set; }

        public frmSummarizeLogs(IEFRepository repository, DateTime selectedMonth)
        {
            Action RegisterController = () => new SummaryLogsController(repository, this);

            RegisterController();

            this.OnQueryViewRecordsCompletion = () => DisplayLogEntries(selectedMonth);
            this.OnGetLogEntriesCompletion = this.UpdateSummaryLogs;
            
            InitializeComponent();
            this.QueryViewRecords(null);
        }

        void DisplayLogEntries(DateTime selectedMonth)
        {
            IEnumerable<LogEntry> logs = this.ViewQueryResult;

            this.GetLogEntries(logs, selectedMonth);
        }

        void UpdateSummaryLogs(dynamic summarizedLogEntries)
        {
            this.dGridLogs.DataSource = summarizedLogEntries;
        }

        void DecorateGrid()
        {
            this.dGridLogs.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dGridLogs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            DataGridViewColumn createdColumn = this.dGridLogs.Columns[SummaryLogsController.CREATED_INDEX];
            createdColumn.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "MM/dd/yyyy"
            };
            createdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dGridLogs.AutoResizeColumn(SummaryLogsController.CREATED_INDEX);

            DataGridViewColumn createdDayColumn = this.dGridLogs.Columns[SummaryLogsController.DAY_INDEX];
            createdDayColumn.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "dddd"
            };
            createdDayColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dGridLogs.AutoResizeColumn(SummaryLogsController.DAY_INDEX);

            DataGridViewColumn descriptionColumn = this.dGridLogs.Columns[SummaryLogsController.DESCRIPTION_INDEX];
            descriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dGridLogs.AutoResizeColumn(SummaryLogsController.DESCRIPTION_INDEX);
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
