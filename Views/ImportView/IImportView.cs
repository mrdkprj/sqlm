using System;
using System.ComponentModel;
using System.Windows.Forms;
using WinFormsMvp;

namespace MasudaManager.Views
{
    public interface IImportView : IView<ImportModel>
    {
        event EventHandler<GenericEventArgs<ExportImportParameter>> Initiated;
        event EventHandler ImportFileTextChanged;
        event EventHandler FileSelectButtonClicked;
        event EventHandler ImportFormatChanged;
        event EventHandler ImportButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler<CancelEventArgs> ViewClosing;
        event EventHandler<CancelEventArgs> ReleaseRequested;

        bool CancelButtonEnabled { get; set; }
        CsvHeaderOption CsvImportHeaderOption { get; }
        bool FileSelectButtonEnabled { get; set; }
        bool ImportButtonEnabled { get; set; }
        string ImportFileText { get; set; }
        bool ImportFormatListEnabled { get; set; }
        ExportImportFormat ImportFormat { get; }
        bool ProgressBarVisible { get; set; }
        bool ImportFileTextBoxEnabled { get; set; }
       
        void CloseView();
        void DisableHeaderOptions();
        void EnableHeaderOptions();
        void FocusOnImportFileText();
        void InitializeOpenFileDialog(string InitialDirectory, string filterText, int filterIndex);
        void SetImportTableName(string tableName);
        void SetImportFormatDatasource(object datasource);
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
