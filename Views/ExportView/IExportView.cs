using System;
using System.ComponentModel;
using System.Windows.Forms;
using WinFormsMvp;

namespace MasudaManager.Views
{
    public interface IExportView : IView<ExportModel>
    {
        event EventHandler<GenericEventArgs<ExportImportParameter>> Initiated;
        event EventHandler ExportFileTextChanged;
        event EventHandler FileSelectButtonClicked;
        event EventHandler ExportFormatChanged;
        event EventHandler ExportButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler<CancelEventArgs> ViewClosing;
        event EventHandler<CancelEventArgs> ReleaseRequested;

        bool CancelButtonEnabled { get; set; }
        CsvHeaderOption CsvExportHeaderOption { get; }
        bool ExportButtonEnabled { get; set; }
        string ExportFileText { get; set; }
        bool ExportFileTextBoxEnabled { get; set; }
        ExportImportFormat ExportFormat { get; }
        bool ExportFormatListEnabled { get; set; }
        bool FileSelectButtonEnabled { get; set; }
        bool ProgressBarVisible { get; set; }
        bool SqlInputViewEnabled { get; set; }
        string SqlInputViewText { get; set; }

        void CloseView();
        void DisableHeaderOptions();
        void EnableHeaderOptions();
        void FocusOnExportFileText();
        void InitializeOpenFileDialog(string InitialDirectory, string filterText, int filterIndex);
        void SetExportFormatDatasource(object datasource);
        void SetExportTableName(string tableName);
        void SetOpenFileDialogFilterText(string filterText);
        void SetProgressBarMaxValue(int maxValue);
        void SetProgressBarValue(int progress);
        void SetStatusLabelText(string status);
        void SetStatusToolTipText(string text);
        DialogResult ShowMessage(string message);
        void ShowModal();
        string ShowOpenFileDialog();
    }
}
