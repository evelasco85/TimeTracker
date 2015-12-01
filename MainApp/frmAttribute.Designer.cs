namespace MainApp
{
    partial class frmAttribute
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
            this.txtAttributeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(15, 352);
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
            // txtAttributeName
            // 
            this.txtAttributeName.Location = new System.Drawing.Point(49, 68);
            this.txtAttributeName.Name = "txtAttributeName";
            this.txtAttributeName.Size = new System.Drawing.Size(200, 20);
            this.txtAttributeName.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Attribute Name:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(49, 130);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 94);
            this.txtDescription.TabIndex = 19;
            this.txtDescription.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Description:";
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(49, 272);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(200, 20);
            this.txtLink.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Link:";
            // 
            // frmAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 652);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAttributeName);
            this.Controls.Add(this.label2);
            this.Name = "frmAttribute";
            this.Text = "Attributes";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtAttributeName, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblId, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtDescription, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtLink, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAttributeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Label label4;
    }
}