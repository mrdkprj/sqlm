using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    public interface IContextMenuTextProvider
    {
        string Cut { get; }
        string Copy { get; }
        string Paste { get; }
        string CopyFromObjectView { get; }
        string CopyToObjectViewFilter { get; }
        string CopyToPropertyViewFilter { get; }
        string ZoomIn { get; }
        string ZoomOut { get; }
        string ResetZoom { get; }
        string AdjustHeaderWidth { get; }
        string CopyText { get; }
        string CopyHeader { get; }
        string CopyTextWithHeader { get; }
        string Edit { get; }
        string ClearLog { get; }
        string SwitchView { get; }
        string DisplayData { get; }
        string CreateSql { get; }
        string Export { get; }
        string Import { get; }
        string AddRow { get; }
        string AddRows { get; }
        string DeleteRow { get; }
        string CloseTab { get; }
    }
}
