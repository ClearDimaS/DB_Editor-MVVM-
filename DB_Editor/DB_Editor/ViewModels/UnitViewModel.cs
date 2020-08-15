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
    class UnitViewModel : BaseViewModel<Unit, Employee>
    {
        public UnitViewModel(DB_Context _context)
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
            cmd.CommandText = "SELECT * FROM UNITS";
            cmd.CommandType = CommandType.Text;
            DbDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            con.Close();
            return dt;
        }

        public override void AddRow(Unit unit)
        {
            context.Units.Add(unit);
        }

        public override Unit ItemFromSelectedRow(int selectedRowIndex)
        {
            int id = context.Units.ToList()[selectedRowIndex].Id;
            return context.Units.Find(id);
        }

        public override void Delete(int selectedRowIndex)
        {
            try
            {
                context.Units.Remove(ItemFromSelectedRow(selectedRowIndex));
            }
            catch (Exception ex)
            {
                MessageBox.Show("No item found in selected row");
            }
        }

        public override void Edit(Unit newItem, int selectedRowIndex)
        {
            Console.WriteLine(selectedRowIndex);
            Unit oldItem = ItemFromSelectedRow(selectedRowIndex);
            oldItem.UnitName = newItem.UnitName;
            oldItem.Boss = oldItem.Boss;
        }

        public override Employee FindReferencedRow(int selectedItemIndexInDataTable)
        {
            int id = context.Employees.ToList()[selectedItemIndexInDataTable].Id;
            return context.Employees.Find(id);
        }
    }
}
