namespace MainApp
{
    partial class frmTaskMonitoringEntry
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
            this.lblTaskDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.dateTimeManualEntry = new System.Windows.Forms.DateTimePicker();
            this.lblDateTimeEntry = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkRememberSettings = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblDay = new System.Windows.Forms.Label();
            this.btnIncementDayByOne = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtObjectives = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTaskDescription
            // 
            this.lblTaskDescription.AutoSize = true;
            this.lblTaskDescription.Location = new System.Drawing.Point(5, 287);
            this.lblTaskDescription.Name = "lblTaskDescription";
            this.lblTaskDescription.Size = new System.Drawing.Size(90, 13);
            this.lblTaskDescription.TabIndex = 0;
            this.lblTaskDescription.Text = "Task Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(103, 287);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(476, 168);
            this.txtDescription.TabIndex = 2;
            // 
            // dateTimeManualEntry
            // 
            this.dateTimeManualEntry.Location = new System.Drawing.Point(103, 461);
            this.dateTimeManualEntry.Name = "dateTimeManualEntry";
            this.dateTimeManualEntry.Size = new System.Drawing.Size(249, 20);
            this.dateTimeManualEntry.TabIndex = 3;
            this.dateTimeManualEntry.ValueChanged += new System.EventHandler(this.dateTimeManualEntry_ValueChanged);
            // 
            // lblDateTimeEntry
            // 
            this.lblDateTimeEntry.AutoSize = true;
            this.lblDateTimeEntry.Location = new System.Drawing.Point(7, 465);
            this.lblDateTimeEntry.Name = "lblDateTimeEntry";
            this.lblDateTimeEntry.Size = new System.Drawing.Size(88, 13);
            this.lblDateTimeEntry.TabIndex = 3;
            this.lblDateTimeEntry.Text = "Date/Time Entry:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(235, 549);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkRememberSettings
            // 
            this.chkRememberSettings.AutoSize = true;
            this.chkRememberSettings.Checked = true;
            this.chkRememberSettings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRememberSettings.Location = new System.Drawing.Point(103, 522);
            this.chkRememberSettings.Name = "chkRememberSettings";
            this.chkRememberSettings.Size = new System.Drawing.Size(118, 17);
            this.chkRememberSettings.TabIndex = 4;
            this.chkRememberSettings.Text = "Remember Settings";
            this.chkRememberSettings.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Category:";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(103, 242);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(173, 21);
            this.cboCategory.TabIndex = 1;
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDay.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblDay.Location = new System.Drawing.Point(516, 468);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(29, 13);
            this.lblDay.TabIndex = 8;
            this.lblDay.Text = "Day";
            // 
            // btnIncementDayByOne
            // 
            this.btnIncementDayByOne.Location = new System.Drawing.Point(380, 463);
            this.btnIncementDayByOne.Name = "btnIncementDayByOne";
            this.btnIncementDayByOne.Size = new System.Drawing.Size(108, 23);
            this.btnIncementDayByOne.TabIndex = 9;
            this.btnIncementDayByOne.Text = "Increment Day by 1";
            this.btnIncementDayByOne.UseVisualStyleBackColor = true;
            this.btnIncementDayByOne.Click += new System.EventHandler(this.btnIncementDayByOne_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(343, 549);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtObjectives
            // 
            this.txtObjectives.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtObjectives.Location = new System.Drawing.Point(103, 25);
            this.txtObjectives.Multiline = true;
            this.txtObjectives.Name = "txtObjectives";
            this.txtObjectives.ReadOnly = true;
            this.txtObjectives.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtObjectives.Size = new System.Drawing.Size(476, 192);
            this.txtObjectives.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(1, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Selected Date Objectives:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // frmTaskMonitoringEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(596, 648);
            this.ControlBox = false;
            this.Controls.Add(this.txtObjectives);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnIncementDayByOne);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkRememberSettings);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblDateTimeEntry);
            this.Controls.Add(this.dateTimeManualEntry);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblTaskDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmTaskMonitoringEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitoring Entry";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmTaskMonitoringEntry_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTaskDescription;
        private System.Windows.Forms.Label lblDateTimeEntry;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Button btnIncementDayByOne;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.DateTimePicker dateTimeManualEntry;
        private System.Windows.Forms.CheckBox chkRememberSettings;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtObjectives;
        private System.Windows.Forms.Label label2;
    }
}