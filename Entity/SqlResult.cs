using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MasudaManager
{
    public class SqlResult
    {
        public SqlResult()
        {
            this.ColumnNames = new List<string>();
            this.ColumnTypes = new List<Type>();
            this.RowValues = new List<string>();
            this.AffectedRowCount = 0;
        }

        public List<string> ColumnNames { get; set; }
        public List<Type> ColumnTypes { get; set; }
        public List<string> RowValues { get; set; }
        public int AffectedRowCount { get; set; }
    }
}
