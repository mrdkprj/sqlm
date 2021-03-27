using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.DbObject
{
    class TableColumnDataFormatter : ISqlResultFormatter
    {
        static TableColumnDataFormatter _instance = new TableColumnDataFormatter();
        static TableColumnDataFormatter() { }
        private TableColumnDataFormatter() { }
        public static TableColumnDataFormatter Instance
        {
            get { return _instance; }
        }

        public IEnumerable<SqlResult> Format(IEnumerable<SqlResult> sourceList)
        {
            foreach (var sqlResult in sourceList)
            {
                sqlResult.ColumnNames[(int)TableColumnDataColumn.ColumnID] = TableColumnDataColumn.ColumnID.DisplayName();
                sqlResult.ColumnNames[(int)TableColumnDataColumn.ColumnName] = TableColumnDataColumn.ColumnName.DisplayName();
                sqlResult.ColumnNames[(int)TableColumnDataColumn.Comment] = TableColumnDataColumn.Comment.DisplayName();
                sqlResult.ColumnNames[(int)TableColumnDataColumn.DataType] = TableColumnDataColumn.DataType.DisplayName();
                sqlResult.ColumnNames[(int)TableColumnDataColumn.DataLength] = TableColumnDataColumn.DataLength.DisplayName();
                sqlResult.ColumnNames[(int)TableColumnDataColumn.Nullable] = TableColumnDataColumn.Nullable.DisplayName();
            }

            return sourceList;
        }
    }
}
