using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Editor
{
    abstract class BaseViewModel<T, U>
    {
        private DB_Context context;

        public abstract DataTable updateDataTable();

        public abstract void AddRow(T DataTableRow);

        public abstract void Edit(T DataTableRow, int selectedRowIndex);

        public abstract void Delete(int selectedRowIndex);

        public abstract U FindReferencedRow(int selectedRowIndexInDataTable);

        public abstract T ItemFromSelectedRow(int curDataTableInd);
    }
}