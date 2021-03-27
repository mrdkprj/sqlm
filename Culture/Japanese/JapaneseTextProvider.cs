using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class JapaneseTextProvider : ITextProvider
    {
        static JapaneseTextProvider _instance = new JapaneseTextProvider();
        static JapaneseTextProvider() { }
        private JapaneseTextProvider() { }
        public static JapaneseTextProvider Instance { get { return _instance; } }

        public string Language
        {
            get { return "Japanese"; }
        }

        public IContextMenuTextProvider ContextMenu
        {
            get { return JapaneseContextMenuTextProvider.Instance; }
        }

        public IFormTextProvider Form
        {
            get { return JapaneseFormTextProvider.Instance; }
        }

        public IMessageTextProvider Message
        {
            get { return JapaneseMessageTextProvider.Instance; }
        }
    }
}
