using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public struct ExportImportParameter
    {
        public ExportImportParameter(string sql, string tableName)
        {
            this.Sql = sql;
            this.TableName = tableName;
        }

        public string Sql;
        public string TableName;
    }
}
