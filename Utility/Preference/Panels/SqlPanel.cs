using System.ComponentModel;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility.Preference.Setting
{
    [ToolboxItem(false)]
    public partial class SqlPanel : PreferencePanelBase
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        public SqlPanel()
        {
            InitializeComponent();
            PrepareFormText();

            this.grpCommandTimeout.TabStop = false;
            this.grpCommandTimeout.TabIndex = MaxTabIndex;
            DisplayPreference();
        }

        void PrepareFormText()
        {
            this.chkDisplayProgress.Text = LocalizedTextProvider.Form.PrefDisplaySqlProgress;
            this.chkAutoCommit.Text = LocalizedTextProvider.Form.PrefAllowAutoCommit;
            this.chkRunAfterSemiColon.Text = LocalizedTextProvider.Form.PrefRunSqlOnEnter;
            this.chkIgnoreError.Text = LocalizedTextProvider.Form.PrefIgnoreError;
            this.grpCommandTimeout.Text = LocalizedTextProvider.Form.PrefTimeout;
            this.lblTimeoutSecond.Text = LocalizedTextProvider.Form.PrefTimeoutSecond;
        }

        public override void SetPanelItemTabIndex(int startIndex, int increment)
        {
            InitializeItemTabIndex(startIndex);
            this.chkDisplayProgress.TabIndex = GetIncrementedTabIndex(increment);
            this.chkAutoCommit.TabIndex = GetIncrementedTabIndex(increment);
            this.chkRunAfterSemiColon.TabIndex = GetIncrementedTabIndex(increment);
            this.chkIgnoreError.TabIndex = GetIncrementedTabIndex(increment);
            this.numTimeout.TabIndex = GetIncrementedTabIndex(increment);
        }
        
        public override void DisplayPreference()
        {
            this.chkDisplayProgress.Checked = UserPreference.Setting.Sql.DisplayProgress;
            this.chkAutoCommit.Checked = UserPreference.Setting.Sql.AllowAutoCommit;
            this.chkRunAfterSemiColon.Checked = UserPreference.Setting.Sql.RunAfterSemicolon;
            this.chkIgnoreError.Checked = UserPreference.Setting.Sql.ContinueAfterError;
            this.numTimeout.Value = UserPreference.Setting.Sql.CommandTimeout;
        }

        public override void RetrievePreference()
        {
            UserPreference.Setting.Sql.DisplayProgress = this.chkDisplayProgress.Checked;
            UserPreference.Setting.Sql.AllowAutoCommit = this.chkAutoCommit.Checked;
            UserPreference.Setting.Sql.RunAfterSemicolon = this.chkRunAfterSemiColon.Checked;
            UserPreference.Setting.Sql.ContinueAfterError = this.chkIgnoreError.Checked;
            UserPreference.Setting.Sql.CommandTimeout = (int)this.numTimeout.Value;
            _dataAccess.DefaultCommandTimeout = UserPreference.Setting.Sql.CommandTimeout;
        }
    }
}
