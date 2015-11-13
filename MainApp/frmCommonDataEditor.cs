using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Domain.Infrastructure;
using Domain.Helpers;
using Domain.Controller;

namespace MainApp
{
    public partial class frmCommonDataEditor : Form 
    {
        public enum ModifierState
        {
            Add,
            Edit,
            Delete,
            Save,
            Cancel
        };

        public frmCommonDataEditor()
        {
            InitializeComponent();
        }

        public virtual void dGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpdateWindow(e.RowIndex);
        }

        public virtual void UpdateWindow(int rowIndex)
        {
        }

        public virtual void EnableInputWindow(bool enable)
        {
        }

        public virtual void ResetInputWindow() { }

        public void WindowInputChanges(ModifierState modifierState)
        {
            switch (modifierState)
            {
                case ModifierState.Add:
                    this.EnableInputWindow(true);
                    this.EnablePersistButtons(true);
                    this.EnableModifierButtons(false);
                    this.EnableDataGridNavigation(false);
                    this.ResetInputWindow();
                    break;
                case ModifierState.Edit:
                    this.EnableInputWindow(true);
                    this.EnablePersistButtons(true);
                    this.EnableModifierButtons(false);
                    this.EnableDataGridNavigation(false);
                    break;
                case ModifierState.Delete:
                    this.ResetInputWindow();
                    break;
                case ModifierState.Save:
                    this.EnableInputWindow(false);
                    this.EnablePersistButtons(false);
                    this.EnableModifierButtons(true);
                    this.EnableDataGridNavigation(true);
                    this.ResetInputWindow();
                    break;
                case ModifierState.Cancel:
                    this.EnableInputWindow(false);
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
            this.dGrid.Enabled = enable;
        }

        private void frmCommonDataEditor_Load(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Cancel);
        }
    }
}
       
