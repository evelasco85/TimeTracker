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
    public partial class frmHolidays : frmCommonDataEditor, IHolidayView
    {
        public Action<Func<Holiday, bool>> QueryViewRecords { get; set; }
        public Action OnQueryViewRecordsCompletion { get; set; }
        public Action<Holiday> SaveViewRecord { get; set; }
        public Action<Func<Holiday, bool>> DeleteViewRecords { get; set; }
        public IEnumerable<Holiday> ViewQueryResult { get; set; }
        public Action<IEnumerable<Holiday>> GetHolidayData { get; set; }
        public Action<dynamic, DateTime> OnGetHolidayDataCompletion { get; set; }

        public frmHolidays(IEFRepository repository) : base()
        {
            InitializeComponent();

            Action RegisterController = () => new HolidayController(repository, this);

            RegisterController();

            this.OnQueryViewRecordsCompletion = this.RefreshGridData;
            this.OnGetHolidayDataCompletion = this.UpdateHolidayData;
            this.holidayDate.Value = DateTime.Now;

            this.QueryViewRecords(null);
        }

        void RefreshGridData()
        {
            IEnumerable<Holiday> holidays = this.ViewQueryResult;

            this.GetHolidayData(holidays);
        }

        void UpdateHolidayData(dynamic displayColumns, DateTime lastUpdatedDate)
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

        public override void UpdateWindow(int rowIndex)
        {
            try
            {
                int id = int.Parse(this.dGrid.Rows[rowIndex].Cells[HolidayController.ID_INDEX].Value.ToString());
                Holiday holiday = DateHelper.GetInstance().GetHoliday(this.ViewQueryResult, id);

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

        public override void EnableInputWindow(bool enable)
        {
            this.holidayDate.Enabled = enable;
            this.txtHolidayDescription.Enabled = enable;
        }

        public override void ResetInputWindow()
        {
            this.lblId.Text = string.Empty;
            this.holidayDate.Value = DateTime.Now;
            this.txtHolidayDescription.Clear();
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
            Holiday holiday = new Holiday
            {
                SystemCreated = DateTime.Now,
            };

            if (!string.IsNullOrEmpty(this.lblId.Text))
                holiday = DateHelper.GetInstance().GetHoliday(this.ViewQueryResult, int.Parse(this.lblId.Text));

            holiday.Date = this.holidayDate.Value;
            holiday.Description = this.txtHolidayDescription.Text;
            holiday.SystemUpdated = DateTime.Now;

            this.SaveViewRecord(holiday);
            this.QueryViewRecords(null);

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
    }
}
       
