using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Domain.Controllers;
using Domain.Views;

namespace MainApp
{
    public partial class frmObjectives : frmCommonDataEditor, IObjectiveView, IFormCommonOperation
    {
        public IObjectiveRequests ViewRequest { get; set; }
        public IObjectiveEvents ViewEvents { get; set; }

        public Action<Func<Objective, bool>> View_QueryRecords { get; set; }
        public Action View_OnQueryRecordsCompletion { get; set; }
        public Action<Objective> View_SaveRecord { get; set; }
        public Action<Func<Objective, bool>> View_DeleteRecords { get; set; }
        public IEnumerable<Objective> View_QueryResults { get; set; }
        public Action<object> View_ViewReady { get; set; }
        public Action<object> View_OnViewReady { get; set; }
        public Action View_OnShow { get; set; }
        Form _parentForm;

        public frmObjectives()
        {
            this.RegisterCommonOperation(this);

            this.ViewEvents = this;

            this.View_OnQueryRecordsCompletion = RefreshGridData;
            this.View_OnViewReady = OnViewReady;
            this.View_OnShow = OnShow;

            InitializeComponent();
        }

        void OnShow()
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

        void OnViewReady(object data)
        {
            this._parentForm = (Form)data.GetType().GetProperty("parentForm").GetValue(data, null);
            this.objectiveDate.Value = DateTime.Now;

            this.View_QueryRecords(null);
        }

        void RefreshGridData()
        {
            IEnumerable<Objective> objectives = this.View_QueryResults;

            this.ViewRequest.GetObjectiveData(objectives);
        }

        public void OnGetObjectiveDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate)
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
                    DateTime systemDate = DateTime.Parse(this.dGrid.Rows[index].Cells[ObjectiveController.SYSTEM_UPDATED_INDEX].Value.ToString());
                    bool identicalTime = recordDate.ToLongTimeString() == systemDate.ToLongTimeString();

                    if ((recordDate.Date == systemDate.Date) && identicalTime)
                    {
                        this.dGrid.CurrentCell = this.dGrid[ObjectiveController.ID_INDEX, index];
                        this.dGrid.Rows[index].Selected = true;
                        this.dGrid.Rows[index].Cells[ObjectiveController.ID_INDEX].Selected = true;
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
                int id = int.Parse(this.dGrid.Rows[rowIndex].Cells[ObjectiveController.ID_INDEX].Value.ToString());
                Objective objective = this.View_QueryResults
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                this.lblId.Text = objective.Id.ToString();
                this.objectiveDate.Value = objective.Date;
                this.txtObjectives.Text = objective.Description;
            }
            catch (ArgumentOutOfRangeException) { /*Skip*/}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnableInputWindow(bool enable)
        {
            this.objectiveDate.Enabled = enable;
            this.txtObjectives.ReadOnly = !enable;
        }

        public void ResetInputWindow()
        {
            this.lblId.Text = string.Empty;
            this.objectiveDate.Value = DateTime.Now;
            this.txtObjectives.Clear();
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
                    this.View_DeleteRecords(x => x.Id == id);
                    this.View_QueryRecords(null);
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
            Objective objective = new Objective
            {
                SystemCreated = DateTime.Now,
            };

            if (!string.IsNullOrEmpty(this.lblId.Text))
                objective = this.View_QueryResults
                    .Where(x => x.Id == int.Parse(this.lblId.Text))
                    .FirstOrDefault();

            objective.Date = this.objectiveDate.Value;
            objective.Description = this.txtObjectives.Text;
            objective.SystemUpdated = DateTime.Now;

            this.View_SaveRecord(objective);
            this.View_QueryRecords(null);
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
       
