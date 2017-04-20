using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Domain.Helpers;
using Domain.Controllers;
using Domain.Views;

namespace MainApp
{
    public partial class frmLeaves : frmCommonDataEditor, ILeaveView, IFormCommonOperation
    {
        public ILeaveRequests ViewRequest { get; set; }
        public IEnumerable<Leave> QueryResults { get; set; }

        Form _parentForm;

        public frmLeaves()
        {
            InitializeComponent();
            this.RegisterCommonOperation(this);
        }

        public void OnShow()
        {
            MethodInvoker invokeFromUI = new MethodInvoker(
               () =>
               {
                   try
                   {
                       this.ShowDialog(this._parentForm);
                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }
               }
           );

            if (this.InvokeRequired)
                this.Invoke(invokeFromUI);
            else
                invokeFromUI.Invoke();
        }

        public void OnViewReady(object data)
        {
            this._parentForm = (Form)data.GetType().GetProperty("parentForm").GetValue(data, null);
            this.leaveDate.Value = DateTime.Now;

            this.ViewRequest.GetData(null);
        }

        public void OnQueryRecordsCompletion()
        {
            IEnumerable<Leave> leaves = this.QueryResults;

            this.ViewRequest.GetLeaveData(leaves);
        }

        public void OnGetLeaveDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate)
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
                Leave leave = DateHelper.GetInstance().GetLeave(this.QueryResults, id);

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
            this.txtLeaveDescription.ReadOnly = !enable;
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
                    this.ViewRequest.DeleteData(x => x.Id == id);
                    this.ViewRequest.GetData(null);
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
                leave = DateHelper.GetInstance().GetLeave(this.QueryResults, int.Parse(this.lblId.Text));

            leave.Date = this.leaveDate.Value;
            leave.Description = this.txtLeaveDescription.Text;
            leave.SystemUpdated = DateTime.Now;

            this.ViewRequest.SaveData(leave);
            this.ViewRequest.GetData(null);
            this.WindowInputChanges(ModifierState.Save);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Cancel);
        }

        public void DecorateGrid()
        {
            IDataGridHelper helper = DataGridHelper.GetInstance();

            helper.SetAutoResizeCells(ref this.dGrid);
        }
    }
}
       
