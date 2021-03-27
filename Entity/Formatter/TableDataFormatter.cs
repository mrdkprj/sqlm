using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.DbObject
{
    class TableDataFormatter : ISqlResultFormatter
    {
        static TableDataFormatter _instance = new TableDataFormatter();
        static TableDataFormatter() { }
        private TableDataFormatter() { }
        public static TableDataFormatter Instance
        {
            get { return _instance; }
        }
        
        public IEnumerable<SqlResult> Format(IEnumerable<SqlResult> sourceList)
        {
            foreach (var sqlResult in sourceList)
            {
                sqlResult.ColumnNames[(int)TableDataColumn.Name] = TableDataColumn.Name.DisplayName();
                sqlResult.ColumnNames[(int)TableDataColumn.Comment] = TableDataColumn.Comment.DisplayName();
            }

            return sourceList;
        }
    }
}
