using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WinFormsMvp;

namespace MasudaManager.Views
{
    public interface IMainMenuView : IView<MainMenuModel>
    {
        event EventHandler ComponentInitialized;
        event EventHandler<CancelEventArgs> ViewClosing;
        event EventHandler ConnectButtonClicked;
        event EventHandler DisconnectButtonClicked;
        event EventHandler AddTabButtonClicked;
        event EventHandler OpenButtonClicked;
        event EventHandler SaveButtonClicked;
        event EventHandler ExportButtonClicked;
        event EventHandler ExecuteButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler SearchButtonClicked;
        event EventHandler EditResultButtonClicked;
        event EventHandler PreferenceButtonClicked;

        bool ConnectButtonEnabled { get; set; }
        bool DisconnectButtonEnabled { get; set; }
        bool AddTabButtonEnabled { get; set; }
        bool ExecuteButtonEnabled { get; set; }
        bool CancelButtonEnabled { get; set; }
        bool ExportButtonEnabled { get; set; }
        bool SearchButtonEnabled { get; set; }
        bool EditResultButtonEnabled { get; set; }
        bool PreferenceButtonEnabled { get; set; }
        bool ProgressBarVisible { get; set; }

        Rectangle CurrentClientSize { get; set; }
        FormWindowState CurrentWindowState { get; set; }
        string FormText { get; set; }
        string GetActiveInputViewFilePath();
        string GetActiveInputViewText();
        IWin32Window GetChildViewOwner();
        string GetDbObjectInfoViewSelectedObjectName();
        ISynchronizeInvoke GetInvoker();
        string GetOpenFilePath();
        string GetSaveFilePath();
        void InitializeSaveFileDialog(string filterText, int filterIndex);
        void InitializeOpenFileDialog(string filterText, int filterIndex);       
        void SetStatusLabelText(string text);
        void SetStatusLabelColor(Color color);
        void SetInfomationLabelText(string information);
        DialogResult ShowMessageBox(string message);
        DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons button, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton);
        DialogResult ShowErrorMessageBox(string message, string caption);
    }
}
