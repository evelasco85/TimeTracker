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
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogHours)).BeginInit();
            this.SuspendLayout();
            // 
            // dGridLogs
            // 
            this.dGridLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridLogs.Location = new System.Drawing.Point(386, 110);
            this.dGridLogs.Name = "dGridLogs";
            this.dGridLogs.RowTemplate.ReadOnly = true;
            this.dGridLogs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridLogs.Size = new System.Drawing.Size(642, 384);
            this.dGridLogs.TabIndex = 1;
            this.dGridLogs.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dGridLogs_DataBindingComplete);
            this.dGridLogs.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dGridLogs_RowPrePaint);
            // 
            // dGridLogHours
            // 
            this.dGridLogHours.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridLogHours.Location = new System.Drawing.Point(12, 110);
            this.dGridLogHours.Name = "dGridLogHours";
            this.dGridLogHours.RowTemplate.ReadOnly = true;
            this.dGridLogHours.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridLogHours.Size = new System.Drawing.Size(332, 384);
            this.dGridLogHours.TabIndex = 2;
            // 
            // frmSummarizeLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 554);
            this.Controls.Add(this.dGridLogHours);
            this.Controls.Add(this.dGridLogs);
            this.Name = "frmSummarizeLogs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Summarize Logs";
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogHours)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGridLogs;
        private System.Windows.Forms.DataGridView dGridLogHours;
    }
}