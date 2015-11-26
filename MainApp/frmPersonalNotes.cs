using Domain;
using Domain.Controllers;
using Domain.Infrastructure;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MainApp
{
    public partial class frmPersonalNotes : frmCommonDataEditor, IPersonalNoteView, IFormCommonOperation
    {
        public Action<Func<PersonalNote, bool>> QueryViewRecords { get; set; }
        public Action OnQueryViewRecordsCompletion { get; set; }
        public Action<PersonalNote> SaveViewRecord { get; set; }
        public Action<Func<PersonalNote, bool>> DeleteViewRecords { get; set; }
        public IEnumerable<PersonalNote> ViewQueryResult { get; set; }
        public Action<IEnumerable<PersonalNote>> GetPersonalNoteData { get; set; }
        public Action<dynamic, DateTime> OnGetPersonalNoteDataCompletion { get; set; }

        public frmPersonalNotes(IEFRepository repository)
        {
            InitializeComponent();

            Action RegisterController = () => new PersonalNoteController(repository, this);

            RegisterController();
            this.RegisterCommonOperation(this);

            this.OnQueryViewRecordsCompletion = this.RefreshGridData;
            this.OnGetPersonalNoteDataCompletion = this.UpdateCategoryData;

            this.QueryViewRecords(null);
        }

        void RefreshGridData()
        {
            IEnumerable<PersonalNote> personalNotes = this.ViewQueryResult;

            this.GetPersonalNoteData(personalNotes);
        }

        void UpdateCategoryData(dynamic displayColumns, DateTime lastUpdatedDate)
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
                    DateTime systemDate = DateTime.Parse(this.dGrid.Rows[index].Cells[PersonalNoteController.SYSTEM_UPDATED_INDEX].Value.ToString());
                    bool identicalTime = recordDate.ToLongTimeString() == systemDate.ToLongTimeString();

                    if ((recordDate.Date == systemDate.Date) && identicalTime)
                    {
                        this.dGrid.CurrentCell = this.dGrid[PersonalNoteController.ID_INDEX, index];
                        this.dGrid.Rows[index].Selected = true;
                        this.dGrid.Rows[index].Cells[PersonalNoteController.ID_INDEX].Selected = true;
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
                int id = int.Parse(this.dGrid.Rows[rowIndex].Cells[PersonalNoteController.ID_INDEX].Value.ToString());
                PersonalNote note = this.ViewQueryResult
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                this.lblId.Text = note.Id.ToString();
                this.txtDescription.Text = note.Description;
            }
            catch (ArgumentOutOfRangeException) { /*Skip*/}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnableInputWindow(bool enable)
        {
            this.txtDescription.Enabled = enable;
        }

        public void ResetInputWindow()
        {
            this.lblId.Text = string.Empty;
            this.txtDescription.Clear();
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
            PersonalNote note = new PersonalNote
            {
                System_Created = DateTime.Now,
            };

            if (!string.IsNullOrEmpty(this.lblId.Text))
                note = this.ViewQueryResult
                    .Where(x => x.Id == int.Parse(this.lblId.Text))
                    .FirstOrDefault();

            note.Description = this.txtDescription.Text;
            note.SystemUpdateDateTime = DateTime.Now;

            this.SaveViewRecord(note);
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
