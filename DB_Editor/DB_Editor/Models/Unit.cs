﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Editor
{
    public class Unit : TableRow
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
        public virtual Employee Boss { get; set; }
    }
}
