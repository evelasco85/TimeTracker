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
    public partial class frmSummarizeLogs : Form, IView<LogEntry>
    {
        IDateHelper _helper;

        public Action<Func<LogEntry, bool>> QueryViewRecords { get; set; }
        public Action<Func<LogEntry, bool>> DeleteViewRecords { get; set; }
        public Action<LogEntry> SaveViewRecord { get; set; }
        public IEnumerable<LogEntry> ViewQueryResult { get; set; } 

        public frmSummarizeLogs(IEFRepository repository, DateTime selectedMonth)
        {
            Action RegisterController = () => new SummaryLogsController(repository, this);

            RegisterController();

            this._helper = DateHelper.GetInstance();

            InitializeComponent();
            this.DisplayLogEntries(selectedMonth);
        }
        void DisplayLogEntries(DateTime selectedMonth)
        {
            this.QueryViewRecords(null);

            IList<LogEntry> logEntries = this._helper.GetMonthSummaryLogs(this.ViewQueryResult, selectedMonth);
            IEnumerable<IGrouping<string, string>> perDateLogs =
                logEntries
                .Where(x => !(x.Category.ToLower() == "others"))
                .GroupBy(log => log.Created.ToString("yyyy-MM-dd"), log => log.Description);

            var officialLogEntries =
                perDateLogs
                .Select(x => new
                {
                    Created = DateTime.Parse(x.Key),
                    Description = string.Join(Environment.NewLine, x.ToList())
                })
                .ToList();

            IList<DateTime> allDates = logEntries
                .Select(x => x.Created)
                .Distinct()
                .ToList();
            IList<DateTime> officialWorkDates = officialLogEntries
                .Select(x => x.Created).ToList();
            IList<DateTime> unofficialDates = allDates
                .Where(x => !(officialWorkDates.Any(y => y.Date == x.Date)))
                .ToList();

            Func<DateTime, string> getDescription = (date) =>
                {
                    return (unofficialDates.Any( x => x.Date == date)) ? "N/A" : 
                        string.Join(Environment.NewLine,
                        officialLogEntries
                            .Where(x => x.Created == date)
                            .Select(x => x.Description)
                            .ToList()
                            )
                        ;
                };

            var summarizedLogEntries = allDates
                .Select(x => new
                {
                    Created = x,
                    @Day = x,
                    Description = getDescription(x)
                })
                .ToList();

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
