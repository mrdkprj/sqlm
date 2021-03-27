using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.DbObject
{
    class IndexColumnDataFormatter : ISqlResultFormatter
    {
        static IndexColumnDataFormatter _instance = new IndexColumnDataFormatter();
        static IndexColumnDataFormatter() { }
        private IndexColumnDataFormatter() { }
        public static IndexColumnDataFormatter Instance
        {
            get { return _instance; }

        }

        public IEnumerable<SqlResult> Format(IEnumerable<SqlResult> sourceList)
        {
            foreach (var sqlResult in sourceList)
            {
                sqlResult.ColumnNames[(int)IndexColumnDataColumn.IndexNo] = IndexColumnDataColumn.IndexNo.DisplayName();
                sqlResult.ColumnNames[(int)IndexColumnDataColumn.ColumnName] = IndexColumnDataColumn.ColumnName.DisplayName();
            }

            return sourceList;
        }
    }
}
