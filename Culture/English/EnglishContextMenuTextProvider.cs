using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class EnglishContextMenuTextProvider : IContextMenuTextProvider
    {
        static EnglishContextMenuTextProvider _instance = new EnglishContextMenuTextProvider();
        static EnglishContextMenuTextProvider() { }
        private EnglishContextMenuTextProvider() { }
        public static EnglishContextMenuTextProvider Instance { get { return _instance; } }

        public string Cut { get { return "Cut"; } }
        public string Copy { get { return "Copy"; } }
        public string Paste { get { return "Paste"; } }
        public string CopyFromObjectView { get { return "Copy from Object view"; } }
        public string CopyToObjectViewFilter { get { return "Copy to Object view filter"; } }
        public string CopyToPropertyViewFilter { get { return "Copy to Property view filter"; } }
        public string ZoomIn { get { return "Zoom in"; } }
        public string ZoomOut { get { return "Zoom out"; } }
        public string ResetZoom { get { return "Reset zoom"; } }
        public string AdjustHeaderWidth { get { return "Adjust header width"; } }
        public string CopyText { get { return "Copy text"; } }
        public string CopyHeader { get { return "Copy header"; } }
        public string CopyTextWithHeader { get { return "Copy text with header"; } }
        public string Edit { get { return "Edit"; } }
        public string ClearLog { get { return "Clear"; } }
        public string SwitchView { get { return "Switch View"; } }
        public string DisplayData { get { return "Display data"; } }
        public string CreateSql { get { return "Create SQL"; } }
        public string Export { get { return "Export"; } }
        public string Import { get { return "Import"; } }
        public string AddRow { get { return "Add row"; } }
        public string AddRows { get { return "Add rows"; } }
        public string DeleteRow { get { return "Delete row"; } }
        public string CloseTab { get { return "Close tab"; } }
    }
}
