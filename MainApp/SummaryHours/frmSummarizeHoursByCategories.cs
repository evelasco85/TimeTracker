using Domain;
using Domain.Controllers;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MainApp
{
    public partial class frmSummarizeHoursByCategories : Form, ISummaryHoursByCategoriesView
    {
        public ISummaryHoursByCategoriesRequests ViewRequest { get; set; }
        public IEnumerable<LogEntry> QueryResults { get; set; }

        Form _parentForm;
        DateTime _selectedMonth;

        public frmSummarizeHoursByCategories()
        {
            InitializeComponent();
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
            this._selectedMonth = (DateTime)data.GetType().GetProperty("selectedMonth").GetValue(data, null);

            this.ViewRequest.GetData(null);
        }
        public void OnQueryRecordsCompletion()
        {
            DisplayLogEntries(_selectedMonth);
        }

        void DisplayLogEntries(DateTime selectedMonth)
        {
            IEnumerable<LogEntry> logs = this.QueryResults;

            this.ViewRequest.GetLogEntries(logs, selectedMonth);
        }

        public void OnGetLogEntriesCompletion(dynamic summarizedLogEntries)
        {
            this.dGridLogs.DataSource = summarizedLogEntries;
        }

        void DecorateGrid()
        {
            this.dGridLogs.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dGridLogs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            IDataGridHelper helper = DataGridHelper.GetInstance();

            helper.SetAutoResizeCells(ref this.dGridLogs);

            this.dGridLogs
                .Columns[SummaryHoursByCategoriesController.CATEGORY_INDEX]
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
        }

        private void dGridLogs_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.DecorateGrid();
        }
    }
}
