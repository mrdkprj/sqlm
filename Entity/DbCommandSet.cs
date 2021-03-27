using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MasudaManager.Utility
{
    public class DbCommandSet 
    {
        public DbCommandSet()
        {
            this.DbCommand = null;
            this.SqlInfo = new SqlBaseInfo();
        }

        public DbCommandSet(IDbCommand command, SqlBaseInfo sqlInfo)
        {
            this.DbCommand = command;
            this.SqlInfo = sqlInfo;
        }

        public IDbCommand DbCommand { get; set; }
        public SqlBaseInfo SqlInfo { get; set; }
    }
}
