using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp
{
    public interface IDataGridHelper
    {
        void SetAutoResizeCells(ref DataGridView dataGrid);
        void SetColumnToDateFormat(DataGridViewColumn column);
        void SetColumnToDateFormat(DataGridViewColumn column, string format);
        void SetColumnToTimeFormat(DataGridViewColumn column);
        void SetColumnToDayFormat(DataGridViewColumn column);
    }

    public class DataGridHelper : IDataGridHelper
    {
        static IDataGridHelper _instance;
        static readonly object _threadsafeLock = new object();

        private DataGridHelper()
        {
        }

        public static IDataGridHelper GetInstance()
        {
            lock(_threadsafeLock)
            {
                if (_instance == null)
                    _instance = new DataGridHelper();

                return _instance;
            }
        }

        public void SetAutoResizeCells(ref DataGridView dataGrid)
        {
            if ((dataGrid == null) || (dataGrid.Columns.Count < 1))
                return;

            dataGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            DataGridViewColumnCollection columns = dataGrid.Columns;

            for(int index = 0; index < columns.Count; index++)
            {
                DataGridViewColumn column = columns[index];

                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dataGrid.AutoResizeColumn(index);
            }
        }

        public void SetColumnToDateFormat(DataGridViewColumn column)
        {
            column.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "MM/dd/yyyy"
            };
        }

        public void SetColumnToTimeFormat(DataGridViewColumn column)
        {
            column.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "HH:mm"
            };
        }

        public void SetColumnToDateFormat(DataGridViewColumn column, string format)
        {
            column.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = format
            };
        }
        public void SetColumnToDayFormat(DataGridViewColumn column)
        {
            column.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "dddd"
            };
        }
    }
}
