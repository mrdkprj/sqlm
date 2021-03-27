using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    public interface IErrorMessageProvider
    {
        string ExportFileNotSpecified { get; }
        string InvalidFilePath { get; }
        string ImportFileNotSpecified { get; }
        string ImportFileNotFound { get; }
        string NoDataToImport { get; }
        string ImportColumnCountNotMatched { get; }
        string InvalidNumberOfSql { get; }
        string InvalidSql { get; }
        string TableNameNotFound { get; }
        string InvalidColumnIncluded { get; }
        string PrimaryKeyNotFound { get; }
        string PrimaryKeyNotIncluded { get; }
    }
}
