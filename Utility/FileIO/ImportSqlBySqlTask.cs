using MasudaManager.Utility.Preference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace MasudaManager.Utility
{
    public class ImportSqlBySqlTask : ImportBySqlTask
    {       
        public ImportSqlBySqlTask(ImportModel importModel) : base(importModel) { }

        protected override IDbCommandBuilder GetInsertCommands(CancellationToken token)
        {
            IDbCommandBuilder _builder = new DbCommandBuilder();

            foreach (string sql in GetInsertSqls(this.ImportModel.ImportFilePath, UserPreference.Reflector.IOEncoding))
            {
                token.ThrowIfCancellationRequested();

                _builder.CreateCommand(sql);
            }

            if (_builder.Count <= 0)
                throw new Exception(LocalizedTextProvider.Message.Error.NoDataToImport);
            else
                SetTotalCount(_builder.Count);

            return _builder;
        }

        IEnumerable<string> GetInsertSqls(string path, Encoding encoding)
        {
            using (var stream = new FileStream(path, this.FileMode, this.FileAccess, UserPreference.Reflector.FileShareMode))
            {
                using (var reader = new StreamReader(stream, encoding))
                {
                    while (!reader.EndOfStream)
                    {
                        yield return reader.ReadLine().TrimEnd(Constants.CharSemicolon);
                    }
                }
            }
        }
    }
}
