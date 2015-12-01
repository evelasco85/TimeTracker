namespace MainApp
{
    partial class frmCategory
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
            this.lblId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkShowInSummary = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblId.Location = new System.Drawing.Point(49, 12);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(15, 15);
            this.lblId.TabIndex = 17;
            this.lblId.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "ID:";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(49, 68);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(200, 20);
            this.txtCategoryName.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Category Name:";
            // 
            // chkShowInSummary
            // 
            this.chkShowInSummary.AutoSize = true;
            this.chkShowInSummary.Location = new System.Drawing.Point(49, 103);
            this.chkShowInSummary.Name = "chkShowInSummary";
            this.chkShowInSummary.Size = new System.Drawing.Size(110, 17);
            this.chkShowInSummary.TabIndex = 19;
            this.chkShowInSummary.Text = "Show in Summary";
            this.chkShowInSummary.UseVisualStyleBackColor = true;
            // 
            // frmCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 651);
            this.Controls.Add(this.chkShowInSummary);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.label2);
            this.Name = "frmCategory";
            this.Text = "Categories";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtCategoryName, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblId, 0);
            this.Controls.SetChildIndex(this.chkShowInSummary, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkShowInSummary;
    }
}