using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class EnglishTextProvider : ITextProvider
    {
        static EnglishTextProvider _instance = new EnglishTextProvider();
        static EnglishTextProvider() { }
        private EnglishTextProvider() { }
        public static EnglishTextProvider Instance { get { return _instance; } }

        public string Language
        {
            get { return "English"; }
        }

        public IContextMenuTextProvider ContextMenu
        {
            get { return EnglishContextMenuTextProvider.Instance; }
        }

        public IFormTextProvider Form
        {
            get { return EnglishFormTextProvider.Instance; }
        }

        public IMessageTextProvider Message
        {
            get { return EnglishMessageTextProvider.Instance; }
        }
    }
}
