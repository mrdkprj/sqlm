using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class EnglishInfoMessageProvider : IInfoMessageProvider
    {
        static EnglishInfoMessageProvider _instance = new EnglishInfoMessageProvider();
        static EnglishInfoMessageProvider() { }
        private EnglishInfoMessageProvider() { }
        public static EnglishInfoMessageProvider Instance { get { return _instance; } }

        public string EditResultAwaitDialogProcessing { get { return "Retrieving data..."; } }
        public string EditResultAwaitDialogCancellingMessage { get { return "Cancelling..."; } }
        public string EditResultDiscardChanges { get { return "Discard these changes?"; } }
        public string EditResultApplyChanges { get { return "Do you save these changes?"; } }
        public string EditResultRecordCountFormat { get { return "{0} records"; } }
        public string EditResultApplyComplete { get { return "Complete"; } }
        public string ExportStatusTextFormat { get { return "{0} records exported"; } }
        public string ImportStatusTextFormat { get { return "{0} records imported"; } }
        public string Preparing { get { return "Preparing..."; } }
        public string Cancelling { get { return "Cancelling..."; } }
        public string MainMenuRecordCountFormat { get { return "Record count [ {0} ]"; } }
        public string MainMenuInvalidSql { get { return "Error"; } }
        public string CloseTab { get { return "Close this tab?"; } }
        public string ClearLogView { get { return "Clear SQL results and log?"; } }
    }
}
