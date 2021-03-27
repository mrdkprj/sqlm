using MasudaManager.DataAccess;
using MasudaManager.Utility;
using MasudaManager.Views;
using System;
using System.ComponentModel;
using System.IO;
using WinFormsMvp;

namespace MasudaManager.Presenters
{
    public class ImportPresenter : Presenter<IImportView>, IObserver
    {
        readonly int _progressBarInitialValue = 0;
        readonly int _progressBarMaxValue = 100;
        readonly string _defaultStatusText = string.Empty;
        readonly string _csvFileFilter = "CSV Files(*.csv)|*.csv|All Files (*.*)|*.*";
        readonly string _sqlFileFilter = "SQL Files(*.sql)|*.sql|All Files (*.*)|*.*";
        readonly string _allFileFilter = "All Files (*.*)|*.*";
        readonly int _fileterIndex = 1;

        IImportStrategy _importStrategy;
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        #region Constructor
        public ImportPresenter(IImportView view)
            : base(view)
        {
            View.Model = new ImportModel();
            RegisterHandlers();
        }
        #endregion

        #region EventHandlers

        #region RegisterHandlers
        void RegisterHandlers()
        {
            View.Initiated += View_Initiated;
            View.ImportFileTextChanged += View_ImportFileTextChanged;
            View.FileSelectButtonClicked += View_FileSelectButtonClicked;
            View.ImportFormatChanged += View_ImportFormatChanged;
            View.ImportButtonClicked += View_ImportButtonClicked;
            View.CancelButtonClicked += View_CancelButtonClicked;
            View.ViewClosing += View_ViewClosing;
            View.ReleaseRequested += View_ReleaseRequested;
        }
        #endregion

        #region Initiated
        void View_Initiated(object sender, GenericEventArgs<ExportImportParameter> e)
        {
            PrepareShowView(e.Data.TableName);
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

        #region ImportFileText changed
        void View_ImportFileTextChanged(object sender, EventArgs e)
        {
            View.Model.ImportFilePath = View.ImportFileText;
        }
        #endregion

        #region FileSelect button clicked
        void View_FileSelectButtonClicked(object sender, EventArgs e)
        {
            View.ImportFileText = View.ShowOpenFileDialog();
            View.FocusOnImportFileText();
        }
        #endregion

        #region Import format changed
        void View_ImportFormatChanged(object sender, EventArgs e)
        {
            OnImportFormatChanged();
        }
        #endregion
        
        #region Cancel button clicked
        void View_CancelButtonClicked(object sender, EventArgs e)
        {
            CancelImport();
        }
        #endregion

        #region Import buttion clicked
        void View_ImportButtonClicked(object sender, EventArgs e)
        {
            TryImport();
        }
        #endregion

        #endregion

        #region Methods

        #region PrepareShowView
        void PrepareShowView(string tableName)
        {
            View.Model.ImportTableName = tableName;
            View.SetImportTableName(View.Model.ImportTableName);
            View.ImportFileText = null;
            View.SetImportFormatDatasource(Enum.GetValues(typeof(ExportImportFormat)));
            View.InitializeOpenFileDialog(View.Model.ImportFilePath.ToStringOrNull(), GetOpenFileDialogFilterText(), _fileterIndex);
            View.SetProgressBarMaxValue(_progressBarMaxValue);
            View.SetStatusLabelText(_defaultStatusText);
            View.FocusOnImportFileText();

            ReleaseView();

            View.ShowModal();
        }
        #endregion

        #region OnViewClosing
        void OnViewClosing(CancelEventArgs e)
        {
            if (_importStrategy == null)
                return;

            if (!_importStrategy.IsBusy)
                return;

            CancelImport();

            e.Cancel = true;
        }
        #endregion

        #region OnImportFormatChanged
        void OnImportFormatChanged()
        {
            if (View.ImportFormat == ExportImportFormat.CSV)
                View.EnableHeaderOptions();
            else
                View.DisableHeaderOptions();

            View.SetOpenFileDialogFilterText(GetOpenFileDialogFilterText());
        }
        #endregion

        #region TryImport
        void TryImport()
        {
            try
            {
                ThrowIfImportFileInvalid();
                PrepareImport();
                Import();
            }
            catch (Exception ex)
            {
                View.ShowMessage(ex.Message);
            }
        }
        #endregion

        #region ThrowIfImportFileInvalid
        void ThrowIfImportFileInvalid()
        {
            if (String.IsNullOrEmpty(View.Model.ImportFilePath))
                throw new Exception(LocalizedTextProvider.Message.Error.ImportFileNotSpecified);

            if (!File.Exists(View.Model.ImportFilePath))
                throw new FileNotFoundException(LocalizedTextProvider.Message.Error.ImportFileNotFound);
        }
        #endregion

        #region PrepareImport
        void PrepareImport()
        {
            LockView();

            View.Model.CsvImportHeaderOption = View.CsvImportHeaderOption;
            View.Model.ImportFormat = View.ImportFormat;
            View.SetProgressBarValue(_progressBarInitialValue);
            View.SetStatusLabelText(LocalizedTextProvider.Message.Info.Preparing);
        }
        #endregion

        #region Import
        async void Import()
        {
            if (View.ImportFormat == ExportImportFormat.CSV)
                _importStrategy = new ImportCsvBySqlTask(View.Model);
            else
                _importStrategy = new ImportSqlBySqlTask(View.Model);

            _importStrategy.Attach(this);

            await _importStrategy.Import();
        }
        #endregion

        #region CancelImport
        void CancelImport()
        {
            if (_importStrategy == null || !_importStrategy.IsBusy)
                return;

            _importStrategy.Cancel();
        }
        #endregion

        #region Lock/Release View
        void LockView()
        {
            View.ImportFileTextBoxEnabled = false;
            View.FileSelectButtonEnabled = false;
            View.ImportFormatListEnabled = false;
            View.DisableHeaderOptions();
            View.ImportButtonEnabled = false;
            View.CancelButtonEnabled = true;
            View.ProgressBarVisible = true;
        }
        
        void ReleaseView()
        {
            View.ImportFileTextBoxEnabled = true;
            View.FileSelectButtonEnabled = true;
            View.ImportFormatListEnabled = true;
            if (View.ImportFormat == ExportImportFormat.CSV)
                View.EnableHeaderOptions();
            else
                View.DisableHeaderOptions();
            View.ImportButtonEnabled = true;
            View.CancelButtonEnabled = false;
            View.ProgressBarVisible = false;
        }
        #endregion

        #region GetOpenFileDialogFilterText
        string GetOpenFileDialogFilterText()
        {
            switch (View.ImportFormat)
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
            View.SetStatusLabelText(String.Format(LocalizedTextProvider.Message.Info.ImportStatusTextFormat, _importStrategy.ImportCount));
            View.SetProgressBarValue(_importStrategy.ImportProgress);
        }

        public void Complete(object sender)
        {
            View.ShowMessage(String.Format(LocalizedTextProvider.Message.Info.ImportStatusTextFormat, _importStrategy.ImportCount));
            ReleaseView();
            View.CloseView();
        }

        public void Error(object sender, Exception e)
        {
            View.ShowMessage(e.Message);
            View.SetStatusLabelText(e.Message);
            View.SetStatusToolTipText(e.Message);
            ReleaseView();
        }
        #endregion

        #endregion
    }
}
