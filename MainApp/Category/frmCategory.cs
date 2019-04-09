using Domain;
using Domain.Controllers;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MainApp
{
    public partial class frmCategory : frmCommonDataEditor, ICategoryView, IFormCommonOperation
    {
        public ICategoryRequests ViewRequest { get; set; }
        public IEnumerable<Category> QueryResults { get; set; }

        Form _parentForm;

        public frmCategory()
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

            this.ViewRequest.GetData(null);
        }

        public void OnQueryRecordsCompletion()
        {
            IEnumerable<Category> categories = this.QueryResults;

            this.ViewRequest.GetCategoryData(categories);
        }

        public void OnGetCategoryDataCompletion(dynamic displayColumns, DateTime lastUpdatedDate)
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
                    DateTime systemDate = DateTime.Parse(this.dGrid.Rows[index].Cells[HolidayController.SYSTEM_UPDATED_INDEX].Value.ToString());
                    bool identicalTime = recordDate.ToLongTimeString() == systemDate.ToLongTimeString();

                    if ((recordDate.Date == systemDate.Date) && identicalTime)
                    {
                        this.dGrid.CurrentCell = this.dGrid[HolidayController.ID_INDEX, index];
                        this.dGrid.Rows[index].Selected = true;
                        this.dGrid.Rows[index].Cells[HolidayController.ID_INDEX].Selected = true;
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
                int id = int.Parse(this.dGrid.Rows[rowIndex].Cells[CategoryController.ID_INDEX].Value.ToString());
                Category category = this.QueryResults
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                this.lblId.Text = category.Id.ToString();
                this.txtCategoryName.Text = category.Name;
                this.chkShowInSummary.Checked = category.ShowInSummary;
                this.chkShowInTaskEntry.Checked = category.ShowInTaskEntry;
            }
            catch (ArgumentOutOfRangeException) { /*Skip*/}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnableInputWindow(bool enable)
        {
            this.txtCategoryName.ReadOnly = !enable;
            this.chkShowInSummary.Enabled = enable;
            this.chkShowInTaskEntry.Enabled = enable;
        }

        public void ResetInputWindow()
        {
            this.lblId.Text = string.Empty;
            this.txtCategoryName.Clear();
            this.chkShowInSummary.Checked = false;
            this.chkShowInTaskEntry.Checked = false;
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
            Category category = new Category
            {
                System_Created = DateTime.Now,
            };

            if (!string.IsNullOrEmpty(this.lblId.Text))
                category = this.QueryResults
                    .Where(x => x.Id == int.Parse(this.lblId.Text))
                    .FirstOrDefault();

            category.Name = this.txtCategoryName.Text;
            category.ShowInSummary = this.chkShowInSummary.Checked;
            category.ShowInTaskEntry = this.chkShowInTaskEntry.Checked;
            category.SystemUpdateDateTime = DateTime.Now;

            this.ViewRequest.SaveData(category);
            this.ViewRequest.GetData(null);
            this.WindowInputChanges(ModifierState.Save);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.WindowInputChanges(ModifierState.Add);
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
