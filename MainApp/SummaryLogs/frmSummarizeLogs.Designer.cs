namespace MainApp
{
    partial class frmSummarizeLogs
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
            this.dGridLogs = new System.Windows.Forms.DataGridView();
            this.dGridLogHours = new System.Windows.Forms.DataGridView();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.lblTotalHours = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogHours)).BeginInit();
            this.SuspendLayout();
            // 
            // dGridLogs
            // 
            this.dGridLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGridLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridLogs.Location = new System.Drawing.Point(365, 110);
            this.dGridLogs.Name = "dGridLogs";
            this.dGridLogs.RowTemplate.ReadOnly = true;
            this.dGridLogs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridLogs.Size = new System.Drawing.Size(675, 432);
            this.dGridLogs.TabIndex = 1;
            this.dGridLogs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridLogs_CellDoubleClick);
            this.dGridLogs.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dGridLogs_DataBindingComplete);
            this.dGridLogs.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dGridLogs_RowPrePaint);
            // 
            // dGridLogHours
            // 
            this.dGridLogHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dGridLogHours.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridLogHours.Location = new System.Drawing.Point(12, 110);
            this.dGridLogHours.Name = "dGridLogHours";
            this.dGridLogHours.RowTemplate.ReadOnly = true;
            this.dGridLogHours.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridLogHours.Size = new System.Drawing.Size(332, 432);
            this.dGridLogHours.TabIndex = 2;
            this.dGridLogHours.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridLogHours_CellClick);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(12, 19);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(58, 13);
            this.lblStartDate.TabIndex = 12;
            this.lblStartDate.Text = "Start Date:";
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeStart.Location = new System.Drawing.Point(76, 15);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(100, 20);
            this.dateTimeStart.TabIndex = 13;
            this.dateTimeStart.ValueChanged += new System.EventHandler(this.dateTimeStart_ValueChanged);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(15, 55);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(55, 13);
            this.lblEndDate.TabIndex = 14;
            this.lblEndDate.Text = "End Date:";
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeEnd.Location = new System.Drawing.Point(76, 51);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(100, 20);
            this.dateTimeEnd.TabIndex = 15;
            this.dateTimeEnd.ValueChanged += new System.EventHandler(this.dateTimeEnd_ValueChanged);
            // 
            // lblTotalHours
            // 
            this.lblTotalHours.AutoSize = true;
            this.lblTotalHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHours.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalHours.Location = new System.Drawing.Point(15, 94);
            this.lblTotalHours.Name = "lblTotalHours";
            this.lblTotalHours.Size = new System.Drawing.Size(88, 13);
            this.lblTotalHours.TabIndex = 16;
            this.lblTotalHours.Text = "Total Hours: 0";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(191, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72, 56);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmSummarizeLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 554);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblTotalHours);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dateTimeEnd);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dateTimeStart);
            this.Controls.Add(this.dGridLogHours);
            this.Controls.Add(this.dGridLogs);
            this.Name = "frmSummarizeLogs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Summarize Logs";
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogHours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGridLogs;
        private System.Windows.Forms.DataGridView dGridLogHours;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Label lblTotalHours;
        private System.Windows.Forms.Button btnRefresh;
    }
}