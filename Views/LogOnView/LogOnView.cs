using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MasudaManager.Views
{
    public partial class LogOnView : LogOnViewSlice, ILogOnView, IChildView<object>
    {
        IWin32Window _owner;

        public event EventHandler<GenericEventArgs<object>> Initiated;
        public event EventHandler OkButtonClicked;
        public event EventHandler CancelButtonClicked;
        public event EventHandler<CancelEventArgs> ReleaseRequested;

        public LogOnView()
        {
            InitializeComponent();
            PrepareFormText();

            SetTabIndex();
        }

        public bool SuppressFormClosingEvent { get; set; }
        public Action ViewClosedAction { get; set; }

        #region EventHandlers
        
        #region OK button click
        private void btOK_Click(object sender, EventArgs e)
        {
            OkButtonClicked(sender, e);
        }
        #endregion

        #region Cancel button click
        private void btCancel_Click(object sender, EventArgs e)
        {
            CancelButtonClicked(sender, e);
        }
        #endregion

        #region ProcessDialogKey
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.txtID.Focused || this.txtPassword.Focused || this.txtDS.Focused)
                {
                    return this.ProcessDialogKey(Keys.Tab);
                }
            }
            else if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #endregion

        #region Methods

        #region PrepareFormText
        void PrepareFormText()
        {
            this.Text = LocalizedTextProvider.Form.Connect;
            this.lblDataSource.Text = LocalizedTextProvider.Form.DataSource;
            this.lblUserId.Text = LocalizedTextProvider.Form.UserId;
            this.lblPassword.Text = LocalizedTextProvider.Form.Password;
            this.lblMode.Text = LocalizedTextProvider.Form.LogOnMode;
            this.btOK.Text = LocalizedTextProvider.Form.OK;
            this.btCancel.Text = LocalizedTextProvider.Form.Cancel;
        }
        #endregion

        #region SetTabIndex
        void SetTabIndex()
        {
            this.txtDS.TabIndex = 0;
            this.txtID.TabIndex = 1;
            this.txtPassword.TabIndex = 2;
            this.cmbMode.TabIndex = 4;
            this.cmbMode.TabStop = false;
            this.btOK.TabIndex = 4;
            this.btCancel.TabIndex = 5;
        }
        #endregion

        #region Initiate
        public void Initiate(IWin32Window owner, GenericEventArgs<object> args)
        {
            _owner = owner;
            Initiated(owner, args);
        }
        #endregion

        #region ShowModal
        public void ShowModal()
        {
            this.ShowDialog(_owner);
            this.ViewClosedAction.Invoke();
        }
        #endregion

        #region CloseView
        public void CloseView()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.Close()));
            else
                this.Close();
        }      
        #endregion

        #region Release
        public void Release(object sender, CancelEventArgs e)
        {
            ReleaseRequested(sender, e);
        }
        #endregion

        #region ShowMessage
        public DialogResult ShowMessage(string message)
        {
            return MessageBox.Show(message);
        }
        #endregion
       
        #region Enable/disable
        public bool OkButtonEnabled
        {
            get { return this.btOK.Enabled; }
            set { this.btOK.Enabled = value; }
        }
        #endregion
        
        #region TextBox property
        public string DataSource
        {
            get { return this.txtDS.Text; }
            set { this.txtDS.Text = value; }
        }

        public string UserId
        {
            get { return this.txtID.Text; }
            set { this.txtID.Text = value; }
        }

        public string Password
        {
            get { return this.txtPassword.Text; }
            set { this.txtPassword.Text = value; }
        }

        public object Mode
        {
            get { return this.cmbMode.SelectedItem; }
            set { this.cmbMode.SelectedItem = value; }
        }
        #endregion       

        #region CreateModeCombobox
        public void CreateModeCombobox(IEnumerable<string> modeList)
        {
            this.cmbMode.DataSource = modeList;
        }
        #endregion

        #endregion
    }

}