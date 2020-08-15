using System.Data.Entity;
using System;
using System.Data.Entity.Core.EntityClient;

//using MySql.Data.MySqlClient;
//using System.Windows;
//using System.Data;
//using System.Collections.Generic;
//using System.Linq;

namespace DB_Editor
{
    public class DB_Context : DbContext
    {
        public DB_Context() : base("DefaultConnection") //59:36
        {
        }
    
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Unit> Units { get; set; }
    }
}
