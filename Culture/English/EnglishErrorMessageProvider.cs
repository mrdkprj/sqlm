using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class EnglishErrorMessageProvider : IErrorMessageProvider
    {
        static EnglishErrorMessageProvider _instance = new EnglishErrorMessageProvider();
        static EnglishErrorMessageProvider() { }
        private EnglishErrorMessageProvider() { }
        public static EnglishErrorMessageProvider Instance { get { return _instance; } }

        public string ExportFileNotSpecified { get { return "Specify the export file"; } }
        public string InvalidFilePath { get { return "Invalid file path"; } }
        public string ImportFileNotSpecified { get { return "Specify the import file"; } }
        public string ImportFileNotFound { get { return "Import file not found"; } }
        public string NoDataToImport { get { return "No data to import"; } }
        public string ImportColumnCountNotMatched { get { return "CSV column count does not match table column count"; } }
        public string InvalidNumberOfSql { get { return "Invalid number of SQL"; } }
        public string InvalidSql { get { return "Invalid SQL"; } }
        public string TableNameNotFound { get { return "Cannot get the table name"; } }
        public string InvalidColumnIncluded { get { return "Invalid column included"; } }
        public string PrimaryKeyNotFound { get { return "Cannot get the Primary Key information"; } }
        public string PrimaryKeyNotIncluded { get { return "Primary key column is not included"; } }
    }
}
