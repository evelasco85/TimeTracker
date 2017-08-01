namespace MainApp.DailyHours
{
    partial class frmDailyHours
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
            this.btnIncementDayByOne = new System.Windows.Forms.Button();
            this.lblDateTimeEntry = new System.Windows.Forms.Label();
            this.dateTimeManualEntry = new System.Windows.Forms.DateTimePicker();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHoursUnrecorded = new System.Windows.Forms.TextBox();
            this.dGridLogs = new System.Windows.Forms.DataGridView();
            this.btnManuaTrackerEntry = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIncementDayByOne
            // 
            this.btnIncementDayByOne.Location = new System.Drawing.Point(283, 12);
            this.btnIncementDayByOne.Name = "btnIncementDayByOne";
            this.btnIncementDayByOne.Size = new System.Drawing.Size(108, 23);
            this.btnIncementDayByOne.TabIndex = 12;
            this.btnIncementDayByOne.Text = "Increment Day by 1";
            this.btnIncementDayByOne.UseVisualStyleBackColor = true;
            // 
            // lblDateTimeEntry
            // 
            this.lblDateTimeEntry.AutoSize = true;
            this.lblDateTimeEntry.Location = new System.Drawing.Point(43, 17);
            this.lblDateTimeEntry.Name = "lblDateTimeEntry";
            this.lblDateTimeEntry.Size = new System.Drawing.Size(88, 13);
            this.lblDateTimeEntry.TabIndex = 10;
            this.lblDateTimeEntry.Text = "Date/Time Entry:";
            // 
            // dateTimeManualEntry
            // 
            this.dateTimeManualEntry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeManualEntry.Location = new System.Drawing.Point(177, 13);
            this.dateTimeManualEntry.Name = "dateTimeManualEntry";
            this.dateTimeManualEntry.Size = new System.Drawing.Size(100, 20);
            this.dateTimeManualEntry.TabIndex = 11;
            // 
            // txtHours
            // 
            this.txtHours.Location = new System.Drawing.Point(177, 39);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(100, 20);
            this.txtHours.TabIndex = 38;
            this.txtHours.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Actual Hour(s) Rendered:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Hour(s) Unrecorded:";
            // 
            // txtHoursUnrecorded
            // 
            this.txtHoursUnrecorded.Location = new System.Drawing.Point(177, 67);
            this.txtHoursUnrecorded.Name = "txtHoursUnrecorded";
            this.txtHoursUnrecorded.ReadOnly = true;
            this.txtHoursUnrecorded.Size = new System.Drawing.Size(100, 20);
            this.txtHoursUnrecorded.TabIndex = 40;
            this.txtHoursUnrecorded.Text = "0";
            // 
            // dGridLogs
            // 
            this.dGridLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGridLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridLogs.Location = new System.Drawing.Point(31, 105);
            this.dGridLogs.Name = "dGridLogs";
            this.dGridLogs.RowTemplate.ReadOnly = true;
            this.dGridLogs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridLogs.Size = new System.Drawing.Size(482, 282);
            this.dGridLogs.TabIndex = 41;
            // 
            // btnManuaTrackerEntry
            // 
            this.btnManuaTrackerEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnManuaTrackerEntry.Location = new System.Drawing.Point(362, 404);
            this.btnManuaTrackerEntry.Name = "btnManuaTrackerEntry";
            this.btnManuaTrackerEntry.Size = new System.Drawing.Size(151, 30);
            this.btnManuaTrackerEntry.TabIndex = 42;
            this.btnManuaTrackerEntry.Text = "Manual Entry";
            this.btnManuaTrackerEntry.UseVisualStyleBackColor = true;
            // 
            // frmDailyHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 446);
            this.Controls.Add(this.btnManuaTrackerEntry);
            this.Controls.Add(this.dGridLogs);
            this.Controls.Add(this.txtHoursUnrecorded);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHours);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnIncementDayByOne);
            this.Controls.Add(this.lblDateTimeEntry);
            this.Controls.Add(this.dateTimeManualEntry);
            this.Name = "frmDailyHours";
            this.Text = "Daily Hours";
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIncementDayByOne;
        private System.Windows.Forms.Label lblDateTimeEntry;
        private System.Windows.Forms.DateTimePicker dateTimeManualEntry;
        private System.Windows.Forms.TextBox txtHours;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHoursUnrecorded;
        private System.Windows.Forms.DataGridView dGridLogs;
        private System.Windows.Forms.Button btnManuaTrackerEntry;
    }
}