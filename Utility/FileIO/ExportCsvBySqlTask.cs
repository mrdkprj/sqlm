using MasudaManager.Utility.Preference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace MasudaManager.Utility
{
    public class ExportCsvBySqlTask : ExportBySqlTask
    {
        //readonly string _escapeSequence = "\"\"";

        public ExportCsvBySqlTask(ExportModel exportModel) : base(exportModel) { }

        protected override void WriteRetrievedSqlResults(CancellationToken token)
        {
            using (var stream = GetFileStream())
            {
                using (var outputFile = new StreamWriter(stream, UserPreference.Reflector.IOEncoding))
                {
                    if (this.ExportModel.CsvExportHeaderOption == CsvHeaderOption.ColumnNameHeader)
                        outputFile.WriteLine(CreateHeaderRecord(token));

                    foreach (var sqlResult in this.ExportData)
                    {
                        outputFile.WriteLine(CreateValueRecord(sqlResult, token));
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

        string CreateHeaderRecord(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return GetSeparatedString(this.ExportData[0].ColumnNames.Select(s => StringUtil.GetEnclosedCsvFiledValue(s)));
            //if (UserPreference.Setting.File.EncloseCsvFields)
            //    return GetSeparatedString(this.ExportData[0].ColumnNames.Select(s => GetEnclosedFiledValue(s)));
            //else
            //    return GetSeparatedString(this.ExportData[0].ColumnNames.Select(s => StringUtil.EncloseCsvFieldValueIfSpeicalCharExists(s)));
        }

        string CreateValueRecord(SqlResult sqlResult, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return GetSeparatedString(sqlResult.RowValues.Select(s => StringUtil.GetEnclosedCsvFiledValue(s)));

            //if (UserPreference.Setting.File.EncloseCsvFields)
            //    return GetSeparatedString(sqlResult.RowValues.Select(s => GetEnclosedFiledValue(s)));
            //else
            //    return GetSeparatedString(sqlResult.RowValues.Select(s => StringUtil.EncloseCsvFieldValueIfSpeicalCharExists(s)));
        }

        string GetSeparatedString(IEnumerable<string> values)
        {
            return String.Join(UserPreference.Reflector.CsvFieldSeparetor, values);
        }

        //string GetEnclosedFiledValue(string value)
        //{
        //    if (value == null)
        //        return value;

        //    string escaptedString = value.Replace(Constants.StringDoubleQuotation, _escapeSequence);

        //    return UserPreference.Reflector.GetEnclosedCsvField(escaptedString);
        //}
    }
}
