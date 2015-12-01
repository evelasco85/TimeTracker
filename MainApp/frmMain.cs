using Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Domain.Infrastructure;
using Domain.Helpers;
using Domain.Controllers;
using Domain.Views;

namespace MainApp
{
    public partial class frmMain : Form, ILogView
    {
        System.Timers.Timer _timerNotification;
        bool _promptingInProgress = false;
        int _secondsRemaining;

        IEFRepository _repository;

        public Action<Func<LogEntry, bool>> View_QueryRecords { get; set; }
        public Action View_OnQueryRecordsCompletion { get; set; }
        public Action<Func<LogEntry, bool>> View_DeleteRecords { get; set; }
        public Action<LogEntry> View_SaveRecord { get; set; }
        public Action<IEnumerable<LogEntry>, DateTime> View_GetLogStatistics { get; set; }
        public Action<int, int, int, int, int, int, int, int> View_OnGetLogStatisticsCompletion { get; set; }
        public Action<IEnumerable<LogEntry>, DateTime> View_GetCalendarData { get; set; }
        public Action<dynamic, DateTime> View_OnGetCalendarDataCompletion { get; set; }
        public IEnumerable<LogEntry> View_QueryResults { get; set; }
        public Func<bool> View_GetRememberedSetting { get; set; }
        public Action<bool> View_SetRememberedSetting { get; set; }

        public Func<DateTime> View_GetRememberedDate { get; set; }
        public Action<DateTime> View_SetRememberedDate { get; set; }
        public Func<IEnumerable<Category>> View_GetCategories { get; set; }
        public Action<DateTime> View_GetObjectiveData { get; set; }
        public Action<string> View_OnGetObjectiveDataCompletion { get; set; }

        public frmMain(IEFRepository repository)
        {
            Action RegisterController = () =>
                {
                   LogEntriesController controller =new LogEntriesController(repository, this);
                };

            RegisterController();

            this._repository = repository;
            this.View_OnQueryRecordsCompletion = this.RefreshGridData;
            this.View_OnGetLogStatisticsCompletion = this.UpdateDashboard;
            this.View_OnGetCalendarDataCompletion = this.UpdateCalendarData;
            this.View_OnGetObjectiveDataCompletion = this.UpdateObjectiveData;

            InitializeComponent();
            this.InitializeRequiredData();
            this.View_QueryRecords(null);
            this.View_GetObjectiveData(DateTime.Now);
            this.RefreshDashboardData();
            this.SetTimer();
            this.StartTimer();
        }

        void InitializeRequiredData()
        {
            this.tryIcon.BalloonTipIcon = ToolTipIcon.Info;
            this.tryIcon.Icon = Resource1.MSN;
            this.dateTimeMonth.CustomFormat = "MMMM";
        }

        void RefreshGridData()
        {
            IEnumerable<LogEntry> log = this.View_QueryResults;
            DateTime selectedMonth = this.dateTimeMonth.Value;

            this.View_GetCalendarData(log, selectedMonth);
        }

        void UpdateCalendarData(dynamic displayColumns, DateTime lastUpdatedDate)
        {
            this.dGridLogs.DataSource = displayColumns;

            this.dGridLogs.Refresh();
            this.HighlightRecordByDate(lastUpdatedDate);
        }

        void DecorateGrid()
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

        #region Timer Configuration and Setup

        int GetMillisecondCountDown()
        {
            Properties.Settings settings = MainApp.Properties.Settings.Default;

            int minutes = (settings.timerMinute) + (settings.timerHour * 60);
            int seconds = settings.timerSecord + (minutes * 60);

            int milliSeconds = (seconds * 1000) + settings.timerMillisecond;

            return milliSeconds;
        }

        int GetSeconds(int milliseconds)
        {
            return milliseconds / 1000;
        }

        void SetTimer()
        {
            this._timerNotification = new System.Timers.Timer(1000);    //Per second update
            this._timerNotification.Elapsed += TimerNotification_Elapsed;
            this._timerNotification.AutoReset = true;
        }

        void StartTimer()
        {
            this._secondsRemaining = this.GetSeconds(this.GetMillisecondCountDown());

            this._timerNotification.Start();
        }

        void StopTimer()
        {
            this._secondsRemaining = 0;

            this._timerNotification.Stop();
        }

        void RestartTimer()
        {
            this.StopTimer();
            this.StartTimer();
        }

        void UpdateTimerCountdown()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   this.lblCountdown.Text = string.Format(
                         "Next Tracker Popup: {0}",
                         TimeSpan
                             .FromSeconds(Convert.ToDouble(this._secondsRemaining))
                             .ToString(@"hh\:mm\:ss")
                         );

                   this._secondsRemaining -= 1;
               }
           );

            if (this.InvokeRequired)
                this.Invoke(invokeFromUI);
            else
                invokeFromUI.Invoke();
        }

        void TimerNotification_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this._secondsRemaining > 0)
                this.UpdateTimerCountdown();
            else
                this.AddEntry();
        }

        #endregion


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
                            this.View_GetCategories()
                                .Select(x => x.Name),
                            primaryKey, category, description, rememberSetting, createdDate, systemCreatedDate,
                            this.txtObjectives.Text
                            ))
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
                    this.View_GetCategories()
                        .Select(x => x.Name),
                    this.View_GetRememberedSetting(),
                    this.View_GetRememberedDate(),
                    this.txtObjectives.Text
                    )
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

            this.View_SetRememberedSetting(monitoring.RememberSetting);
            this.View_SetRememberedDate(logEntry.Created);

            try
            {
                if (logEntry.System_Created == DateTime.MinValue)
                    logEntry.System_Created = DateTime.Now;

                this.View_SaveRecord(logEntry);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            this.View_QueryRecords(null);
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
            IEnumerable<LogEntry> logs = this.View_QueryResults;
            this.View_GetLogStatistics(logs, selectedMonth);
        }

        void UpdateDashboard(int holidayCount, int leaveCount, int saturdayCount, int sundayCount, int workdaysCount, int daysInMonth, int uniqueLogEntriesPerDate, int daysCountWithoutLogs)
        {
            this.lblHolidaysCount.Text = string.Format("Holidays Count (Weekdays): {0}", holidayCount.ToString());
            this.lblLeavesCount.Text = string.Format("Leaves Count (Weekdays): {0}", leaveCount.ToString());
            this.lblSaturdaysCount.Text = string.Format("Saturday Days Count: {0}", saturdayCount.ToString());
            this.lblSundayDaysCount.Text = string.Format("Sunday Days Count: {0}", sundayCount.ToString());
            this.lblWorkdaysCount.Text = string.Format("Workdays Count: {0} = {1} - ({2} + {3})",
                workdaysCount.ToString(),
                workdaysCount + (leaveCount + holidayCount),
                leaveCount,
                holidayCount
                );
            this.lblMonthDaysCount.Text = string.Format("Month Days Count: {0}", daysInMonth.ToString());
            this.lblLogCountsPerMonth.Text = string.Format("Unique Month Logs Count: {0}", uniqueLogEntriesPerDate.ToString());
            this.lblDaysCountWithNoLogs.Text = string.Format("Days Count Without Logs: {0}", daysCountWithoutLogs.ToString());
        }

        private void dateTimeMonth_ValueChanged(object sender, EventArgs e)
        {
            this.View_QueryRecords(null);
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
                bool rememberSetting = this.View_GetRememberedSetting();
                DateTime rememberedCreatedDateTime = this.View_GetRememberedDate();

                this.SafeEditEntry(primaryKey, category, description, rememberSetting, createdDate, systemCreatedDate, rememberedCreatedDateTime);
            }
            catch(ArgumentOutOfRangeException)
            {
                /*Skip*/
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.View_QueryRecords(null);
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
                
                this.View_GetObjectiveData(DateTime.Parse(row.Cells[LogEntriesController.CREATED_INDEX].Value.ToString()));
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

        void UpdateObjectiveData(string objectives)
        {
            this.txtObjectives.Clear();
            this.txtObjectives.Text = objectives;
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

            this.View_QueryRecords(null);
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

            this.View_QueryRecords(null);
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

        void OpenActivities()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   using (frmActivity leave = new frmActivity(this._repository))
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

        void OpenDailyAttributes()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   using (frmDailyAttribute leave = new frmDailyAttribute(this._repository))
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

        void OpenPersonalNote()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   using (frmPersonalNotes personalNote = new frmPersonalNotes(this._repository))
                   {
                       personalNote.ShowDialog(this);
                       personalNote.Dispose();
                   }
               }
           );

            if (this.InvokeRequired)
                this.Invoke(invokeFromUI);
            else
                invokeFromUI.Invoke();
        }


        void OpenDailyActivity()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   using (frmDailyActivity dailyActivity = new frmDailyActivity(this._repository))
                   {
                       dailyActivity.ShowDialog(this);
                       dailyActivity.Dispose();
                   }
               }
           );

            if (this.InvokeRequired)
                this.Invoke(invokeFromUI);
            else
                invokeFromUI.Invoke();
        }


        void OpenObjective()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   using (frmObjectives objective = new frmObjectives(this._repository))
                   {
                       objective.ShowDialog(this);
                       objective.Dispose();
                   }
               }
           );

            if (this.InvokeRequired)
                this.Invoke(invokeFromUI);
            else
                invokeFromUI.Invoke();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            this.OpenCategories();
        }

        private void btnAttribute_Click(object sender, EventArgs e)
        {
            this.OpenAttributes();
        }

        private void btnActivity_Click(object sender, EventArgs e)
        {
            this.OpenActivities();
        }

        private void btnDailyAttribute_Click(object sender, EventArgs e)
        {
            this.OpenDailyAttributes();
        }

        private void btnPersonalNote_Click(object sender, EventArgs e)
        {
            this.OpenPersonalNote();
        }

        private void btnDailyActivity_Click(object sender, EventArgs e)
        {
            this.OpenDailyActivity();
        }

        private void btnObjective_Click(object sender, EventArgs e)
        {
            this.OpenObjective();
        }
    }
}
