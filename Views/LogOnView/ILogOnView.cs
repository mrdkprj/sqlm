using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using WinFormsMvp;

namespace MasudaManager.Views
{
    public interface ILogOnView : IView<LogOnModel>
    {
        event EventHandler<GenericEventArgs<object>> Initiated;
        event EventHandler OkButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler<CancelEventArgs> ReleaseRequested;

        string DataSource { get; set; }
        string UserId { get; set; }
        string Password { get; set; }
        object Mode { get; set; }
        bool OkButtonEnabled { get; set; }

        void CloseView();
        void CreateModeCombobox(IEnumerable<string> modeList);
        DialogResult ShowMessage(string message);
        void ShowModal();
    }
}
