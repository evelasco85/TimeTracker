using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Domain.Infrastructure;
using Domain.Helpers;
using Domain.Controllers;

namespace MainApp
{
    public partial class frmCommonByDateDataEditor : Form 
    {
        IFormCommonOperation _formCommonOperation;

        public enum ModifierState
        {
            Add,
            Edit,
            Delete,
            Save,
            Cancel
        };

        public frmCommonByDateDataEditor()
        {
            InitializeComponent();

            this.SetDisplay();
        }

        void SetDisplay()
        {
            this.periodPicker.Format = DateTimePickerFormat.Custom;
            this.periodPicker.CustomFormat = "MM/yyyy";
        }

        public virtual void UpdateSummarizedDailyActivityHours(decimal totalHours)
        {
            this.lblSummedDailyActivityHours.Text = string.Format(
                "Summarized Daily Activity Hours: {0}",
                totalHours.ToString()
                );
        }

        public virtual void RegisterCommonOperation(IFormCommonOperation formCommonOperation)
        {
            this._formCommonOperation = formCommonOperation;
        }

        public virtual void recordGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this._formCommonOperation.UpdateWindow(e.RowIndex);
        }

        public void WindowInputChanges(ModifierState modifierState)
        {
            if (this._formCommonOperation == null)
            {
                MessageBox.Show("Must implement/inherit IFormCommonOperation, call in constructor: 'this.RegisterCommonOperation(this)'",
                    "Implementation Required", MessageBoxButtons.OK, MessageBoxIcon.Information
                    );

                return;
            }

            switch (modifierState)
            {
                case ModifierState.Add:
                    this._formCommonOperation.EnableInputWindow(true);
                    this.EnablePersistButtons(true);
                    this.EnableModifierButtons(false);
                    this.EnableDataGridNavigation(false);
                    this._formCommonOperation.ResetInputWindow();
                    break;
                case ModifierState.Edit:
                    this._formCommonOperation.EnableInputWindow(true);
                    this.EnablePersistButtons(true);
                    this.EnableModifierButtons(false);
                    this.EnableDataGridNavigation(false);
                    break;
                case ModifierState.Delete:
                    this._formCommonOperation.ResetInputWindow();
                    break;
                case ModifierState.Save:
                    this._formCommonOperation.EnableInputWindow(false);
                    this.EnablePersistButtons(false);
                    this.EnableModifierButtons(true);
                    this.EnableDataGridNavigation(true);
                    this._formCommonOperation.ResetInputWindow();
                    break;
                case ModifierState.Cancel:
                    this._formCommonOperation.EnableInputWindow(false);
                    this.EnablePersistButtons(false);
                    this.EnableModifierButtons(true);
                    this.EnableDataGridNavigation(true);
                    break;
            }
        }

        void EnablePersistButtons(bool enable)
        {
            this.btnSave.Enabled = enable;
            this.btnCancel.Enabled = enable;
        }

        void EnableModifierButtons(bool enable)
        {
            this.btnAdd.Enabled = enable;
            this.btnEdit.Enabled = enable;
            this.btnDelete.Enabled = enable;
        }

        void EnableDataGridNavigation(bool enable)
        {
            this.recordGrid.Enabled = enable;
        }

        private void frmCommonDataEditor_Load(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Cancel);
        }
    }
}
       
