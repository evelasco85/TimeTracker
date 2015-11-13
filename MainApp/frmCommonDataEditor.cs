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
        enum ModifierState
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
            throw new NotImplementedException("Derived forms must implement this function");
        }

        public virtual void EnableInputWindow(bool enable)
        {
            throw new NotImplementedException("Derived forms must implement this function");
        }

        public virtual void ResetInputWindow() { }

        void WindowInputChanges(ModifierState modifierState)
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

        public void btnAdd_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Add);
        }

        public void btnEdit_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Edit);
        }

        public void btnDelete_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Delete);
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Save);
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Cancel);
        }

        private void frmCommonDataEditor_Load(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Cancel);
        }
    }
}
       
