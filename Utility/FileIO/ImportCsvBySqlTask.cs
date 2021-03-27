using MasudaManager.Utility.Preference;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace MasudaManager.Utility
{
    public class ImportCsvBySqlTask : ImportBySqlTask
    {
        readonly FieldType _fieldType = FieldType.Delimited;
        string[] _fields;
        int _recordFieldCount = 0;

        public ImportCsvBySqlTask(ImportModel importModel) : base(importModel) { }

        protected override IDbCommandBuilder GetInsertCommands(CancellationToken token)
        {
            SchemaTableSqlService _sqlService = new SchemaTableSqlService();
            IDmlDbCommandBuilder _builder = new InsertDbCommandBuilder();

            SqlResult importTableColumnInfo = _sqlService.GetColumnInfoFromTable(this.ImportModel.ImportTableName).FirstOrDefault();

            foreach (IEnumerable<string> csvRecord in GetCsvRecords(this.ImportModel.ImportFilePath, UserPreference.Reflector.CsvFieldSeparetor, UserPreference.Reflector.IOEncoding))
            {
                token.ThrowIfCancellationRequested();

                //if (csvRecord.Count() != importTableColumnInfo.ColumnNames.Count)
                if (_recordFieldCount != importTableColumnInfo.ColumnNames.Count)
                    throw new Exception(LocalizedTextProvider.Message.Error.ImportColumnCountNotMatched);

                importTableColumnInfo.RowValues = new List<string>(csvRecord);
                _builder.CreateCommand(this.ImportModel.ImportTableName, importTableColumnInfo);
            }

            if (_builder.Count <= 0)
                throw new Exception(LocalizedTextProvider.Message.Error.NoDataToImport);
            else
                SetTotalCount(_builder.Count);

            return _builder;
        }

        IEnumerable<IEnumerable<string>> GetCsvRecords(string path, string separator, Encoding encoding)
        {
            using (var stream = new FileStream(path, this.FileMode, this.FileAccess, UserPreference.Reflector.FileShareMode))
            {
                using (var textFiledParser = new TextFieldParser(stream, encoding, true, false))
                {
                    textFiledParser.TextFieldType = _fieldType;
                    textFiledParser.Delimiters = new string[] { separator };
                    textFiledParser.HasFieldsEnclosedInQuotes = UserPreference.Setting.File.EncloseCsvFields;
                    textFiledParser.TrimWhiteSpace = false;

                    if (this.ImportModel.CsvImportHeaderOption == CsvHeaderOption.ColumnNameHeader)
                        SkipHeader(textFiledParser);

                    while (!textFiledParser.EndOfData)
                    {
                        _fields = textFiledParser.ReadFields();
                        _recordFieldCount = _fields.Length;
                        yield return ParseStringFields(_fields);
                    }
                }
            }
        }

        IEnumerable<string> ParseStringFields(string[] fields)
        {
            foreach (string field in fields)
            {
                if (String.IsNullOrEmpty(field))
                    yield return null;
                else
                    yield return field;
            }
        }

        void SkipHeader(TextFieldParser textFiledParser)
        {
            if (textFiledParser.EndOfData)
                return;

            textFiledParser.ReadLine();
        }
    }
}
