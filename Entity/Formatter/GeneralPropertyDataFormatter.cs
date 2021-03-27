using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.DbObject
{
    class GeneralPropertyDataFormatter : ISqlResultFormatter
    {
        static GeneralPropertyDataFormatter _instance = new GeneralPropertyDataFormatter();
        static GeneralPropertyDataFormatter() { }
        private GeneralPropertyDataFormatter() { }
        public static GeneralPropertyDataFormatter Instance
        {
            get { return _instance; }
        }

        public IEnumerable<SqlResult> Format(IEnumerable<SqlResult> sourceList)
        {
            foreach (var sqlResult in sourceList)
            {
                for (int i = 0; i < sqlResult.RowValues.Count; i++)
                {
                    SqlResult newReulstSet = new SqlResult();
                    newReulstSet.ColumnNames.Add(GeneralPropertyDataColumn.Name.DisplayName());
                    newReulstSet.ColumnTypes.Add(typeof(String));
                    newReulstSet.RowValues.Add(sqlResult.ColumnNames[i]);
                    newReulstSet.ColumnNames.Add(GeneralPropertyDataColumn.Value.DisplayName());
                    newReulstSet.ColumnTypes.Add(sqlResult.ColumnTypes[i]);
                    newReulstSet.RowValues.Add(sqlResult.RowValues[i]);
                    yield return newReulstSet;
                }
            }
        }
    }
}
