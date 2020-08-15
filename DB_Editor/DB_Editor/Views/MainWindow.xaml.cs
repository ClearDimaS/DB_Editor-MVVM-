using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace DB_Editor
{
    enum Tables 
    {
        Employee,
        Unit, 
        Order
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DB_Context context = null;

        EmployeeView employeeView;
        EmployeeViewModel employeeViewModel;

        UnitView unitView;
        UnitViewModel unitViewModel;

        OrderView orderView;
        OrderViewModel orderViewModel;

        public MainWindow()
        {
            InitializeComponent();
            setConnection();
        }

        private void setConnection()
        {
            context = new DB_Context();

            employeeView = new EmployeeView(EmpDataGrid);
            employeeViewModel = new EmployeeViewModel(context);

            unitView = new UnitView(UnitDataGrid);
            unitViewModel = new UnitViewModel(context);

            orderView = new OrderView(OrderDataGrid);
            orderViewModel = new OrderViewModel(context);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataTable();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            context.SaveChanges();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataTable();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditEntityFromRow();
            UpdateDataTable();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEntityFromRow();
            UpdateDataTable();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteEntityFromRow();
            UpdateDataTable();
        }

        private void AddEntityFromRow()
        {
            if (TabControlDB.SelectedIndex == 0)
            {
                employeeViewModel.AddRow(CreateEmployee());
            }
            else if (TabControlDB.SelectedIndex == 1) 
            {
                unitViewModel.AddRow(CreateUnit());
            }
            else
            {
                orderViewModel.AddRow(CreateOrder());
            }
        }

        private void DeleteEntityFromRow()
        {
            if (TabControlDB.SelectedIndex == 0)
            {
                int selectedRowIndex = employeeView.CurrentRowIndex();
                employeeViewModel.Delete(selectedRowIndex);
            }
            else if (TabControlDB.SelectedIndex == 1)
            {
                int selectedRowIndex = unitView.CurrentRowIndex();
                unitViewModel.Delete(selectedRowIndex);
            }
            else
            {
                int selectedRowIndex = orderView.CurrentRowIndex();
                orderViewModel.Delete(selectedRowIndex);
            }
        }

        private void EditEntityFromRow()
        {
            if (TabControlDB.SelectedIndex == 0)
            {
                int selectedRowIndex = employeeView.CurrentRowIndex();
                employeeViewModel.Edit(CreateEmployee(), selectedRowIndex);
            }
            else if (TabControlDB.SelectedIndex == 1)
            {
                int selectedRowIndex = unitView.CurrentRowIndex();
                unitViewModel.Edit(CreateUnit(), selectedRowIndex);
            }
            else
            {
                int selectedRowIndex = orderView.CurrentRowIndex();
                orderViewModel.Edit(CreateOrder(), selectedRowIndex);
            }
        }

        private void UpdateDataTable()
        {
            if (TabControlDB.SelectedIndex == 0)
            {
                DataTable dt = employeeViewModel.updateDataTable();
                employeeView.updateDataGrid(dt);
            }
            else if (TabControlDB.SelectedIndex == 1)
            {
                DataTable dt = unitViewModel.updateDataTable();
                unitView.updateDataGrid(dt);
            }
            else
            {
                DataTable dt = orderViewModel.updateDataTable();
                orderView.updateDataGrid(dt);
            }
        }

        private Employee CreateEmployee()
        {
            Employee employee = new Employee()
            {
                Name = txtbxEmpName.Text,
                SurName = txtbxEmpSurName.Text,
                LastName = txtbxEmpLastName.Text,
                BirthDate = dateTimeEmpBirthDate.SelectedDate.HasValue ? (DateTime)dateTimeEmpBirthDate.SelectedDate : DateTime.Now,
                Gender = comboBoxEmpGender.Items[0] == comboBoxEmpGender.SelectedItem ? GenderEnum.Male : GenderEnum.Female,
                Unit = FindReferencedUnit()
            };

            return employee;
        }

        private Unit FindReferencedUnit()
        {
            return context.Units.Find(150);
        }

        private Unit CreateUnit()
        {
            Unit unit = new Unit()
            {
                UnitName = txtbxUnitName.Text,
                //Boss = new Employee()
            };
            return unit;
        }

        private Order CreateOrder() 
        {
            int tmp = 0;
            Order order = new Order()
            {
                OrderNum = int.TryParse(txtbxOrderNum.Text, out tmp) ? tmp : 0,
                GoodName = txtbxOrderName.Text,
                //Employee = new Employee()
            };
            return order;
        }

        private void TabControlDB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCombox();
        }

        private void UpdateCombox()
        {
            if (TabControlDB.SelectedIndex == 0)
            {
                comboBoxEmpUnitRef.Items.Clear();
                employeeView.SetReferencedValueToComboBoxView(context, comboBoxEmpUnitRef);
            }
            else if (TabControlDB.SelectedIndex == 1)
            {
                comboBoxUnitEmpRef.Items.Clear();
                unitView.SetReferencedValueToComboBoxView(context, comboBoxUnitEmpRef);
            }
            else
            {
                comboBoxOrderEmpRef.Items.Clear();
                orderView.SetReferencedValueToComboBoxView(context, comboBoxOrderEmpRef);
            }
        }
    }
}
