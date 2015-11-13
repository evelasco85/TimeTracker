namespace MainApp
{
    partial class frmHolidays
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.holidayDate = new System.Windows.Forms.DateTimePicker();
            this.txtHolidayDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ClientSizeChanged += new System.EventHandler(this.btnEdit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Holiday Description:";
            // 
            // holidayDate
            // 
            this.holidayDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.holidayDate.Location = new System.Drawing.Point(49, 45);
            this.holidayDate.Name = "holidayDate";
            this.holidayDate.Size = new System.Drawing.Size(119, 20);
            this.holidayDate.TabIndex = 5;
            // 
            // txtHolidayDescription
            // 
            this.txtHolidayDescription.Location = new System.Drawing.Point(49, 108);
            this.txtHolidayDescription.Name = "txtHolidayDescription";
            this.txtHolidayDescription.Size = new System.Drawing.Size(200, 20);
            this.txtHolidayDescription.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "ID:";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblId.Location = new System.Drawing.Point(49, 18);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(15, 15);
            this.lblId.TabIndex = 13;
            this.lblId.Text = "0";
            // 
            // frmHolidays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 474);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHolidayDescription);
            this.Controls.Add(this.holidayDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmHolidays";
            this.Text = "Holidays";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.holidayDate, 0);
            this.Controls.SetChildIndex(this.txtHolidayDescription, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblId, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker holidayDate;
        private System.Windows.Forms.TextBox txtHolidayDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblId;
    }
}