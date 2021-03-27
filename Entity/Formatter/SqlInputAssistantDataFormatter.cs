using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    class SqlInputAssistantDataFormatter : ISqlResultFormatter
    {
        readonly string _columnName1 = "Name";
        readonly string _columnName2 = "Size"; 
        
        static SqlInputAssistantDataFormatter _instance = new SqlInputAssistantDataFormatter();
        static SqlInputAssistantDataFormatter() { }
        private SqlInputAssistantDataFormatter() { }
        public static SqlInputAssistantDataFormatter Instance
        {
            get { return _instance; }

        }

        public IEnumerable<SqlResult> Format(IEnumerable<SqlResult> sourceList)
        {
            SqlResult sourceSqlResut = sourceList.FirstOrDefault();

            if (sourceSqlResut == null || sourceSqlResut.ColumnNames.Count <= 0)
                yield return null;

            for (int i = 0; i < sourceSqlResut.ColumnNames.Count; i++)
            {
                SqlResult convertedResult = new SqlResult();
                convertedResult.ColumnNames.Add(_columnName1);
                convertedResult.ColumnNames.Add(_columnName2);
                convertedResult.ColumnTypes.Add(typeof(string));
                convertedResult.ColumnTypes.Add(typeof(string));
                convertedResult.RowValues.Add(sourceSqlResut.ColumnNames[i]);
                convertedResult.RowValues.Add(sourceSqlResut.RowValues[i]);
                yield return convertedResult;
            }
        }
    }
}
