using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp
{
    public partial class frmTaskMonitoringEntry : Form
    {
        DateTime _dateTimeInvoked = DateTime.Now;
        bool _rememberSetting = false;
        DateTime _rememberedCreatedDateTime;
        LogEntry _logEntry;

        public LogEntry LogEntry
        {
            get { return _logEntry; }
        }

        public bool RememberSetting
        {
            get { return _rememberSetting; }
        }
        

        public frmTaskMonitoringEntry()
        {
            InitializeComponent();
        }

        public frmTaskMonitoringEntry(IEnumerable<string> Categories, bool rememberSetting, DateTime rememberedCreatedDateTime) : this()
        {
            this.cboCategory.DataSource =  Categories.ToList();
            this.dateTimeManualEntry.Format = DateTimePickerFormat.Custom;
            this.dateTimeManualEntry.CustomFormat = @"MM'/'dd'/'yyyy hh':'mm tt";
            this._rememberSetting = rememberSetting;

            if (rememberSetting)
            {
                DateTime rememberDate = rememberedCreatedDateTime.Date;

                rememberDate = rememberDate.Add(DateTime.Now.TimeOfDay);
                _rememberedCreatedDateTime = rememberDate;
            }

            this.SetDayOfWeek();
        }

        public frmTaskMonitoringEntry(
            IEnumerable<string> Categories,
            int primaryKey, string category,
            string description, bool rememberSetting,
            DateTime createdDate, DateTime systemCreatedDate)
            : this(Categories, rememberSetting, createdDate)
        {
            this._logEntry = new LogEntry
            {
                Id = primaryKey,
                Category = category,
                Description = description,
                Created = createdDate,
                System_Created = systemCreatedDate
            };

            this.cboCategory.Text = category;
            this.txtDescription.Text = description;
            this.dateTimeManualEntry.Value = createdDate;
            this._dateTimeInvoked = systemCreatedDate;
            this._rememberSetting = rememberSetting;
            this._rememberedCreatedDateTime = createdDate;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this._logEntry == null)
                this._logEntry = new LogEntry();
            
            this._logEntry.Category = this.cboCategory.Text;
            this._logEntry.Created = this.dateTimeManualEntry.Value;
            this._logEntry.Description = this.txtDescription.Text;
            this._logEntry.System_Created = this._dateTimeInvoked;
            this._logEntry.SystemUpdateDateTime = DateTime.Now;

            this._rememberSetting = this.chkRememberSettings.Checked;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void frmTaskMonitoringEntry_Load(object sender, EventArgs e)
        {
            this.chkRememberSettings.Checked = _rememberSetting;
            this.dateTimeManualEntry.Value = (this._rememberedCreatedDateTime.Date == DateTime.MinValue.Date) ? DateTime.Now : _rememberedCreatedDateTime;
        }

        void SetDayOfWeek()
        {
            DayOfWeek dayName = this.dateTimeManualEntry.Value.DayOfWeek;
            this.lblDay.Text = dayName.ToString();

            switch (dayName)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    this.lblDay.ForeColor = Color.Red;
                    break;
                default:
                    this.lblDay.ForeColor = Color.Blue;
                    break;
            }
        }

        void IncrementDayByOne()
        {
            this.dateTimeManualEntry.Value = this.dateTimeManualEntry.Value.AddDays(1); 

            this.SetDayOfWeek();
        }

        private void btnIncementDayByOne_Click(object sender, EventArgs e)
        {
            this.IncrementDayByOne();
        }

        private void dateTimeManualEntry_ValueChanged(object sender, EventArgs e)
        {
            this.SetDayOfWeek();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
