namespace MainApp
{
    partial class frmSummarizeHoursByCategories
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
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // dGridLogs
            // 
            this.dGridLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGridLogs.Location = new System.Drawing.Point(0, 0);
            this.dGridLogs.Name = "dGridLogs";
            this.dGridLogs.RowTemplate.ReadOnly = true;
            this.dGridLogs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridLogs.Size = new System.Drawing.Size(1073, 554);
            this.dGridLogs.TabIndex = 1;
            this.dGridLogs.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dGridLogs_DataBindingComplete);
            // 
            // frmSummarizeHoursByCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 554);
            this.Controls.Add(this.dGridLogs);
            this.Name = "frmSummarizeHoursByCategories";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Summarize Hours By Categories";
            ((System.ComponentModel.ISupportInitialize)(this.dGridLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGridLogs;
    }
}