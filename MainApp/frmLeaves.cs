using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Domain.Infrastructure;
using Domain.MVP;
using Domain.Helpers;

namespace MainApp
{
    public partial class frmLeaves : Form, IView<Leave>
    {
        enum ModifierState
        {
            Add,
            Edit,
            Delete,
            Save,
            Cancel
        };

        IDateHelper _helper;
        public Action<Func<Leave, bool>> QueryViewRecords { get; set; }
        public Action OnQueryViewRecordsCompletion { get; set; }
        public Action<Leave> SaveViewRecord { get; set; }
        public Action<Func<Leave, bool>> DeleteViewRecords { get; set; }
        public IEnumerable<Leave> ViewQueryResult { get; set; }

        public frmLeaves(IEFRepository repository)
        {
            Action RegisterController = () => new LeaveController(repository, this);

            RegisterController();

            this._helper = DateHelper.GetInstance();
            this.OnQueryViewRecordsCompletion = RefreshGridData;

            InitializeComponent();

            this.leaveDate.Value = DateTime.Now;

            this.QueryViewRecords(null);
            this.WindowInputChanges(ModifierState.Cancel);
        }

        void RefreshGridData()
        {
            IEnumerable<Leave> leaves = this.ViewQueryResult;
            var displayColumns = this._helper.GetLeaves(leaves);
            this.dGridLeaves.DataSource = displayColumns;

            this.dGridLeaves.Refresh();

            DateTime lastUpdatedDate = displayColumns
                .Select(x => x.SystemUpdated)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this.HighlightRecordByDate(lastUpdatedDate);
        }

        void HighlightRecordByDate(DateTime recordDate)
        {
            for (int index = 0; index < this.dGridLeaves.Rows.Count; index++)
            {
                try
                {
                    DateTime systemDate = DateTime.Parse(this.dGridLeaves.Rows[index].Cells[LeaveController.SYSTEM_UPDATED_INDEX].Value.ToString());
                    bool identicalTime = recordDate.ToLongTimeString() == systemDate.ToLongTimeString();

                    if ((recordDate.Date == systemDate.Date) && identicalTime)
                    {
                        this.dGridLeaves.CurrentCell = this.dGridLeaves[LeaveController.ID_INDEX, index];
                        this.dGridLeaves.Rows[index].Selected = true;
                        this.dGridLeaves.Rows[index].Cells[LeaveController.ID_INDEX].Selected = true;
                        this.dGridLeaves.FirstDisplayedScrollingRowIndex = index;

                        this.dGridLeaves.Update();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void dGridLeaves_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpdateWindow(e.RowIndex);
        }

        void UpdateWindow(int rowIndex)
        {
            try
            {
                int id = int.Parse(this.dGridLeaves.Rows[rowIndex].Cells[LeaveController.ID_INDEX].Value.ToString());

                this.QueryViewRecords(null);

                Leave leave = this._helper.GetLeave(this.ViewQueryResult, id);

                this.lblId.Text = leave.Id.ToString();
                this.leaveDate.Value = leave.Date;
                this.txtLeaveDescription.Text = leave.Description;
            }
            catch (ArgumentOutOfRangeException) { /*Skip*/}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void EnableInputWindow(bool enable)
        {
            this.leaveDate.Enabled = enable;
            this.txtLeaveDescription.Enabled = enable;
        }

        void ResetInputWindow()
        {
            this.lblId.Text = string.Empty;
            this.leaveDate.Value = DateTime.Now;
            this.txtLeaveDescription.Clear();
        }

        void WindowInputChanges(ModifierState modifierState)
        {
            switch (modifierState)
            {
                case ModifierState.Add:
                case ModifierState.Edit:
                    this.EnableInputWindow(true);
                    this.EnablePersistButtons(true);
                    this.EnableModifierButtons(false);
                    this.EnableDataGridNavigation(false);
                    break;
                case ModifierState.Delete:
                    break;
                case ModifierState.Save:
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
            this.dGridLeaves.Enabled = enable;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Add);
            this.ResetInputWindow();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(this.lblId.Text);

                if (id == 0)
                    throw new FormatException();

                this.WindowInputChanges(ModifierState.Edit);
            }
            catch (FormatException)
            {
                MessageBox.Show("A record selection is required for editing");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(this.lblId.Text);

                if (id == 0)
                    throw new FormatException();

                DialogResult result = MessageBox.Show("Delete record?", "Delete Record Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.DeleteViewRecords(x => x.Id == id);
                    this.QueryViewRecords(null);
                    this.ResetInputWindow();

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("A record selection is required for deletion");
            }

            this.WindowInputChanges(ModifierState.Delete);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Leave leave = new Leave
            {
                SystemCreated = DateTime.Now,
            };

            if (!string.IsNullOrEmpty(this.lblId.Text))
            {
                this.QueryViewRecords(null);

                leave = this._helper.GetLeave(this.ViewQueryResult, int.Parse(this.lblId.Text));
            }

            leave.Date = this.leaveDate.Value;
            leave.Description = this.txtLeaveDescription.Text;
            leave.SystemUpdated = DateTime.Now;

            this.SaveViewRecord(leave);
            this.WindowInputChanges(ModifierState.Save);
            this.QueryViewRecords(null);
            this.ResetInputWindow();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Cancel);
        }
    }
}
       
