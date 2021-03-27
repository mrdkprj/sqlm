using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.DbObject
{
    class TableConstraintDataFormatter : ISqlResultFormatter
    {
        static TableConstraintDataFormatter _instance = new TableConstraintDataFormatter();
        static TableConstraintDataFormatter() { }
        private TableConstraintDataFormatter() { }
        public static TableConstraintDataFormatter Instance
        {
            get { return _instance; }
        }
        
        public IEnumerable<SqlResult> Format(IEnumerable<SqlResult> sourceList)
        {
            foreach (var sqlResult in sourceList)
            {
                sqlResult.ColumnNames[(int)TableConstraintDataColumn.SchemaName] = TableConstraintDataColumn.SchemaName.DisplayName();
                sqlResult.ColumnNames[(int)TableConstraintDataColumn.ConstraintName] = TableConstraintDataColumn.ConstraintName.DisplayName();
                sqlResult.ColumnNames[(int)TableConstraintDataColumn.ConstraintType] = TableConstraintDataColumn.ConstraintType.DisplayName();
            }

            return sourceList;
        }
    }
}
