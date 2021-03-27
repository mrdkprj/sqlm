using MasudaManager.DataAccess;
using MasudaManager.Views;
using System;
using System.ComponentModel;
using WinFormsMvp;
using WinFormsMvp.Binder;
using WinFormsMvp.Messaging;

namespace MasudaManager.Presenters
{
    public class LogOnPresenter : Presenter<ILogOnView>
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        public LogOnPresenter(ILogOnView view)
            : base(view)
        {
            View.Model = new LogOnModel();
            RegisterHandlers();
        }

        #region EventHandlers

        #region RegisterHandlers
        void RegisterHandlers()
        {
            View.Initiated += View_Initiated;
            View.OkButtonClicked += View_OkButtonClicked;
            View.CancelButtonClicked += View_CancelButtonClicked;
            View.ReleaseRequested += View_ReleaseRequested;
        }
        #endregion

        #region Initiate/Release/ShutDown
        void View_Initiated(object sender, GenericEventArgs<object> e)
        {
            PrepareShowView();
        }

        void View_ReleaseRequested(object sender, CancelEventArgs e)
        {
        }
        #endregion

        #region OK button click
        private void View_OkButtonClicked(object sender, EventArgs e)
        {
            if (!TryConnect())
                return;

            View.CloseView();
        }
        #endregion

        #region Cancel button click
        private void View_CancelButtonClicked(object sender, EventArgs e)
        {
            View.CloseView();
        }
        #endregion

        #endregion

        #region Messaging
        void NotifyConnectionChanged()
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<object>(this)
                , PresenterTokens.ConnectionChangedToken);
        }
        #endregion

        #region Methods

        #region PrepareView
        void PrepareShowView()
        {
            View.CreateModeCombobox(_dataAccess.ModeList);

            SetInitialConnectionData();

            View.ShowModal();
        }

        void SetInitialConnectionData()
        {
            View.Model.InitialConnectionData = _dataAccess.CurrentConnectionData;

            View.DataSource = View.Model.InitialConnectionData.DataSource;
            View.UserId = View.Model.InitialConnectionData.UserId;
            View.Password = View.Model.InitialConnectionData.Password;
            View.Mode = View.Model.InitialConnectionData.Mode;
        }
        #endregion

        #region Lock/Release View
        void LockView()
        {
            View.OkButtonEnabled = false;
        }

        void ReleaseView()
        {
            View.OkButtonEnabled = true;
        }
        #endregion

        #region Try connect
        bool TryConnect()
        {
            LockView();

            View.Model.NewConnectionData = GetInputConnectionData();

            try
            {
                _dataAccess.TryConnect(View.Model.NewConnectionData);
                RenewConnection();
                return true;
            }
            catch (Exception ex)
            {
                View.ShowMessage(ex.Message);
                return false;
            }
            finally
            {
                ReleaseView();
            }
        }

        ConnectionData GetInputConnectionData()
        {
            ConnectionData connectionData = new ConnectionData();
            connectionData.DataSource = View.DataSource;
            connectionData.UserId = View.UserId;
            connectionData.Password = View.Password;
            connectionData.Mode = View.Mode.ToStringOrNull();

            return connectionData;
        }
       
        void RenewConnection()
        {
            if (!RenewConnectionRequired())
                return;

            _dataAccess.Connect(View.Model.NewConnectionData);

            NotifyConnectionChanged();
        }

        bool RenewConnectionRequired()
        {
            if (!View.Model.InitialConnectionData.IsConnected)
                return true;

            if (!View.Model.InitialConnectionData.Equals(View.Model.NewConnectionData))
                return true;

            return false;
        }       
        #endregion

        #endregion
    }
}
