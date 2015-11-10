using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity.Migrations;
using Domain.Infrastructure;
using Domain.MVP;
using Domain.Helpers;
using System.Linq.Expressions;

namespace MainApp
{
    public partial class frmMain : Form, IView<LogEntry>
    {
        System.Timers.Timer _timerNotification;
        bool _rememberSetting = true;
        DateTime _rememberedCreatedDateTime;
        bool _promptingInProgress = false;
        
        IEFRepository _repo;
        IDateHelper _helper;
        public Action<Func<LogEntry, bool>> QueryViewRecords { get; set; }
        public Action<Func<LogEntry, bool>> DeleteViewRecords { get; set; }
        public Action<LogEntry> SaveViewRecord { get; set; }
        public IEnumerable<LogEntry> ViewQueryResult { get; set; }

        Func<Func<Holiday, bool>, IEnumerable<Holiday>> _getHolidaysFunc { get; set; }

        public frmMain(IEFRepository repository)
        {
            Action RegisterController = () =>
                {
                   LogEntriesController controller =new LogEntriesController(repository, this);

                   this._getHolidaysFunc = controller.GetHolidays;
                };

            RegisterController();

            this._helper = DateHelper.GetInstance();
            this._repo = repository;

            InitializeComponent();
            this.InitializeRequiredData();
            this.RefreshDashboardData();
            this.RefreshGridData();
            this.StartTimer();
        }

        void InitializeRequiredData()
        {
            this.tryIcon.BalloonTipIcon = ToolTipIcon.Info;
            this.tryIcon.Icon = Resource1.MSN;
            this.dateTimeMonth.CustomFormat = "MMMM";

            this.SetTimer();
        }

        void RefreshGridData()
        {
            this.QueryViewRecords(null);        //Query the controller

            DateTime selectedMonth = this.dateTimeMonth.Value;
            IList<LogEntry> availableLogEntries = this._helper.GetMonthLogs(this.ViewQueryResult, selectedMonth);
            IList<LogEntry> missingLogEntries = this._helper.GenerateMissingEntriesForMissingDates(availableLogEntries, selectedMonth);
            List<LogEntry> logEntries = new List<LogEntry>();

            logEntries.AddRange(availableLogEntries);
            logEntries.AddRange(missingLogEntries);

            var displayColumns = logEntries
                .Select(x => new {
                    x.Id,
                    x.Created,
                    Time = x.Created,
                    Day = x.Created,
                    x.Category,
                    x.Description,
                    x.System_Created,
                    x.SystemUpdateDateTime,
                })
                .OrderByDescending(x => x.Created)
                .ToList();

            this.dGridLogs.DataSource = displayColumns;

            this.dGridLogs.Refresh();

            DateTime lastUpdatedDate = availableLogEntries
                .Select(x => x.SystemUpdateDateTime)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this.HighlightRecordByDate(lastUpdatedDate);
        }

        void DecorateGrid()
        {
            this.dGridLogs.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dGridLogs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            DataGridViewColumn createdColumn = this.dGridLogs.Columns[LogEntriesController.CREATED_INDEX];
            createdColumn.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "MM/dd/yyyy"
            };
            createdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridViewColumn timeColumn = this.dGridLogs.Columns[LogEntriesController.TIME_INDEX];
            timeColumn.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "HH:mm"
            };
            timeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridViewColumn createdDayColumn = this.dGridLogs.Columns[LogEntriesController.DAY_INDEX];
            createdDayColumn.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "dddd"
            };
            createdDayColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridViewColumn descriptionColumn = this.dGridLogs.Columns[LogEntriesController.DESCRIPTION_INDEX];
            descriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            this.dGridLogs.AutoResizeColumn(LogEntriesController.CREATED_INDEX);
            this.dGridLogs.AutoResizeColumn(LogEntriesController.TIME_INDEX);
            this.dGridLogs.AutoResizeColumn(LogEntriesController.DAY_INDEX);
            this.dGridLogs.AutoResizeColumn(LogEntriesController.DESCRIPTION_INDEX);
        }

        void HighlightRecordByDate(DateTime recordDate)
        {
            for (int index = 0; index < this.dGridLogs.Rows.Count; index++)
            {
                try
                {
                    DateTime systemDate = DateTime.Parse(this.dGridLogs.Rows[index].Cells[LogEntriesController.SYSTEM_UPDATED_INDEX].Value.ToString());
                    bool identicalTime = recordDate.ToLongTimeString() == systemDate.ToLongTimeString();

                    if ((recordDate.Date == systemDate.Date) && identicalTime)
                    {
                        this.dGridLogs.CurrentCell = this.dGridLogs[LogEntriesController.ID_INDEX, index];
                        this.dGridLogs.Rows[index].Selected = true;
                        this.dGridLogs.Rows[index].Cells[LogEntriesController.ID_INDEX].Selected = true;
                        this.dGridLogs.FirstDisplayedScrollingRowIndex = index;

                        this.dGridLogs.Update();
                        break;
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.tryIcon.Visible = true;

                this.tryIcon.ShowBalloonTip(500);

                this.ShowInTaskbar = false;
            }
        }

        private void tryIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.tryIcon.Visible = false;
        }

        void SetTimer()
        {
            Properties.Settings settings = MainApp.Properties.Settings.Default;

            int minutes = (settings.timerMinute) + (settings.timerHour * 60);
            int seconds = settings.timerSecord + (minutes * 60);

            int milliSeconds = (seconds * 1000) + settings.timerMillisecond;

            this._timerNotification = new System.Timers.Timer(milliSeconds);
            this._timerNotification.Elapsed += TimerNotification_Elapsed;
            this._timerNotification.AutoReset = false;
        }

        void StartTimer()
        {
            this._timerNotification.Start();
        }

        void StopTimer()
        {
            this._timerNotification.Stop();
        }

        void RestartTimer()
        {
            this.StopTimer();
            this.StartTimer();
        }

        void TimerNotification_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.AddEntry();
        }

        void AddEntry()
        {
            this.StopTimer();
            this.SafeOpenTimeEntryDialog();
            this.RestartTimer();
        }

        void SafeEditEntry(int primaryKey, string category, string description, bool rememberSetting,
            DateTime createdDate, DateTime systemCreatedDate, DateTime rememberedCreatedDateTime)
        {
            this.StopTimer();

            MethodInvoker invokeFromUI = new MethodInvoker(
                () =>
                {
                    try
                    {
                        if (this._promptingInProgress)
                            return;

                        this._promptingInProgress = true;

                        using (frmTaskMonitoringEntry monitoring = new frmTaskMonitoringEntry(
                            primaryKey, category, description, rememberSetting, createdDate, systemCreatedDate))
                        {
                            DialogResult result = monitoring.ShowDialog(this);

                            if (result != System.Windows.Forms.DialogResult.OK)
                                return;

                            this.SaveLogDetail(monitoring);

                            monitoring.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        this._promptingInProgress = false;
                    }
                }
            );

            if (this.InvokeRequired)
                this.Invoke(invokeFromUI);
            else
                invokeFromUI.Invoke();

            this.RestartTimer();
        }

        void PromptMonitoring()
        {
            try
            {
                if (this._promptingInProgress)
                    return;

                this._promptingInProgress = true;

                using (frmTaskMonitoringEntry monitoring = new frmTaskMonitoringEntry(this._rememberSetting, this._rememberedCreatedDateTime))
                {
                    DialogResult result = monitoring.ShowDialog(this);

                    if (result != System.Windows.Forms.DialogResult.OK)
                        return;

                    this.SaveLogDetail(monitoring);

                    monitoring.Dispose();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                this._promptingInProgress = false;
            }
        }

        void SafeOpenTimeEntryDialog()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(this.PromptMonitoring);

            if (this.InvokeRequired)
                this.Invoke(invokeFromUI);
            else
                invokeFromUI.Invoke();
        }

        bool SaveLogDetail(frmTaskMonitoringEntry monitoring)
        {
            bool success = false;
            LogEntry logEntry = monitoring.LogEntry;

            if ((logEntry == null) || (string.IsNullOrEmpty(logEntry.Description)))
                return success;

            this._rememberSetting = monitoring.RememberSetting;
            this._rememberedCreatedDateTime = logEntry.Created;

            try
            {
                if (logEntry.System_Created == DateTime.MinValue)
                    logEntry.System_Created = DateTime.Now;

                this.SaveViewRecord(logEntry);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            this.RefreshGridData();
            this.RefreshDashboardData();

            success = true;

            return success;
        }

        private void btnManuaTrackerEntry_Click(object sender, EventArgs e)
        {
            this.AddEntry();
        }


        void RefreshDashboardData()
        {
            this.QueryViewRecords(null);        //Query the controller

            DateTime selectedMonth = this.dateTimeMonth.Value;
            int year = DateTime.Now.Year;
            int month = selectedMonth.Month;
            IList<LogEntry> logEntries = this._helper.GetMonthLogs(this.ViewQueryResult, selectedMonth);
            int uniqueLogEntriesPerDate = logEntries.GroupBy(x => x.Created.Date).Distinct().Count();
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime startDate = this._helper.GetStartDate(selectedMonth);
            DateTime endDate = this._helper.GetEndDate(startDate);
            int saturdayCount = this._helper.CountDaysByDayName(DayOfWeek.Saturday, startDate, endDate);
            int sundayCount = this._helper.CountDaysByDayName(DayOfWeek.Sunday, startDate, endDate);
            int holidayCount = this._getHolidaysFunc(holiday =>
                ((holiday.Date.Ticks > startDate.Ticks) && (holiday.Date.Ticks < endDate.AddDays(1).Ticks))
                ).Count();
            int workdaysCount = (daysInMonth - (saturdayCount + sundayCount + holidayCount));

            this.lblHolidaysCount.Text = string.Format("Holidays Count: {0}", holidayCount.ToString());
            this.lblSaturdaysCount.Text = string.Format("Saturday Days Count: {0}", saturdayCount.ToString());
            this.lblSundayDaysCount.Text = string.Format("Sunday Days Count: {0}", sundayCount.ToString());
            this.lblWorkdaysCount.Text = string.Format("Workdays Count: {0}", workdaysCount.ToString());
            this.lblMonthDaysCount.Text = string.Format("Month Days Count: {0}", daysInMonth.ToString());
            this.lblLogCountsPerMonth.Text = string.Format("Unique Month Logs Count: {0}", uniqueLogEntriesPerDate.ToString());
            this.lblDaysCountWithNoLogs.Text = string.Format("Days Count Without Logs: {0}", (workdaysCount - uniqueLogEntriesPerDate).ToString());
        }

        private void dateTimeMonth_ValueChanged(object sender, EventArgs e)
        {
            this.RefreshGridData();
            this.RefreshDashboardData();
        }

        private void dGridLogs_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int index = 0; index < this.dGridLogs.Rows.Count; index++)
            {
                DataGridViewRow row = this.dGridLogs.Rows[index];
                DateTime created = DateTime.Parse(row.Cells[LogEntriesController.CREATED_INDEX].Value.ToString());
                string description = row.Cells[LogEntriesController.DESCRIPTION_INDEX].Value.ToString();
                string category = row.Cells[LogEntriesController.CATEGORY_INDEX].Value.ToString();

                if (this._helper.WeekendDate(created))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.Cells[LogEntriesController.DESCRIPTION_INDEX].Value = (string.IsNullOrEmpty(description)) ? LogEntriesController.WEEKEND : description;
                    row.Cells[LogEntriesController.CATEGORY_INDEX].Value = (string.IsNullOrEmpty(category)) ? LogEntriesController.WEEKEND : category;
                }
                else if (description == LogEntriesController.NO_DESCRIPTION)
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                else
                {
                    bool evenValue = ((created.DayOfYear % 2) == 0);

                    if(evenValue)
                        row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    else
                        row.DefaultCellStyle.BackColor = Color.GhostWhite;
                }
            }
        }

        private void dGridLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            try
            {
                this.UpdateTextView(index);

                DataGridViewRow row = this.dGridLogs.Rows[index];

                int primaryKey = int.Parse(row.Cells[LogEntriesController.ID_INDEX].Value.ToString());
                DateTime createdDate = DateTime.Parse(row.Cells[LogEntriesController.CREATED_INDEX].Value.ToString());
                DateTime systemCreatedDate = DateTime.Parse(row.Cells[LogEntriesController.SYSTEM_CREATED_INDEX].Value.ToString());
                string description = row.Cells[LogEntriesController.DESCRIPTION_INDEX].Value.ToString();
                string category = row.Cells[LogEntriesController.CATEGORY_INDEX].Value.ToString();
                bool rememberSetting = this._rememberSetting;
                DateTime rememberedCreatedDateTime = this._rememberedCreatedDateTime;

                this.SafeEditEntry(primaryKey, category, description, rememberSetting, createdDate, systemCreatedDate, rememberedCreatedDateTime);
            }
            catch(ArgumentOutOfRangeException)
            {
                /*Skip*/
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshGridData();
            this.RefreshDashboardData();
        }

        private void dGridLogs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpdateTextView(e.RowIndex);
        }

        void UpdateTextView(int dataGridRowIndex)
        {
            int index = dataGridRowIndex;

            try
            {
                DataGridViewRow row = this.dGridLogs.Rows[index];
                string description = row.Cells[LogEntriesController.DESCRIPTION_INDEX].Value.ToString();
                string category = row.Cells[LogEntriesController.CATEGORY_INDEX].Value.ToString();

                this.txtCategory.Clear();
                this.txtDescription.Clear();

                this.txtCategory.Text = category;
                this.txtDescription.Text = description;
            }
            catch (ArgumentOutOfRangeException) { /*Skip*/}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dGridLogs_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.DecorateGrid();
        }

        private void btnSummarizeLogs_Click(object sender, EventArgs e)
        {
            this.OpenSummarizedLogs(this.dateTimeMonth.Value);
        }

        void OpenSummarizedLogs(DateTime selectedMonth)
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   try
                   {
                       using (frmSummarizeLogs logs = new frmSummarizeLogs(_repo, selectedMonth))
                       {
                           logs.ShowDialog(this);
                           logs.Dispose();
                       }
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

        private void btnHoliday_Click(object sender, EventArgs e)
        {
            this.OpenHolidays();
        }

        void OpenHolidays()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   using (frmHolidays holiday = new frmHolidays(this._repo))
                   {
                       holiday.ShowDialog(this);
                       holiday.Dispose();
                   }
               }
           );

            if (this.InvokeRequired)
                this.Invoke(invokeFromUI);
            else
                invokeFromUI.Invoke();

            this.RefreshDashboardData();
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.OpenLeaves();
        }

        void OpenLeaves()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   using (frmLeaves leave = new frmLeaves(this._repo))
                   {
                       leave.ShowDialog(this);
                       leave.Dispose();
                   }
               }
           );

            if (this.InvokeRequired)
                this.Invoke(invokeFromUI);
            else
                invokeFromUI.Invoke();
        }
    }
}
