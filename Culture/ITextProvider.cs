using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Culture;

namespace MasudaManager
{
    public interface ITextProvider
    {
        string Language { get; }
        IContextMenuTextProvider ContextMenu { get; }
        IFormTextProvider Form { get; }
        IMessageTextProvider Message { get; }
    }
}
