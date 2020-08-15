
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System;

namespace DB_Editor
{
    class OrderView : BaseView
    {
        public OrderView(DataGrid _employeeDataGrid)
        {
            employeeDataGrid = _employeeDataGrid;
        }

        DataGrid employeeDataGrid;

        public override void updateDataGrid(DataTable dataTable)
        {
            employeeDataGrid.ItemsSource = dataTable.DefaultView;
            //employeeDataGrid.Columns[5].Visibility = Visibility.Hidden;
        }

        public override void SetReferencedValueToComboBoxView(DB_Context context, ComboBox comboBox)
        {
            comboBox.Items.Clear();
            foreach (Employee employee in context.Employees)
                comboBox.Items.Add(employee.Name);
        }

        public override void SystemResponse(string msg)
        {
            try
            {
                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override int CurrentRowIndex()
        {
            int curDataTableInd = employeeDataGrid.SelectedIndex;
            return curDataTableInd;
        }
    }
}
