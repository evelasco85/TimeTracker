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

        public Action<Func<LogEntry, bool>> View_QueryRecords { get; set; }
        public Action View_OnQueryRecordsCompletion { get; set; }
        public Action<Func<LogEntry, bool>> View_DeleteRecords { get; set; }
        public Action<LogEntry> View_SaveRecord { get; set; }
        public IEnumerable<LogEntry> View_QueryResults { get; set; }
        public Action<object> View_ViewReady { get; set; }
        public Action<object> View_OnViewReady { get; set; }
        public Action View_OnShow { get; set; }

        Form _parentForm;

        public frmSummarizeHoursByCategories()
        {
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
            DateTime selectedMonth = (DateTime)data.GetType().GetProperty("selectedMonth").GetValue(data, null);
            this.View_OnQueryRecordsCompletion = () => DisplayLogEntries(selectedMonth);

            this.View_QueryRecords(null);
        }

        void DisplayLogEntries(DateTime selectedMonth)
        {
            IEnumerable<LogEntry> logs = this.View_QueryResults;

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
