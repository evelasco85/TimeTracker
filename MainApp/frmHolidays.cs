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
    public partial class frmHolidays : Form, IView<Holiday>
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
        public Action<Func<Holiday, bool>> QueryViewRecords { get; set; }
        public Action OnQueryViewRecordsCompletion { get; set; }
        public Action<Holiday> SaveViewRecord { get; set; }
        public Action<Func<Holiday, bool>> DeleteViewRecords { get; set; }
        public IEnumerable<Holiday> ViewQueryResult { get; set; } 

        public frmHolidays(IEFRepository repository)
        {
            Action RegisterController = () => new HolidayController(repository, this);

            RegisterController();

            this.OnQueryViewRecordsCompletion = this.RefreshGridData;
            this._helper = DateHelper.GetInstance();

            InitializeComponent();

            this.holidayDate.Value = DateTime.Now;

            this.QueryViewRecords(null);
            this.WindowInputChanges(ModifierState.Cancel);
        }

        void RefreshGridData()
        {
            IEnumerable<Holiday> holidays = this.ViewQueryResult;
            var displayColumns = this._helper.GetHolidays(holidays);
            this.dGridHolidays.DataSource = displayColumns;

            this.dGridHolidays.Refresh();

            DateTime lastUpdatedDate = displayColumns
                .Select(x => x.SystemUpdated)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            this.HighlightRecordByDate(lastUpdatedDate);
        }

        void HighlightRecordByDate(DateTime recordDate)
        {
            for (int index = 0; index < this.dGridHolidays.Rows.Count; index++)
            {
                try
                {
                    DateTime systemDate = DateTime.Parse(this.dGridHolidays.Rows[index].Cells[HolidayController.SYSTEM_UPDATED_INDEX].Value.ToString());
                    bool identicalTime = recordDate.ToLongTimeString() == systemDate.ToLongTimeString();

                    if ((recordDate.Date == systemDate.Date) && identicalTime)
                    {
                        this.dGridHolidays.CurrentCell = this.dGridHolidays[HolidayController.ID_INDEX, index];
                        this.dGridHolidays.Rows[index].Selected = true;
                        this.dGridHolidays.Rows[index].Cells[HolidayController.ID_INDEX].Selected = true;
                        this.dGridHolidays.FirstDisplayedScrollingRowIndex = index;

                        this.dGridHolidays.Update();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void dGridHolidays_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpdateWindow(e.RowIndex);
        }

        void UpdateWindow(int rowIndex)
        {
            try
            {
                int id = int.Parse(this.dGridHolidays.Rows[rowIndex].Cells[HolidayController.ID_INDEX].Value.ToString());

                this.QueryViewRecords(null);

                Holiday holiday = this._helper.GetHoliday(this.ViewQueryResult, id);

                this.lblId.Text = holiday.Id.ToString();
                this.holidayDate.Value = holiday.Date;
                this.txtHolidayDescription.Text = holiday.Description;
            }
            catch (ArgumentOutOfRangeException) { /*Skip*/}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Holiday GetSelectedHoliday()
        {
            DataGridViewRow row = this.dGridHolidays.CurrentRow;
            int id = int.Parse(row.Cells[HolidayController.ID_INDEX].Value.ToString());

            this.QueryViewRecords(null);

            Holiday holiday = this._helper.GetHoliday(this.ViewQueryResult, id);

            return holiday;
        }

        void EnableInputWindow(bool enable)
        {
            this.holidayDate.Enabled = enable;
            this.txtHolidayDescription.Enabled = enable;
        }

        void ResetInputWindow()
        {
            this.lblId.Text = string.Empty;
            this.holidayDate.Value = DateTime.Now;
            this.txtHolidayDescription.Clear();
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
            this.dGridHolidays.Enabled = enable;
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
            Holiday holiday = new Holiday
            {
                SystemCreated = DateTime.Now,
            };

            if (!string.IsNullOrEmpty(this.lblId.Text))
            {
                this.QueryViewRecords(null);

                holiday = this._helper.GetHoliday(this.ViewQueryResult, int.Parse(this.lblId.Text));
            }

            holiday.Date = this.holidayDate.Value;
            holiday.Description = this.txtHolidayDescription.Text;
            holiday.SystemUpdated = DateTime.Now;

            this.SaveViewRecord(holiday);
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
       
