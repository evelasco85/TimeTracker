﻿namespace MainApp.DailyHours
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
            this.txtHoursRendered = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHoursUnrecorded = new System.Windows.Forms.TextBox();
            this.dGridLogs = new System.Windows.Forms.DataGridView();
            this.btnManuaTrackerEntry = new System.Windows.Forms.Button();
            this.txtHoursRecorded = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDecrementDayByOne = new System.Windows.Forms.Button();
            this.dgdCategorySummary = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgdCategorySummary)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIncementDayByOne
            // 
            this.btnIncementDayByOne.Location = new System.Drawing.Point(328, 12);
            this.btnIncementDayByOne.Name = "btnIncementDayByOne";
            this.btnIncementDayByOne.Size = new System.Drawing.Size(39, 23);
            this.btnIncementDayByOne.TabIndex = 12;
            this.btnIncementDayByOne.Text = ">>";
            this.btnIncementDayByOne.UseVisualStyleBackColor = true;
            this.btnIncementDayByOne.Click += new System.EventHandler(this.btnIncementDayByOne_Click);
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
            this.dateTimeManualEntry.ValueChanged += new System.EventHandler(this.dateTimeManualEntry_ValueChanged);
            // 
            // txtHoursRendered
            // 
            this.txtHoursRendered.Location = new System.Drawing.Point(177, 39);
            this.txtHoursRendered.Name = "txtHoursRendered";
            this.txtHoursRendered.Size = new System.Drawing.Size(100, 20);
            this.txtHoursRendered.TabIndex = 38;
            this.txtHoursRendered.Text = "0";
            this.txtHoursRendered.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHoursRendered_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Hour(s) Rendered:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Hour(s) Unrecorded:";
            // 
            // txtHoursUnrecorded
            // 
            this.txtHoursUnrecorded.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtHoursUnrecorded.ForeColor = System.Drawing.Color.Red;
            this.txtHoursUnrecorded.Location = new System.Drawing.Point(177, 91);
            this.txtHoursUnrecorded.Name = "txtHoursUnrecorded";
            this.txtHoursUnrecorded.ReadOnly = true;
            this.txtHoursUnrecorded.Size = new System.Drawing.Size(100, 20);
            this.txtHoursUnrecorded.TabIndex = 40;
            this.txtHoursUnrecorded.Text = "0";
            this.txtHoursUnrecorded.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dGridLogs
            // 
            this.dGridLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGridLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridLogs.Location = new System.Drawing.Point(373, 168);
            this.dGridLogs.Name = "dGridLogs";
            this.dGridLogs.RowTemplate.ReadOnly = true;
            this.dGridLogs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridLogs.Size = new System.Drawing.Size(1047, 469);
            this.dGridLogs.TabIndex = 41;
            this.dGridLogs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridLogs_CellDoubleClick);
            this.dGridLogs.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dGridLogs_DataBindingComplete);
            this.dGridLogs.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dGridLogs_RowPrePaint);
            // 
            // btnManuaTrackerEntry
            // 
            this.btnManuaTrackerEntry.Location = new System.Drawing.Point(31, 132);
            this.btnManuaTrackerEntry.Name = "btnManuaTrackerEntry";
            this.btnManuaTrackerEntry.Size = new System.Drawing.Size(151, 30);
            this.btnManuaTrackerEntry.TabIndex = 42;
            this.btnManuaTrackerEntry.Text = "Manual Entry";
            this.btnManuaTrackerEntry.UseVisualStyleBackColor = true;
            this.btnManuaTrackerEntry.Click += new System.EventHandler(this.btnManuaTrackerEntry_Click);
            // 
            // txtHoursRecorded
            // 
            this.txtHoursRecorded.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtHoursRecorded.ForeColor = System.Drawing.Color.Blue;
            this.txtHoursRecorded.Location = new System.Drawing.Point(177, 65);
            this.txtHoursRecorded.Name = "txtHoursRecorded";
            this.txtHoursRecorded.ReadOnly = true;
            this.txtHoursRecorded.Size = new System.Drawing.Size(100, 20);
            this.txtHoursRecorded.TabIndex = 44;
            this.txtHoursRecorded.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Hour(s) Recorded:";
            // 
            // btnDecrementDayByOne
            // 
            this.btnDecrementDayByOne.Location = new System.Drawing.Point(283, 12);
            this.btnDecrementDayByOne.Name = "btnDecrementDayByOne";
            this.btnDecrementDayByOne.Size = new System.Drawing.Size(39, 23);
            this.btnDecrementDayByOne.TabIndex = 45;
            this.btnDecrementDayByOne.Text = "<<";
            this.btnDecrementDayByOne.UseVisualStyleBackColor = true;
            this.btnDecrementDayByOne.Click += new System.EventHandler(this.btnDecrementDayByOne_Click);
            // 
            // dgdCategorySummary
            // 
            this.dgdCategorySummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgdCategorySummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdCategorySummary.Location = new System.Drawing.Point(31, 168);
            this.dgdCategorySummary.Name = "dgdCategorySummary";
            this.dgdCategorySummary.RowTemplate.ReadOnly = true;
            this.dgdCategorySummary.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgdCategorySummary.Size = new System.Drawing.Size(336, 469);
            this.dgdCategorySummary.TabIndex = 46;
            this.dgdCategorySummary.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgdCategorySummary_DataBindingComplete);
            // 
            // frmDailyHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 649);
            this.Controls.Add(this.dgdCategorySummary);
            this.Controls.Add(this.btnDecrementDayByOne);
            this.Controls.Add(this.txtHoursRecorded);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnManuaTrackerEntry);
            this.Controls.Add(this.dGridLogs);
            this.Controls.Add(this.txtHoursUnrecorded);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHoursRendered);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnIncementDayByOne);
            this.Controls.Add(this.lblDateTimeEntry);
            this.Controls.Add(this.dateTimeManualEntry);
            this.Name = "frmDailyHours";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Daily Hours";
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgdCategorySummary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIncementDayByOne;
        private System.Windows.Forms.Label lblDateTimeEntry;
        private System.Windows.Forms.DateTimePicker dateTimeManualEntry;
        private System.Windows.Forms.TextBox txtHoursRendered;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHoursUnrecorded;
        private System.Windows.Forms.DataGridView dGridLogs;
        private System.Windows.Forms.Button btnManuaTrackerEntry;
        private System.Windows.Forms.TextBox txtHoursRecorded;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDecrementDayByOne;
        private System.Windows.Forms.DataGridView dgdCategorySummary;
    }
}