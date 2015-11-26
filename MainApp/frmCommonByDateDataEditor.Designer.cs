namespace MainApp
{
    public partial class frmCommonByDateDataEditor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.periodPicker = new System.Windows.Forms.DateTimePicker();
            this.pnlRecordGrid = new System.Windows.Forms.Panel();
            this.recordGrid = new System.Windows.Forms.DataGridView();
            this.lstUniqueDates = new System.Windows.Forms.ListBox();
            this.lblSummedDailyActivityHours = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlRecordGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recordGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Location = new System.Drawing.Point(489, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 67);
            this.panel1.TabIndex = 12;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(129, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(37, 41);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(162, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(81, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(0, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Period:";
            // 
            // periodPicker
            // 
            this.periodPicker.Location = new System.Drawing.Point(12, 47);
            this.periodPicker.Name = "periodPicker";
            this.periodPicker.ShowUpDown = true;
            this.periodPicker.Size = new System.Drawing.Size(154, 20);
            this.periodPicker.TabIndex = 15;
            // 
            // pnlRecordGrid
            // 
            this.pnlRecordGrid.Controls.Add(this.recordGrid);
            this.pnlRecordGrid.Location = new System.Drawing.Point(184, 286);
            this.pnlRecordGrid.Name = "pnlRecordGrid";
            this.pnlRecordGrid.Size = new System.Drawing.Size(542, 208);
            this.pnlRecordGrid.TabIndex = 16;
            // 
            // recordGrid
            // 
            this.recordGrid.AllowUserToAddRows = false;
            this.recordGrid.AllowUserToDeleteRows = false;
            this.recordGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.recordGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recordGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recordGrid.Location = new System.Drawing.Point(0, 0);
            this.recordGrid.MultiSelect = false;
            this.recordGrid.Name = "recordGrid";
            this.recordGrid.ReadOnly = true;
            this.recordGrid.RowTemplate.ReadOnly = true;
            this.recordGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.recordGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.recordGrid.Size = new System.Drawing.Size(542, 208);
            this.recordGrid.TabIndex = 6;
            this.recordGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.recordGrid_CellClick);
            // 
            // lstUniqueDates
            // 
            this.lstUniqueDates.FormattingEnabled = true;
            this.lstUniqueDates.Location = new System.Drawing.Point(12, 73);
            this.lstUniqueDates.Name = "lstUniqueDates";
            this.lstUniqueDates.Size = new System.Drawing.Size(154, 420);
            this.lstUniqueDates.TabIndex = 17;
            // 
            // lblSummedDailyActivityHours
            // 
            this.lblSummedDailyActivityHours.AutoSize = true;
            this.lblSummedDailyActivityHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummedDailyActivityHours.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblSummedDailyActivityHours.Location = new System.Drawing.Point(181, 270);
            this.lblSummedDailyActivityHours.Name = "lblSummedDailyActivityHours";
            this.lblSummedDailyActivityHours.Size = new System.Drawing.Size(193, 13);
            this.lblSummedDailyActivityHours.TabIndex = 18;
            this.lblSummedDailyActivityHours.Text = "Summarized Daily Activity Hours:";
            this.lblSummedDailyActivityHours.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmCommonByDateDataEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 506);
            this.Controls.Add(this.lblSummedDailyActivityHours);
            this.Controls.Add(this.lstUniqueDates);
            this.Controls.Add(this.pnlRecordGrid);
            this.Controls.Add(this.periodPicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "frmCommonByDateDataEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmCommonDataEditor_Load);
            this.panel1.ResumeLayout(false);
            this.pnlRecordGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.recordGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnEdit;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker periodPicker;
        public System.Windows.Forms.DataGridView recordGrid;
        public System.Windows.Forms.Panel pnlRecordGrid;
        public System.Windows.Forms.ListBox lstUniqueDates;
        public System.Windows.Forms.Label lblSummedDailyActivityHours;


    }
}