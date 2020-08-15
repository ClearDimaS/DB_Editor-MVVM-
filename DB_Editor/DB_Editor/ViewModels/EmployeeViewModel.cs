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
    class EmployeeViewModel : BaseViewModel<Employee, Unit>
    {
        public EmployeeViewModel(DB_Context _context) 
        {
            this.context = _context;
        }

        private DB_Context context;

        public override DataTable updateDataTable()
        {
            context.SaveChanges();
            DbConnection con = context.Database.Connection;
            con.Open();
            DbCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM EMPLOYEES";
            cmd.CommandType = CommandType.Text;
            DbDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dt.Columns.Add("Gen", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                int value = (int)row[5];
                row[6] = value == 0 ? GenderEnum.Male.ToString() : GenderEnum.Female.ToString();
            }
            dr.Close();
            con.Close();
            return dt;
        }

        public override void AddRow(Employee employee)
        {
            context.Employees.Add(employee);
        }

        public override Employee ItemFromSelectedRow(int selectedRowIndex)
        {
            int id = context.Employees.ToList()[selectedRowIndex].Id;
            return context.Employees.Find(id);
        }

        public override void Delete(int selectedRowIndex)
        {
            try
            {
                context.Employees.Remove(ItemFromSelectedRow(selectedRowIndex));
            }
            catch (Exception ex) 
            {
                MessageBox.Show("No item found in selected row");
            }
        }

        public override void Edit(Employee newTableRow, int selectedRowIndex)
        {
            Console.WriteLine(selectedRowIndex);
            Employee oldTableRow = ItemFromSelectedRow(selectedRowIndex);
            oldTableRow.Name = newTableRow.Name;
            oldTableRow.SurName = newTableRow.SurName;
            oldTableRow.LastName = newTableRow.LastName;
            oldTableRow.BirthDate = newTableRow.BirthDate;
            oldTableRow.Gender = newTableRow.Gender;
            oldTableRow.Unit = newTableRow.Unit; 
        }

        public override Unit FindReferencedRow(int selectedItemIndexInDataTable)
        {
            int id = context.Units.ToList()[selectedItemIndexInDataTable].Id;
            return context.Units.Find(id);
        }
    }
}
