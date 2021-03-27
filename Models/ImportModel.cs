using System;

namespace MasudaManager
{
    public class ImportModel : IModel
    {
        public string ImportTableName { get; set; }
        public ExportImportFormat ImportFormat { get; set; }
        public CsvHeaderOption CsvImportHeaderOption { get; set; }
        public string ImportFilePath { get; set; }

        public void ReleaseModel()
        {
        }
    }
}
