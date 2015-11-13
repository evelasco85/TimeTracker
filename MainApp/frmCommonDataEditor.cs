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
    public abstract partial class frmCommonDataEditor : Form 
    {
        enum ModifierState
        {
            Add,
            Edit,
            Delete,
            Save,
            Cancel
        };

        public frmCommonDataEditor(IEFRepository repository)
        {
            InitializeComponent();

            this.WindowInputChanges(ModifierState.Cancel);
        }

        public virtual void dGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpdateWindow(e.RowIndex);
        }

        public abstract void UpdateWindow(int rowIndex);

        public abstract void EnableInputWindow(bool enable);

        public abstract void ResetInputWindow();

        void WindowInputChanges(ModifierState modifierState)
        {
            switch (modifierState)
            {
                case ModifierState.Add:
                    this.ResetInputWindow();
                    this.EnableInputWindow(true);
                    this.EnablePersistButtons(true);
                    this.EnableModifierButtons(false);
                    this.EnableDataGridNavigation(false);
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
                    this.ResetInputWindow();
                    this.EnableInputWindow(false);
                    this.EnablePersistButtons(false);
                    this.EnableModifierButtons(true);
                    this.EnableDataGridNavigation(true);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Add);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Edit);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Delete);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Save);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Cancel);
        }
    }
}
       
