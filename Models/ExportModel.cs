using System;

namespace MasudaManager
{
    public class ExportModel : IModel
    {
        public string ExportSql { get; set; }
        public string ExportTableName { get; set; }
        public ExportImportFormat ExportFormat { get; set; }
        public CsvHeaderOption CsvExportHeaderOption { get; set; }
        public string ExportFilePath { get; set; }

        public void ReleaseModel()
        {
        }
    }
}
