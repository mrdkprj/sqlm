using System.Windows.Forms;

namespace MasudaManager
{
    public static class ClipboardExtensions
    {
        public static string GetTextOrNull()
        {
            if (!Clipboard.ContainsText(TextDataFormat.UnicodeText))
                return null;

            try
            {
                return Clipboard.GetText(TextDataFormat.UnicodeText);
            }
            catch
            {
                return null;
            }
        }
    }
}
