using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Editor
{
    public class Order : TableRow
    {
        public int Id { get; set; }
        public int OrderNum { get; set; }
        public string GoodName { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
