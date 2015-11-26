using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Domain.Infrastructure;
using Domain.Helpers;
using Domain.Controllers;
using Domain.Views;

namespace MainApp
{
    public partial class frmLeaves : frmCommonDataEditor, ILeaveView, IFormCommonOperation
    {
        public Action<Func<Leave, bool>> QueryViewRecords { get; set; }
        public Action OnQueryViewRecordsCompletion { get; set; }
        public Action<Leave> SaveViewRecord { get; set; }
        public Action<Func<Leave, bool>> DeleteViewRecords { get; set; }
        public IEnumerable<Leave> ViewQueryResult { get; set; }
        public Action<IEnumerable<Leave>> GetLeaveData { get; set; }
        public Action<dynamic, DateTime> OnGetLeaveDataCompletion { get; set; }

        public frmLeaves(IEFRepository repository)
        {
            Action RegisterController = () => new LeaveController(repository, this);

            RegisterController();
            this.RegisterCommonOperation(this);

            this.OnQueryViewRecordsCompletion = RefreshGridData;
            this.OnGetLeaveDataCompletion = UpdateLeaveData;

            InitializeComponent();

            this.leaveDate.Value = DateTime.Now;

            this.QueryViewRecords(null);
        }

        void RefreshGridData()
        {
            IEnumerable<Leave> leaves = this.ViewQueryResult;

            this.GetLeaveData(leaves);
        }

        void UpdateLeaveData(dynamic displayColumns, DateTime lastUpdatedDate)
        {
            this.dGrid.DataSource = displayColumns;

            this.dGrid.Refresh();
            this.HighlightRecordByDate(lastUpdatedDate);
        }

        void HighlightRecordByDate(DateTime recordDate)
        {
            for (int index = 0; index < this.dGrid.Rows.Count; index++)
            {
                try
                {
                    DateTime systemDate = DateTime.Parse(this.dGrid.Rows[index].Cells[LeaveController.SYSTEM_UPDATED_INDEX].Value.ToString());
                    bool identicalTime = recordDate.ToLongTimeString() == systemDate.ToLongTimeString();

                    if ((recordDate.Date == systemDate.Date) && identicalTime)
                    {
                        this.dGrid.CurrentCell = this.dGrid[LeaveController.ID_INDEX, index];
                        this.dGrid.Rows[index].Selected = true;
                        this.dGrid.Rows[index].Cells[LeaveController.ID_INDEX].Selected = true;
                        this.dGrid.FirstDisplayedScrollingRowIndex = index;
                        
                        break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            this.dGrid.Update();
        }

        public void UpdateWindow(int rowIndex)
        {
            try
            {
                int id = int.Parse(this.dGrid.Rows[rowIndex].Cells[LeaveController.ID_INDEX].Value.ToString());
                Leave leave = DateHelper.GetInstance().GetLeave(this.ViewQueryResult, id);

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

        public void EnableInputWindow(bool enable)
        {
            this.leaveDate.Enabled = enable;
            this.txtLeaveDescription.Enabled = enable;
        }

        public void ResetInputWindow()
        {
            this.lblId.Text = string.Empty;
            this.leaveDate.Value = DateTime.Now;
            this.txtLeaveDescription.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Add);
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
                leave = DateHelper.GetInstance().GetLeave(this.ViewQueryResult, int.Parse(this.lblId.Text));

            leave.Date = this.leaveDate.Value;
            leave.Description = this.txtLeaveDescription.Text;
            leave.SystemUpdated = DateTime.Now;

            this.SaveViewRecord(leave);
            this.QueryViewRecords(null);
            this.WindowInputChanges(ModifierState.Save);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Cancel);
        }
    }
}
       
