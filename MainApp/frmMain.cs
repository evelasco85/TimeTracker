using Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Domain.Infrastructure;
using Domain.Helpers;
using Domain.Controller;

namespace MainApp
{
    public partial class frmMain : Form, ILogView
    {
        System.Timers.Timer _timerNotification;
        bool _promptingInProgress = false;
        
        IEFRepository _repository;

        public Action<Func<LogEntry, bool>> QueryViewRecords { get; set; }
        public Action OnQueryViewRecordsCompletion { get; set; }
        public Action<Func<LogEntry, bool>> DeleteViewRecords { get; set; }
        public Action<LogEntry> SaveViewRecord { get; set; }
        public Action<IEnumerable<LogEntry>, DateTime> GetLogStatistics { get; set; }
        public Action<int, int, int, int, int, int, int, int> OnGetLogStatisticsCompletion { get; set; }
        public Action<IEnumerable<LogEntry>, DateTime> GetCalendarData { get; set; }
        public Action<dynamic, DateTime> OnGetCalendarDataCompletion { get; set; }
        public IEnumerable<LogEntry> ViewQueryResult { get; set; }
        public Func<bool> GetRememberedSetting { get; set; }
        public Action<bool> SetRememberedSetting { get; set; }

        public Func<DateTime> GetRememberedDate { get; set; }
        public Action<DateTime> SetRememberedDate { get; set; }
        public Func<IEnumerable<Category>> GetCategories { get; set; }

        public frmMain(IEFRepository repository)
        {
            Action RegisterController = () =>
                {
                   LogEntriesController controller =new LogEntriesController(repository, this);
                };

            RegisterController();

            this._repository = repository;
            this.OnQueryViewRecordsCompletion = this.RefreshGridData;
            this.OnGetLogStatisticsCompletion = this.UpdateDashboard;
            this.OnGetCalendarDataCompletion = this.UpdateCalendarData;

            InitializeComponent();
            this.InitializeRequiredData();
            this.QueryViewRecords(null);
            this.RefreshDashboardData();
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
            IEnumerable<LogEntry> log = this.ViewQueryResult;
            DateTime selectedMonth = this.dateTimeMonth.Value;

            this.GetCalendarData(log, selectedMonth);
        }

        void UpdateCalendarData(dynamic displayColumns, DateTime lastUpdatedDate)
        {
            this.dGridLogs.DataSource = displayColumns;

            this.dGridLogs.Refresh();
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
                            this.GetCategories()
                                .Select(x => x.Name),
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

                using (frmTaskMonitoringEntry monitoring = new frmTaskMonitoringEntry(
                    this.GetCategories()
                        .Select(x => x.Name),
                    this.GetRememberedSetting(),
                    this.GetRememberedDate())
                    )
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

            this.SetRememberedSetting(monitoring.RememberSetting);
            this.SetRememberedDate(logEntry.Created);

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

            this.QueryViewRecords(null);
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
            DateTime selectedMonth = this.dateTimeMonth.Value;
            IEnumerable<LogEntry> logs = this.ViewQueryResult;
            this.GetLogStatistics(logs, selectedMonth);
        }

        void UpdateDashboard(int holidayCount, int leaveCount, int saturdayCount, int sundayCount, int workdaysCount, int daysInMonth, int uniqueLogEntriesPerDate, int daysCountWithoutLogs)
        {
            this.lblHolidaysCount.Text = string.Format("Holidays Count (Weekdays): {0}", holidayCount.ToString());
            this.lblLeavesCount.Text = string.Format("Leaves Count (Weekdays): {0}", leaveCount.ToString());
            this.lblSaturdaysCount.Text = string.Format("Saturday Days Count: {0}", saturdayCount.ToString());
            this.lblSundayDaysCount.Text = string.Format("Sunday Days Count: {0}", sundayCount.ToString());
            this.lblWorkdaysCount.Text = string.Format("Workdays Count: {0}", workdaysCount.ToString());
            this.lblMonthDaysCount.Text = string.Format("Month Days Count: {0}", daysInMonth.ToString());
            this.lblLogCountsPerMonth.Text = string.Format("Unique Month Logs Count: {0}", uniqueLogEntriesPerDate.ToString());
            this.lblDaysCountWithNoLogs.Text = string.Format("Days Count Without Logs: {0}", daysCountWithoutLogs.ToString());
        }

        private void dateTimeMonth_ValueChanged(object sender, EventArgs e)
        {
            this.QueryViewRecords(null);
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

                if((category == LogEntriesController.HOLIDAY) || (category == LogEntriesController.LEAVE))
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                }
                else if (DateHelper.GetInstance().WeekendDate(created))
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

                string category = row.Cells[LogEntriesController.CATEGORY_INDEX].Value.ToString();

                if ((category == LogEntriesController.HOLIDAY) || (category == LogEntriesController.LEAVE))
                    return;

                int primaryKey = int.Parse(row.Cells[LogEntriesController.ID_INDEX].Value.ToString());
                DateTime createdDate = DateTime.Parse(row.Cells[LogEntriesController.CREATED_INDEX].Value.ToString());
                DateTime systemCreatedDate = DateTime.Parse(row.Cells[LogEntriesController.SYSTEM_CREATED_INDEX].Value.ToString());
                string description = row.Cells[LogEntriesController.DESCRIPTION_INDEX].Value.ToString();
                bool rememberSetting = this.GetRememberedSetting();
                DateTime rememberedCreatedDateTime = this.GetRememberedDate();

                this.SafeEditEntry(primaryKey, category, description, rememberSetting, createdDate, systemCreatedDate, rememberedCreatedDateTime);
            }
            catch(ArgumentOutOfRangeException)
            {
                /*Skip*/
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.QueryViewRecords(null);
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
                       using (frmSummarizeLogs logs = new frmSummarizeLogs(_repository, selectedMonth))
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
                   using (frmHolidays holiday = new frmHolidays(this._repository))
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

        void OpenCategories()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   using (frmCategory category = new frmCategory(this._repository))
                   {
                       category.ShowDialog(this);
                       category.Dispose();
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
                   using (frmLeaves leave = new frmLeaves(this._repository))
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

            this.RefreshDashboardData();
        }

        void OpenAttributes()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   using (frmAttribute leave = new frmAttribute(this._repository))
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

            this.RefreshDashboardData();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            this.OpenCategories();
        }

        private void btnAttribute_Click(object sender, EventArgs e)
        {
            this.OpenAttributes();
        }
    }
}
