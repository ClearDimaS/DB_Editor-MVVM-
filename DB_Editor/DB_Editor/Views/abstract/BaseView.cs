using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace DB_Editor
{
    abstract class BaseView
    {    
        public abstract void updateDataGrid(DataTable dataTable);

        public abstract void SetReferencedValueToComboBoxView(DB_Context context, ComboBox comboBox);

        public abstract void SystemResponse(string msg);

        public abstract int CurrentRowIndex();
    }
}
