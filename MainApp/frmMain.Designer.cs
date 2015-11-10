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
            this.lblDaysCountWithNoLogs = new System.Windows.Forms.Label();
            this.lblLogCountsPerMonth = new System.Windows.Forms.Label();
            this.lblMonthDaysCount = new System.Windows.Forms.Label();
            this.lblWorkdaysCount = new System.Windows.Forms.Label();
            this.lblHolidaysCount = new System.Windows.Forms.Label();
            this.lblSundayDaysCount = new System.Windows.Forms.Label();
            this.lblSaturdaysCount = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSummarizeLogs = new System.Windows.Forms.Button();
            this.btnHoliday = new System.Windows.Forms.Button();
            this.btnLeave = new System.Windows.Forms.Button();
            this.lblLeavesCount = new System.Windows.Forms.Label();
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
            this.dGridLogs.Location = new System.Drawing.Point(12, 12);
            this.dGridLogs.Name = "dGridLogs";
            this.dGridLogs.RowTemplate.ReadOnly = true;
            this.dGridLogs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridLogs.Size = new System.Drawing.Size(895, 443);
            this.dGridLogs.TabIndex = 0;
            this.dGridLogs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridLogs_CellClick);
            this.dGridLogs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridLogs_CellDoubleClick);
            this.dGridLogs.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dGridLogs_DataBindingComplete);
            this.dGridLogs.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dGridLogs_RowPrePaint);
            // 
            // btnManuaTrackerEntry
            // 
            this.btnManuaTrackerEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnManuaTrackerEntry.Location = new System.Drawing.Point(392, 528);
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
            this.btnRefresh.Location = new System.Drawing.Point(16, 528);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(148, 30);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblLeavesCount);
            this.panel1.Controls.Add(this.lblDaysCountWithNoLogs);
            this.panel1.Controls.Add(this.lblLogCountsPerMonth);
            this.panel1.Controls.Add(this.lblMonthDaysCount);
            this.panel1.Controls.Add(this.lblWorkdaysCount);
            this.panel1.Controls.Add(this.lblHolidaysCount);
            this.panel1.Controls.Add(this.lblSundayDaysCount);
            this.panel1.Controls.Add(this.lblSaturdaysCount);
            this.panel1.Location = new System.Drawing.Point(692, 461);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 176);
            this.panel1.TabIndex = 13;
            // 
            // lblDaysCountWithNoLogs
            // 
            this.lblDaysCountWithNoLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDaysCountWithNoLogs.AutoSize = true;
            this.lblDaysCountWithNoLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaysCountWithNoLogs.ForeColor = System.Drawing.Color.Red;
            this.lblDaysCountWithNoLogs.Location = new System.Drawing.Point(12, 150);
            this.lblDaysCountWithNoLogs.Name = "lblDaysCountWithNoLogs";
            this.lblDaysCountWithNoLogs.Size = new System.Drawing.Size(159, 13);
            this.lblDaysCountWithNoLogs.TabIndex = 18;
            this.lblDaysCountWithNoLogs.Text = "Days Count Without Logs: ";
            // 
            // lblLogCountsPerMonth
            // 
            this.lblLogCountsPerMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogCountsPerMonth.AutoSize = true;
            this.lblLogCountsPerMonth.Location = new System.Drawing.Point(12, 130);
            this.lblLogCountsPerMonth.Name = "lblLogCountsPerMonth";
            this.lblLogCountsPerMonth.Size = new System.Drawing.Size(134, 13);
            this.lblLogCountsPerMonth.TabIndex = 17;
            this.lblLogCountsPerMonth.Text = "Unique Month Logs Count:";
            // 
            // lblMonthDaysCount
            // 
            this.lblMonthDaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMonthDaysCount.AutoSize = true;
            this.lblMonthDaysCount.Location = new System.Drawing.Point(12, 90);
            this.lblMonthDaysCount.Name = "lblMonthDaysCount";
            this.lblMonthDaysCount.Size = new System.Drawing.Size(98, 13);
            this.lblMonthDaysCount.TabIndex = 16;
            this.lblMonthDaysCount.Text = "Month Days Count:";
            // 
            // lblWorkdaysCount
            // 
            this.lblWorkdaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWorkdaysCount.AutoSize = true;
            this.lblWorkdaysCount.Location = new System.Drawing.Point(12, 110);
            this.lblWorkdaysCount.Name = "lblWorkdaysCount";
            this.lblWorkdaysCount.Size = new System.Drawing.Size(89, 13);
            this.lblWorkdaysCount.TabIndex = 15;
            this.lblWorkdaysCount.Text = "Workdays Count:";
            // 
            // lblHolidaysCount
            // 
            this.lblHolidaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHolidaysCount.AutoSize = true;
            this.lblHolidaysCount.Location = new System.Drawing.Point(12, 50);
            this.lblHolidaysCount.Name = "lblHolidaysCount";
            this.lblHolidaysCount.Size = new System.Drawing.Size(104, 13);
            this.lblHolidaysCount.TabIndex = 14;
            this.lblHolidaysCount.Text = "Holidays Count: N/A";
            // 
            // lblSundayDaysCount
            // 
            this.lblSundayDaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSundayDaysCount.AutoSize = true;
            this.lblSundayDaysCount.Location = new System.Drawing.Point(12, 30);
            this.lblSundayDaysCount.Name = "lblSundayDaysCount";
            this.lblSundayDaysCount.Size = new System.Drawing.Size(104, 13);
            this.lblSundayDaysCount.TabIndex = 13;
            this.lblSundayDaysCount.Text = "Sunday Days Count:";
            // 
            // lblSaturdaysCount
            // 
            this.lblSaturdaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSaturdaysCount.AutoSize = true;
            this.lblSaturdaysCount.Location = new System.Drawing.Point(12, 10);
            this.lblSaturdaysCount.Name = "lblSaturdaysCount";
            this.lblSaturdaysCount.Size = new System.Drawing.Size(110, 13);
            this.lblSaturdaysCount.TabIndex = 12;
            this.lblSaturdaysCount.Text = "Saturday Days Count:";
            // 
            // lblCategory
            // 
            this.lblCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(944, 12);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCategory.TabIndex = 14;
            this.lblCategory.Text = "Category:";
            // 
            // txtCategory
            // 
            this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCategory.Location = new System.Drawing.Point(947, 28);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.ReadOnly = true;
            this.txtCategory.Size = new System.Drawing.Size(150, 20);
            this.txtCategory.TabIndex = 15;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(947, 85);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(290, 552);
            this.txtDescription.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(944, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Description:";
            // 
            // btnSummarizeLogs
            // 
            this.btnSummarizeLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSummarizeLogs.Location = new System.Drawing.Point(13, 600);
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
            this.btnHoliday.Location = new System.Drawing.Point(392, 564);
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
            this.btnLeave.Location = new System.Drawing.Point(392, 600);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(151, 30);
            this.btnLeave.TabIndex = 20;
            this.btnLeave.Text = "Leave";
            this.btnLeave.UseVisualStyleBackColor = true;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // lblLeavesCount
            // 
            this.lblLeavesCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLeavesCount.AutoSize = true;
            this.lblLeavesCount.Location = new System.Drawing.Point(12, 70);
            this.lblLeavesCount.Name = "lblLeavesCount";
            this.lblLeavesCount.Size = new System.Drawing.Size(99, 13);
            this.lblLeavesCount.TabIndex = 19;
            this.lblLeavesCount.Text = "Leaves Count: N/A";
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1249, 649);
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
    }
}

