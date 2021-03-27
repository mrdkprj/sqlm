using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.DbObject
{
    class TableIndexDataFormatter : ISqlResultFormatter
    {
        string _name = null;

        static TableIndexDataFormatter _instance = new TableIndexDataFormatter();
        static TableIndexDataFormatter() { }
        private TableIndexDataFormatter() { }
        public static TableIndexDataFormatter Instance
        {
            get { return _instance; }
        }

        public IEnumerable<SqlResult> Format(IEnumerable<SqlResult> sourceList)
        {
            _name = null;

            foreach (var sqlResult in sourceList)
            {
                sqlResult.ColumnNames[(int)TableIndexDataColumn.IndexName] = TableIndexDataColumn.IndexName.DisplayName();
                sqlResult.ColumnNames[(int)TableIndexDataColumn.IndexNo] = TableIndexDataColumn.IndexNo.DisplayName();
                sqlResult.ColumnNames[(int)TableIndexDataColumn.ColumnName] = TableIndexDataColumn.ColumnName.DisplayName();

                SetRowValues(sqlResult);
            }

            return sourceList;
        }

        void SetRowValues(SqlResult sqlResult)
        {
            if (sqlResult.RowValues.Count <= 0)
                return;

            if (_name == sqlResult.RowValues[(int)TableIndexDataColumn.IndexName])
                sqlResult.RowValues[(int)TableIndexDataColumn.IndexName] = string.Empty;
            else
                _name = sqlResult.RowValues[(int)TableIndexDataColumn.IndexName];
        }
    }
}
