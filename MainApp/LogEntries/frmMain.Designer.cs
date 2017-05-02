namespace MainApp
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tryIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.dGridLogs = new System.Windows.Forms.DataGridView();
            this.btnManuaTrackerEntry = new System.Windows.Forms.Button();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblCurrentWeekRange = new System.Windows.Forms.Label();
            this.dateTimeMonth = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalHours = new System.Windows.Forms.Label();
            this.lblLeavesCount = new System.Windows.Forms.Label();
            this.lblDaysCountWithNoLogs = new System.Windows.Forms.Label();
            this.lblLogCountsPerMonth = new System.Windows.Forms.Label();
            this.lblMonthDaysCount = new System.Windows.Forms.Label();
            this.lblWorkdaysCount = new System.Windows.Forms.Label();
            this.lblSundayDaysCount = new System.Windows.Forms.Label();
            this.lblSaturdaysCount = new System.Windows.Forms.Label();
            this.lblHolidaysCount = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSummarizeLogs = new System.Windows.Forms.Button();
            this.btnHoliday = new System.Windows.Forms.Button();
            this.btnLeave = new System.Windows.Forms.Button();
            this.btnCategory = new System.Windows.Forms.Button();
            this.btnAttribute = new System.Windows.Forms.Button();
            this.btnActivity = new System.Windows.Forms.Button();
            this.btnDailyAttribute = new System.Windows.Forms.Button();
            this.btnPersonalNote = new System.Windows.Forms.Button();
            this.lblCountdown = new System.Windows.Forms.Label();
            this.btnDailyActivity = new System.Windows.Forms.Button();
            this.btnObjective = new System.Windows.Forms.Button();
            this.txtObjectives = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStandardOperatingProcedure = new System.Windows.Forms.Button();
            this.btnSummarizeHoursByCategories = new System.Windows.Forms.Button();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tryIcon
            // 
            this.tryIcon.BalloonTipText = "Time Tracker In-progress";
            this.tryIcon.BalloonTipTitle = "Time Tracker";
            this.tryIcon.Text = "notifyIcon1";
            this.tryIcon.Visible = true;
            this.tryIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tryIcon_MouseDoubleClick);
            // 
            // dGridLogs
            // 
            this.dGridLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGridLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridLogs.Location = new System.Drawing.Point(12, 69);
            this.dGridLogs.Name = "dGridLogs";
            this.dGridLogs.RowTemplate.ReadOnly = true;
            this.dGridLogs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridLogs.Size = new System.Drawing.Size(1089, 386);
            this.dGridLogs.TabIndex = 0;
            this.dGridLogs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridLogs_CellClick);
            this.dGridLogs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridLogs_CellDoubleClick);
            this.dGridLogs.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dGridLogs_DataBindingComplete);
            this.dGridLogs.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dGridLogs_RowPrePaint);
            // 
            // btnManuaTrackerEntry
            // 
            this.btnManuaTrackerEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnManuaTrackerEntry.Location = new System.Drawing.Point(197, 528);
            this.btnManuaTrackerEntry.Name = "btnManuaTrackerEntry";
            this.btnManuaTrackerEntry.Size = new System.Drawing.Size(151, 30);
            this.btnManuaTrackerEntry.TabIndex = 1;
            this.btnManuaTrackerEntry.Text = "Manual Entry";
            this.btnManuaTrackerEntry.UseVisualStyleBackColor = true;
            this.btnManuaTrackerEntry.Click += new System.EventHandler(this.btnManuaTrackerEntry_Click);
            // 
            // lblMonth
            // 
            this.lblMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(12, 480);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(77, 13);
            this.lblMonth.TabIndex = 6;
            this.lblMonth.Text = "Current Month:";
            // 
            // lblCurrentWeekRange
            // 
            this.lblCurrentWeekRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrentWeekRange.AutoSize = true;
            this.lblCurrentWeekRange.Location = new System.Drawing.Point(12, 503);
            this.lblCurrentWeekRange.Name = "lblCurrentWeekRange";
            this.lblCurrentWeekRange.Size = new System.Drawing.Size(134, 13);
            this.lblCurrentWeekRange.TabIndex = 7;
            this.lblCurrentWeekRange.Text = "Current Week Range: N/A";
            // 
            // dateTimeMonth
            // 
            this.dateTimeMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimeMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeMonth.Location = new System.Drawing.Point(95, 480);
            this.dateTimeMonth.Name = "dateTimeMonth";
            this.dateTimeMonth.ShowUpDown = true;
            this.dateTimeMonth.Size = new System.Drawing.Size(144, 20);
            this.dateTimeMonth.TabIndex = 8;
            this.dateTimeMonth.ValueChanged += new System.EventHandler(this.dateTimeMonth_ValueChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(201, 32);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(58, 33);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblTotalHours);
            this.panel1.Controls.Add(this.lblLeavesCount);
            this.panel1.Controls.Add(this.lblDaysCountWithNoLogs);
            this.panel1.Controls.Add(this.lblLogCountsPerMonth);
            this.panel1.Controls.Add(this.lblMonthDaysCount);
            this.panel1.Controls.Add(this.lblWorkdaysCount);
            this.panel1.Controls.Add(this.lblSundayDaysCount);
            this.panel1.Controls.Add(this.lblSaturdaysCount);
            this.panel1.Controls.Add(this.lblHolidaysCount);
            this.panel1.Location = new System.Drawing.Point(886, 461);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 189);
            this.panel1.TabIndex = 13;
            // 
            // lblTotalHours
            // 
            this.lblTotalHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalHours.AutoSize = true;
            this.lblTotalHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHours.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalHours.Location = new System.Drawing.Point(12, 145);
            this.lblTotalHours.Name = "lblTotalHours";
            this.lblTotalHours.Size = new System.Drawing.Size(107, 13);
            this.lblTotalHours.TabIndex = 20;
            this.lblTotalHours.Text = "Hours Rendered: ";
            // 
            // lblLeavesCount
            // 
            this.lblLeavesCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLeavesCount.AutoSize = true;
            this.lblLeavesCount.Location = new System.Drawing.Point(12, 26);
            this.lblLeavesCount.Name = "lblLeavesCount";
            this.lblLeavesCount.Size = new System.Drawing.Size(159, 13);
            this.lblLeavesCount.TabIndex = 19;
            this.lblLeavesCount.Text = "Leaves Count (Weekdays): N/A";
            // 
            // lblDaysCountWithNoLogs
            // 
            this.lblDaysCountWithNoLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDaysCountWithNoLogs.AutoSize = true;
            this.lblDaysCountWithNoLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaysCountWithNoLogs.ForeColor = System.Drawing.Color.Red;
            this.lblDaysCountWithNoLogs.Location = new System.Drawing.Point(12, 162);
            this.lblDaysCountWithNoLogs.Name = "lblDaysCountWithNoLogs";
            this.lblDaysCountWithNoLogs.Size = new System.Drawing.Size(159, 13);
            this.lblDaysCountWithNoLogs.TabIndex = 18;
            this.lblDaysCountWithNoLogs.Text = "Days Count Without Logs: ";
            // 
            // lblLogCountsPerMonth
            // 
            this.lblLogCountsPerMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogCountsPerMonth.AutoSize = true;
            this.lblLogCountsPerMonth.Location = new System.Drawing.Point(12, 126);
            this.lblLogCountsPerMonth.Name = "lblLogCountsPerMonth";
            this.lblLogCountsPerMonth.Size = new System.Drawing.Size(134, 13);
            this.lblLogCountsPerMonth.TabIndex = 17;
            this.lblLogCountsPerMonth.Text = "Unique Month Logs Count:";
            // 
            // lblMonthDaysCount
            // 
            this.lblMonthDaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMonthDaysCount.AutoSize = true;
            this.lblMonthDaysCount.Location = new System.Drawing.Point(12, 86);
            this.lblMonthDaysCount.Name = "lblMonthDaysCount";
            this.lblMonthDaysCount.Size = new System.Drawing.Size(98, 13);
            this.lblMonthDaysCount.TabIndex = 16;
            this.lblMonthDaysCount.Text = "Month Days Count:";
            // 
            // lblWorkdaysCount
            // 
            this.lblWorkdaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWorkdaysCount.AutoSize = true;
            this.lblWorkdaysCount.Location = new System.Drawing.Point(12, 106);
            this.lblWorkdaysCount.Name = "lblWorkdaysCount";
            this.lblWorkdaysCount.Size = new System.Drawing.Size(89, 13);
            this.lblWorkdaysCount.TabIndex = 15;
            this.lblWorkdaysCount.Text = "Workdays Count:";
            // 
            // lblSundayDaysCount
            // 
            this.lblSundayDaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSundayDaysCount.AutoSize = true;
            this.lblSundayDaysCount.Location = new System.Drawing.Point(12, 66);
            this.lblSundayDaysCount.Name = "lblSundayDaysCount";
            this.lblSundayDaysCount.Size = new System.Drawing.Size(104, 13);
            this.lblSundayDaysCount.TabIndex = 13;
            this.lblSundayDaysCount.Text = "Sunday Days Count:";
            // 
            // lblSaturdaysCount
            // 
            this.lblSaturdaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSaturdaysCount.AutoSize = true;
            this.lblSaturdaysCount.Location = new System.Drawing.Point(12, 46);
            this.lblSaturdaysCount.Name = "lblSaturdaysCount";
            this.lblSaturdaysCount.Size = new System.Drawing.Size(110, 13);
            this.lblSaturdaysCount.TabIndex = 12;
            this.lblSaturdaysCount.Text = "Saturday Days Count:";
            // 
            // lblHolidaysCount
            // 
            this.lblHolidaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHolidaysCount.AutoSize = true;
            this.lblHolidaysCount.Location = new System.Drawing.Point(12, 6);
            this.lblHolidaysCount.Name = "lblHolidaysCount";
            this.lblHolidaysCount.Size = new System.Drawing.Size(164, 13);
            this.lblHolidaysCount.TabIndex = 14;
            this.lblHolidaysCount.Text = "Holidays Count (Weekdays): N/A";
            // 
            // lblCategory
            // 
            this.lblCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(1138, 12);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCategory.TabIndex = 14;
            this.lblCategory.Text = "Category:";
            // 
            // txtCategory
            // 
            this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCategory.Location = new System.Drawing.Point(1141, 28);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.ReadOnly = true;
            this.txtCategory.Size = new System.Drawing.Size(150, 20);
            this.txtCategory.TabIndex = 15;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(1141, 85);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(290, 232);
            this.txtDescription.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1138, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Description:";
            // 
            // btnSummarizeLogs
            // 
            this.btnSummarizeLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSummarizeLogs.Location = new System.Drawing.Point(702, 530);
            this.btnSummarizeLogs.Name = "btnSummarizeLogs";
            this.btnSummarizeLogs.Size = new System.Drawing.Size(151, 30);
            this.btnSummarizeLogs.TabIndex = 18;
            this.btnSummarizeLogs.Text = "Summarize Logs";
            this.btnSummarizeLogs.UseVisualStyleBackColor = true;
            this.btnSummarizeLogs.Click += new System.EventHandler(this.btnSummarizeLogs_Click);
            // 
            // btnHoliday
            // 
            this.btnHoliday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHoliday.Location = new System.Drawing.Point(364, 564);
            this.btnHoliday.Name = "btnHoliday";
            this.btnHoliday.Size = new System.Drawing.Size(151, 30);
            this.btnHoliday.TabIndex = 19;
            this.btnHoliday.Text = "Holiday";
            this.btnHoliday.UseVisualStyleBackColor = true;
            this.btnHoliday.Click += new System.EventHandler(this.btnHoliday_Click);
            // 
            // btnLeave
            // 
            this.btnLeave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLeave.Location = new System.Drawing.Point(364, 600);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(151, 30);
            this.btnLeave.TabIndex = 20;
            this.btnLeave.Text = "Leave";
            this.btnLeave.UseVisualStyleBackColor = true;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // btnCategory
            // 
            this.btnCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCategory.Location = new System.Drawing.Point(364, 528);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(151, 30);
            this.btnCategory.TabIndex = 21;
            this.btnCategory.Text = "Category";
            this.btnCategory.UseVisualStyleBackColor = true;
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // btnAttribute
            // 
            this.btnAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAttribute.Location = new System.Drawing.Point(535, 528);
            this.btnAttribute.Name = "btnAttribute";
            this.btnAttribute.Size = new System.Drawing.Size(151, 30);
            this.btnAttribute.TabIndex = 22;
            this.btnAttribute.Text = "Attribute";
            this.btnAttribute.UseVisualStyleBackColor = true;
            this.btnAttribute.Click += new System.EventHandler(this.btnAttribute_Click);
            // 
            // btnActivity
            // 
            this.btnActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActivity.Location = new System.Drawing.Point(535, 564);
            this.btnActivity.Name = "btnActivity";
            this.btnActivity.Size = new System.Drawing.Size(151, 30);
            this.btnActivity.TabIndex = 23;
            this.btnActivity.Text = "Activity";
            this.btnActivity.UseVisualStyleBackColor = true;
            this.btnActivity.Click += new System.EventHandler(this.btnActivity_Click);
            // 
            // btnDailyAttribute
            // 
            this.btnDailyAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDailyAttribute.Enabled = false;
            this.btnDailyAttribute.Location = new System.Drawing.Point(197, 600);
            this.btnDailyAttribute.Name = "btnDailyAttribute";
            this.btnDailyAttribute.Size = new System.Drawing.Size(151, 30);
            this.btnDailyAttribute.TabIndex = 24;
            this.btnDailyAttribute.Text = "Daily Attributes";
            this.btnDailyAttribute.UseVisualStyleBackColor = true;
            this.btnDailyAttribute.Click += new System.EventHandler(this.btnDailyAttribute_Click);
            // 
            // btnPersonalNote
            // 
            this.btnPersonalNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPersonalNote.Location = new System.Drawing.Point(535, 600);
            this.btnPersonalNote.Name = "btnPersonalNote";
            this.btnPersonalNote.Size = new System.Drawing.Size(151, 30);
            this.btnPersonalNote.TabIndex = 25;
            this.btnPersonalNote.Text = "Note";
            this.btnPersonalNote.UseVisualStyleBackColor = true;
            this.btnPersonalNote.Click += new System.EventHandler(this.btnPersonalNote_Click);
            // 
            // lblCountdown
            // 
            this.lblCountdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountdown.AutoSize = true;
            this.lblCountdown.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblCountdown.Location = new System.Drawing.Point(13, 12);
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Size = new System.Drawing.Size(106, 13);
            this.lblCountdown.TabIndex = 26;
            this.lblCountdown.Text = "Next Tracker Popup:";
            // 
            // btnDailyActivity
            // 
            this.btnDailyActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDailyActivity.Location = new System.Drawing.Point(196, 564);
            this.btnDailyActivity.Name = "btnDailyActivity";
            this.btnDailyActivity.Size = new System.Drawing.Size(151, 30);
            this.btnDailyActivity.TabIndex = 27;
            this.btnDailyActivity.Text = "Daily Activities";
            this.btnDailyActivity.UseVisualStyleBackColor = true;
            this.btnDailyActivity.Click += new System.EventHandler(this.btnDailyActivity_Click);
            // 
            // btnObjective
            // 
            this.btnObjective.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnObjective.Location = new System.Drawing.Point(16, 528);
            this.btnObjective.Name = "btnObjective";
            this.btnObjective.Size = new System.Drawing.Size(151, 30);
            this.btnObjective.TabIndex = 28;
            this.btnObjective.Text = "Objective";
            this.btnObjective.UseVisualStyleBackColor = true;
            this.btnObjective.Click += new System.EventHandler(this.btnObjective_Click);
            // 
            // txtObjectives
            // 
            this.txtObjectives.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObjectives.Location = new System.Drawing.Point(1141, 369);
            this.txtObjectives.Multiline = true;
            this.txtObjectives.Name = "txtObjectives";
            this.txtObjectives.ReadOnly = true;
            this.txtObjectives.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObjectives.Size = new System.Drawing.Size(290, 268);
            this.txtObjectives.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(1138, 353);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Selected Date Objectives:";
            // 
            // btnStandardOperatingProcedure
            // 
            this.btnStandardOperatingProcedure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStandardOperatingProcedure.Location = new System.Drawing.Point(16, 600);
            this.btnStandardOperatingProcedure.Name = "btnStandardOperatingProcedure";
            this.btnStandardOperatingProcedure.Size = new System.Drawing.Size(151, 30);
            this.btnStandardOperatingProcedure.TabIndex = 31;
            this.btnStandardOperatingProcedure.Text = "S.O.P";
            this.btnStandardOperatingProcedure.UseVisualStyleBackColor = true;
            this.btnStandardOperatingProcedure.Click += new System.EventHandler(this.btnStandardOperatingProcedure_Click);
            // 
            // btnSummarizeHoursByCategories
            // 
            this.btnSummarizeHoursByCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSummarizeHoursByCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSummarizeHoursByCategories.Location = new System.Drawing.Point(692, 600);
            this.btnSummarizeHoursByCategories.Name = "btnSummarizeHoursByCategories";
            this.btnSummarizeHoursByCategories.Size = new System.Drawing.Size(173, 30);
            this.btnSummarizeHoursByCategories.TabIndex = 32;
            this.btnSummarizeHoursByCategories.Text = "Summarize Hours By Categories";
            this.btnSummarizeHoursByCategories.UseVisualStyleBackColor = true;
            this.btnSummarizeHoursByCategories.Click += new System.EventHandler(this.btnSummarizeHoursByCategories_Click);
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(74, 39);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(121, 21);
            this.cboCategory.TabIndex = 33;
            this.cboCategory.SelectedValueChanged += new System.EventHandler(this.cboCategory_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Category: ";
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1443, 649);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.btnSummarizeHoursByCategories);
            this.Controls.Add(this.btnStandardOperatingProcedure);
            this.Controls.Add(this.txtObjectives);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnObjective);
            this.Controls.Add(this.btnDailyActivity);
            this.Controls.Add(this.lblCountdown);
            this.Controls.Add(this.btnPersonalNote);
            this.Controls.Add(this.btnDailyAttribute);
            this.Controls.Add(this.btnActivity);
            this.Controls.Add(this.btnAttribute);
            this.Controls.Add(this.btnCategory);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.btnHoliday);
            this.Controls.Add(this.btnSummarizeLogs);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dateTimeMonth);
            this.Controls.Add(this.lblCurrentWeekRange);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.btnManuaTrackerEntry);
            this.Controls.Add(this.dGridLogs);
            this.Name = "frmMain";
            this.Text = "Main";
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon tryIcon;
        private System.Windows.Forms.DataGridView dGridLogs;
        private System.Windows.Forms.Button btnManuaTrackerEntry;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblCurrentWeekRange;
        private System.Windows.Forms.DateTimePicker dateTimeMonth;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDaysCountWithNoLogs;
        private System.Windows.Forms.Label lblLogCountsPerMonth;
        private System.Windows.Forms.Label lblMonthDaysCount;
        private System.Windows.Forms.Label lblWorkdaysCount;
        private System.Windows.Forms.Label lblHolidaysCount;
        private System.Windows.Forms.Label lblSundayDaysCount;
        private System.Windows.Forms.Label lblSaturdaysCount;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSummarizeLogs;
        private System.Windows.Forms.Button btnHoliday;
        private System.Windows.Forms.Button btnLeave;
        private System.Windows.Forms.Label lblLeavesCount;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.Button btnAttribute;
        private System.Windows.Forms.Button btnActivity;
        private System.Windows.Forms.Button btnDailyAttribute;
        private System.Windows.Forms.Button btnPersonalNote;
        private System.Windows.Forms.Label lblCountdown;
        private System.Windows.Forms.Button btnDailyActivity;
        private System.Windows.Forms.Button btnObjective;
        private System.Windows.Forms.TextBox txtObjectives;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStandardOperatingProcedure;
        private System.Windows.Forms.Label lblTotalHours;
        private System.Windows.Forms.Button btnSummarizeHoursByCategories;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label label3;
    }
}

