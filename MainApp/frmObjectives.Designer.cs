namespace MainApp
{
    partial class frmObjectives
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.objectiveDate = new System.Windows.Forms.DateTimePicker();
            this.txtObjectives = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 443);
            // 
            // btnCancel
            // 
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Objective Description:";
            // 
            // objectiveDate
            // 
            this.objectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.objectiveDate.Location = new System.Drawing.Point(49, 45);
            this.objectiveDate.Name = "objectiveDate";
            this.objectiveDate.Size = new System.Drawing.Size(119, 20);
            this.objectiveDate.TabIndex = 5;
            // 
            // txtObjectives
            // 
            this.txtObjectives.Location = new System.Drawing.Point(15, 108);
            this.txtObjectives.Multiline = true;
            this.txtObjectives.Name = "txtObjectives";
            this.txtObjectives.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtObjectives.Size = new System.Drawing.Size(234, 329);
            this.txtObjectives.TabIndex = 6;
            this.txtObjectives.WordWrap = false;
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
            // frmObjectives
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 647);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtObjectives);
            this.Controls.Add(this.objectiveDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmObjectives";
            this.Text = "Objectives";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.objectiveDate, 0);
            this.Controls.SetChildIndex(this.txtObjectives, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblId, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker objectiveDate;
        private System.Windows.Forms.TextBox txtObjectives;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblId;
    }
}