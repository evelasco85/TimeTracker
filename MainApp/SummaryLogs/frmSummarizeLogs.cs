using Domain;
using Domain.Controllers;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Domain.Helpers;

namespace MainApp
{
    public partial class frmSummarizeLogs : Form, ISummaryLogsView
    {
        public ISummaryLogsRequests ViewRequest { get; set; }
        public IEnumerable<LogEntry> QueryResults { get; set; }

        Form _parentForm;

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
            DateTime selectedMonth = (DateTime)data.GetType().GetProperty("selectedMonth").GetValue(data, null);

            dateTimeStart.Value = new DateTime(selectedMonth.Year, selectedMonth.Month, 1);
            dateTimeEnd.Value = dateTimeStart.Value.AddMonths(1).AddDays(-1);

            RefreshView();
        }

        void RefreshView()
        {
            this.ViewRequest.GetData(null);
        }

        public void OnQueryRecordsCompletion()
        {
            DisplayLogEntries(dateTimeStart.Value, dateTimeEnd.Value);
        }

        void DisplayLogEntries(DateTime startDate, DateTime endDate)
        {
            IEnumerable<LogEntry> logs = this.QueryResults;

            this.ViewRequest.GetLogEntries(logs, startDate, endDate);
        }

        public void OnGetLogEntriesCompletion(dynamic summarizedLogHours, dynamic summarizedLogEntries, double totalHours)
        {
            this.dGridLogHours.DataSource = summarizedLogHours;
            this.dGridLogs.DataSource = summarizedLogEntries;
            lblTotalHours.Text = string.Format("Total Hours: {0}", totalHours);
        }

        public void OnGetLogEntriesByCategoryCompletion(dynamic summarizedLogEntries)
        {
            this.dGridLogs.DataSource = summarizedLogEntries;
        }

        void DecorateGrid()
        {
            this.dGridLogHours.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dGridLogHours.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dGridLogs.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dGridLogs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            IDataGridHelper helper = DataGridHelper.GetInstance();

            helper.SetAutoResizeCells(ref this.dGridLogHours);
            helper.SetAutoResizeCells(ref this.dGridLogs);
            helper.SetColumnToDateFormat(this.dGridLogs.Columns[SummaryLogsController.CREATED_INDEX], "yyyy-MM-dd");
            helper.SetColumnToDayFormat(this.dGridLogs.Columns[SummaryLogsController.DAY_INDEX]);
            
            this.dGridLogs
                .Columns[SummaryLogsController.DESCRIPTION_INDEX]
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dGridLogHours
                .Columns[0]
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
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
                string description = row.Cells[SummaryLogsController.DESCRIPTION_INDEX].Value.ToString();
                string category = row.Cells[SummaryLogsController.CATEGORY_INDEX].Value.ToString();

                if ((category == LogEntriesController.HOLIDAY) || (category == LogEntriesController.LEAVE))
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                }
                else if (DateHelper.GetInstance().WeekendDate(created))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.Cells[SummaryLogsController.DESCRIPTION_INDEX].Value = (string.IsNullOrEmpty(description)) ? LogEntriesController.WEEKEND : description;
                    row.Cells[SummaryLogsController.CATEGORY_INDEX].Value = (string.IsNullOrEmpty(category)) ? LogEntriesController.WEEKEND : category;
                }
                else if (description == LogEntriesController.NO_DESCRIPTION)
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                else
                {
                    bool evenValue = ((created.DayOfYear % 2) == 0);

                    if (evenValue)
                        row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    else
                        row.DefaultCellStyle.BackColor = Color.GhostWhite;
                }
            }
        }

        private void dateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimeStart.Value > dateTimeEnd.Value)
                dateTimeEnd.Value = dateTimeStart.Value;

            RefreshView();
        }

        private void dateTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimeEnd.Value < dateTimeStart.Value)
                dateTimeStart.Value = dateTimeEnd.Value;

            RefreshView();
        }

        private void dGridLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void dGridLogHours_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            string category = string.Empty;

            try
            {
                DataGridViewRow row = this.dGridLogHours.Rows[index];
                category = row.Cells[0].Value.ToString();

            }
            catch (ArgumentOutOfRangeException)
            {
                /*Skip*/
            }
            finally
            {
                this.ViewRequest.GetLogEntriesByCategory(this.QueryResults, dateTimeStart.Value, dateTimeEnd.Value, category);
            }
        }
    }
}
