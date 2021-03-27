using System.Windows.Forms;

namespace MasudaManager.Controls
{
    public partial class XRichTextBox : RichTextBox
    {

        public XRichTextBox()
        {
            InitializeComponent();
        }

        public bool IsOverwriteMode { get; set; }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.V:
                    this.Paste(DataFormats.GetFormat(DataFormats.UnicodeText));
                    e.Handled = true;
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
    }
}
