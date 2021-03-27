using System;
using System.Windows.Forms;

namespace MasudaManager.Views
{
    public interface IProgressView
    {
        event EventHandler CancelButtonClicked;
        string Caption { get; set; }
        bool CloseOnCancel { get; set; }
        int MaxProgressValue { get; set; }
        int MixProgressValue { get; set; }
        int ProgressBarValue { get; set; }
        string StatusLabelText { get; set; }
        ProgressBarStyle Style { get; set; }

        void ShowView(IWin32Window owner);
        void CloseView();
        void Start();
    }
}
