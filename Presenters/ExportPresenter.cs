using MasudaManager.DataAccess;
using MasudaManager.Utility;
using MasudaManager.Views;
using System;
using System.ComponentModel;
using System.IO;
using WinFormsMvp;

namespace MasudaManager.Presenters
{
    public class ExportPresenter : Presenter<IExportView>, IObserver
    {
        readonly int _progressBarInitialValue = 0;
        readonly int _progressBarMaxValue = 100;
        readonly string _defaultStatusText = string.Empty;
        readonly string _csvFileFilter = "CSV Files(*.csv)|*.csv|All Files (*.*)|*.*";
        readonly string _sqlFileFilter = "SQL Files(*.sql)|*.sql|All Files (*.*)|*.*";
        readonly string _allFileFilter = "All Files (*.*)|*.*";
        readonly int _fileterIndex = 1;

        IExportStrategy _exportStrategy;
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        #region Constructor
        public ExportPresenter(IExportView view)
            : base(view)
        {
            View.Model = new ExportModel();
            RegisterHandlers();
        }

        #endregion

        #region EventHandlers

        #region RegisterHandlers
        void RegisterHandlers()
        {
            View.Initiated += View_Initiated;
            View.ExportFileTextChanged += View_ExportFileTextChanged;
            View.FileSelectButtonClicked += View_FileSelectButtonClicked;
            View.ExportFormatChanged += View_ExportFormatChanged;
            View.ExportButtonClicked += View_ExportButtonClicked;
            View.CancelButtonClicked += View_CancelButtonClicked;
            View.ViewClosing += View_ViewClosing;
            View.ReleaseRequested += View_ReleaseRequested;
        } 
        #endregion

        #region Initiated
        void View_Initiated(object sender, GenericEventArgs<ExportImportParameter> e)
        {
            PrepareShowView(e.Data);
        }
        #endregion

        #region ViewClosing
        void View_ViewClosing(object sender, CancelEventArgs e)
        {
            OnViewClosing(e);
        }
        #endregion

        #region Release requested
        void View_ReleaseRequested(object sender, CancelEventArgs e)
        {
            OnViewClosing(e);
        }
        #endregion

        #region Export file text changed
        void View_ExportFileTextChanged(object sender, EventArgs e)
        {
            View.Model.ExportFilePath = View.ExportFileText;
        }
        #endregion

        #region FileSelect button clicked
        void View_FileSelectButtonClicked(object sender, EventArgs e)
        {
            View.ExportFileText = View.ShowOpenFileDialog();
            View.FocusOnExportFileText();
        }
        #endregion

        #region Export format changed
        void View_ExportFormatChanged(object sender, EventArgs e)
        {
            OnExportFormatChanged();
        }
        #endregion

        #region Cancel button clicked
        void View_CancelButtonClicked(object sender, EventArgs e)
        {
            CancelExport();
        }
        #endregion

        #region Export buttion clicked
        void View_ExportButtonClicked(object sender, EventArgs e)
        {
            TryExport();
        }
        #endregion

        #endregion

        #region Methods

        #region PrepareShowView
        void PrepareShowView(ExportImportParameter paremeter)
        {
            if (String.IsNullOrEmpty(paremeter.TableName))
                PrepareSqlExport(paremeter);
            else
                PrepareTableDataExport(paremeter);

            View.Model.ExportTableName = paremeter.TableName;
            View.SetExportTableName(paremeter.TableName);
            View.ExportFileText = null;
            View.SetExportFormatDatasource(Enum.GetValues(typeof(ExportImportFormat)));
            View.InitializeOpenFileDialog(View.Model.ExportFilePath.ToStringOrNull(), GetOpenFileDialogFilterText(), _fileterIndex);
            View.SetProgressBarMaxValue(_progressBarMaxValue);
            View.SetStatusLabelText(_defaultStatusText);
            View.FocusOnExportFileText();

            ReleaseView();

            View.ShowModal();
        }

        void PrepareTableDataExport(ExportImportParameter paremeter)
        {
            View.Model.ExportSql = _dataAccess.SqlLibrary.FormatSelectAllFromTable(paremeter.TableName);
            View.SqlInputViewText = string.Empty;
            View.SqlInputViewEnabled = false;
            View.ExportFormatListEnabled = true;
        }

        void PrepareSqlExport(ExportImportParameter paremeter)
        {
            View.Model.ExportSql = paremeter.Sql;
            View.SqlInputViewText = paremeter.Sql;
            View.SqlInputViewEnabled = true;
            View.ExportFormatListEnabled = false;
        }
        #endregion
        
        #region OnViewClosing
        void OnViewClosing(CancelEventArgs e)
        {
            if (_exportStrategy == null)
                return;

            if (!_exportStrategy.IsBusy)
                return;

            CancelExport();

            e.Cancel = true;
        }
        #endregion

        #region OnExportFormatChanged
        void OnExportFormatChanged()
        {
            if (View.ExportFormat == ExportImportFormat.CSV)
                View.EnableHeaderOptions();
            else
                View.DisableHeaderOptions();

            View.SetOpenFileDialogFilterText(GetOpenFileDialogFilterText());
        }
        #endregion

        #region TryExport
        void TryExport()
        {
            try
            {
                ThrowIfExportFileInvalid();
                PrepareExport();
                Export();
            }
            catch (Exception ex)
            {
                View.ShowMessage(ex.Message);
            }
        }
        #endregion

        #region ThrowIfExportFileInvalid
        void ThrowIfExportFileInvalid()
        {
            if (String.IsNullOrEmpty(View.Model.ExportFilePath))
                throw new Exception(LocalizedTextProvider.Message.Error.ExportFileNotSpecified);
            
            if (!Directory.Exists(Path.GetDirectoryName(View.Model.ExportFilePath)))
                throw new DirectoryNotFoundException(LocalizedTextProvider.Message.Error.InvalidFilePath);
        }
        #endregion

        #region PrepareExport
        void PrepareExport()
        {
            LockView();

            if (String.IsNullOrEmpty(View.Model.ExportTableName))
                View.Model.ExportSql = View.SqlInputViewText;

            View.Model.CsvExportHeaderOption = View.CsvExportHeaderOption;
            View.Model.ExportFormat = View.ExportFormat;
            View.SetProgressBarValue(_progressBarInitialValue);
            View.SetStatusLabelText(LocalizedTextProvider.Message.Info.Preparing);
        }
        #endregion

        #region Export
        async void Export()
        {
            if (View.ExportFormat == ExportImportFormat.CSV)
                _exportStrategy = new ExportCsvBySqlTask(View.Model);
            else
                _exportStrategy = new ExportSqlBySqlTask(View.Model);

            _exportStrategy.Attach(this);

            await _exportStrategy.Export();
        }
        #endregion

        #region CancelExport
        void CancelExport()
        {
            if (_exportStrategy == null || !_exportStrategy.IsBusy)
                return;

            _exportStrategy.Cancel();
        }
        #endregion

        #region Lock/Release View
        void LockView()
        {
            View.ExportFileTextBoxEnabled = false;
            View.FileSelectButtonEnabled = false;
            View.ExportFormatListEnabled = false;
            View.DisableHeaderOptions();
            View.ExportButtonEnabled = false;
            View.CancelButtonEnabled = true;
            View.ProgressBarVisible = true;
        }
        
        void ReleaseView()
        {
            View.ExportFileTextBoxEnabled = true;
            View.FileSelectButtonEnabled = true;
            View.ExportFormatListEnabled = true;
            if (View.ExportFormat == ExportImportFormat.CSV)
                View.EnableHeaderOptions();
            else
                View.DisableHeaderOptions();
            View.ExportButtonEnabled = true;
            View.CancelButtonEnabled = false;
            View.ProgressBarVisible = false;
        }
        #endregion

        #region GetOpenFileDialogFilterText
        string GetOpenFileDialogFilterText()
        {
            switch (View.ExportFormat)
            {
                case ExportImportFormat.CSV:
                    return _csvFileFilter;
                case ExportImportFormat.SQL:
                    return _sqlFileFilter;
                default:
                    return _allFileFilter;
            }
        }
        #endregion

        #region Observer
        public void Update(object sender)
        {
            View.SetStatusLabelText(String.Format(LocalizedTextProvider.Message.Info.ExportStatusTextFormat, _exportStrategy.ExportCount));
            View.SetProgressBarValue(_exportStrategy.ExportProgress);
        }

        public void Complete(object sender)
        {
            View.ShowMessage(String.Format(LocalizedTextProvider.Message.Info.ExportStatusTextFormat, _exportStrategy.ExportCount));
            ReleaseView();
            View.CloseView();
        }

        public void Error(object sender, Exception e)
        {
            View.ShowMessage(e.Message);
            if (e.GetType().Equals(typeof(OperationCanceledException)))
                View.SetStatusLabelText(String.Format(LocalizedTextProvider.Message.Info.ExportStatusTextFormat, _exportStrategy.ExportCount));
            else
                View.SetStatusLabelText(e.Message);
            View.SetStatusToolTipText(e.Message);
            ReleaseView();
        }
        #endregion

        #endregion
    }
}
