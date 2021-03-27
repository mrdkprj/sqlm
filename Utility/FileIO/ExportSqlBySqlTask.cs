using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MasudaManager.Utility.Preference;

namespace MasudaManager.Utility
{
    public class ExportSqlBySqlTask : ExportBySqlTask
    {
        SqlStatementBuilder _statementBuilder = new SqlStatementBuilder(true);

        public ExportSqlBySqlTask(ExportModel exportModel) : base(exportModel) { }

        protected override void WriteRetrievedSqlResults(CancellationToken token)
        {
            using (var stream = GetFileStream())
            {
                using (var outputFile = new StreamWriter(stream, UserPreference.Reflector.IOEncoding))
                {
                    foreach (var sqlResult in this.ExportData)
                    {
                        outputFile.WriteLine(GetInsertSql(sqlResult, token));
                        UpdateProgress();
                        Notify();
                    }
                }
            }
        }

        FileStream GetFileStream()
        {
            return new FileStream(this.ExportModel.ExportFilePath, this.FileMode, this.FileAccess, UserPreference.Reflector.FileShareMode);
        }
        
        string GetInsertSql(SqlResult sqlResult, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return _statementBuilder.CreateInsertStatement(this.ExportModel.ExportTableName, sqlResult);
        }
    }
}
