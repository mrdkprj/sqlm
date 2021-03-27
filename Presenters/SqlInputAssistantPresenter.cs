using MasudaManager.Controls;
using MasudaManager.DataAccess;
using MasudaManager.Utility.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasudaManager.Utility
{
    public class SqlInputAssistantPresenter : IObserver
    {
        readonly int _columnIndex = 0;
        readonly ISqlService _queryService = new QuerySqlService();
        readonly ISqlService _schemaService = new SchemaTableSqlService();
        readonly SqlInputAssistant _inputAssistant = new SqlInputAssistant();

        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        ConnectionData _connectionData = new ConnectionData();
        Control _container;
        SqlInputAssistantSqlTask _sqlTask;
        SqlInputAssistantParser _inputAssistantParser = SqlInputAssistantParser.Instance;
        SqlInputView _inputView;
        List<string> _tableNameList = new List<string>();

        static SqlInputAssistantPresenter _instance = new SqlInputAssistantPresenter();
        static SqlInputAssistantPresenter() { }
        public static SqlInputAssistantPresenter Instance
        {
            get { return _instance; }
        }

        private SqlInputAssistantPresenter()
        {
            ConfigureSqlInputAssistant();
            PrepareTableNameList();
        }

        void PrepareTableNameList()
        {
            if (!_connectionData.Equals(_dataAccess.CurrentConnectionData))
            {
                _connectionData = _dataAccess.CurrentConnectionData;
                _tableNameList = _queryService.ExecuteSql(_dataAccess.SqlLibrary.FormatSelectTableNameFromSchema(_connectionData.UserId.ToUpper())).Select(s => s.RowValues).SelectMany(s => s.Select(i => i)).ToList();
            }
        }

        void ConfigureSqlInputAssistant()
        {
            _inputAssistant.ItemSelected += SqlInputAssistant_ItemSelected;
            _inputAssistant.Leave += SqlInputAssistant_Leave;
            _inputAssistant.KeyPress += SqlInputAssistant_KeyPress;
            _inputAssistant.BackSpaceKeyEnter += SqlInputAssistant_BackSpaceKeyEnter;
            _inputAssistant.EscapeKeyEnter += SqlInputAssistant_EscapeKeyEnter;
        }
       
        public void SetAssistantContainer(Control container)
        {
            if (_container != container)
            {
                _container = container;
                _inputAssistant.Parent = container;
                _sqlTask = new SqlInputAssistantSqlTask(_container, null);
                _sqlTask.Attach(this);
            }
        }

        public void SubscribeAssistant(SqlInputView inputView)
        {
            _inputView = inputView;
            _inputAssistant.SetOwnerView(inputView);
        }

        public async void ShowAssistant(string text, string inputWord, int startPosition)
        {
            if (_inputAssistant.Visible)
                return;

            if (!UserPreference.Setting.Input.UseAssistant)
                return;

            await GetSqlInputAssistantDataSource(text, inputWord, startPosition);
        }
        
        async Task GetSqlInputAssistantDataSource(string text, string inputWord, int startPosition)
        {
            _inputAssistantParser.Keyword = inputWord;
            _inputAssistantParser.InputPosition = startPosition;
            _inputAssistantParser.ObjectNames = _tableNameList;
            IDbCommandBuilder builder = _inputAssistantParser.Parse(text);

            if (builder != null)
            {
                PrepareTableNameList();

                if (_inputAssistantParser.ComplementMode == SqlInputAssistantComplementMode.InlineViewColumn)
                    _sqlTask.SqlService = _schemaService;
                else
                    _sqlTask.SqlService = _queryService;

                await _sqlTask.Run(builder, CancellationToken.None);
            }
        }

        void BindSqlInputAssistant()
        {
            using (_sqlTask)
            {
                _inputAssistant.DataSource = null;
                _inputAssistant.DataSource = _sqlTask.BindingModel;
                ChangeFocus();
            }
        }

        void ChangeFocus()
        {
            if (_sqlTask.BindingModel.Count > 0)
            {
                _inputAssistant.Visible = true;
                _inputAssistant.Focus();
                _inputAssistant.BringToFront();
            }
            else
            {
                //_inputAssistant.Visible = false;
                _inputView.Focus();
            }
        }

        #region Events

        void SqlInputAssistant_ItemSelected(object sender, EventArgs e)
        {
            int rowIndex = _inputAssistant.CurrentCell.RowIndex;
            string itemName = _sqlTask.SqlResults.ToList()[rowIndex].RowValues[_columnIndex];

            //_inputAssistant.Visible = false;
            _inputView.ReplaceSelection(UserPreference.Reflector.GetEnclosedPropertyValue(itemName));
            _inputView.Focus();
        }

        private void SqlInputAssistant_Leave(object sender, EventArgs e)
        {
            _inputAssistant.Visible = false;
            _sqlTask.Filter(string.Empty);
            _sqlTask.ClearFilter();
        }

        private void SqlInputAssistant_KeyPress(object sender, KeyPressEventArgs e)
        {
            _sqlTask.Filter(e.KeyChar.ToString());
            BindSqlInputAssistant();
            e.Handled = true;
        }

        void SqlInputAssistant_EscapeKeyEnter(object sender, EventArgs e)
        {
            //SqlInputAssistant_Leave(sender, e);
            _inputView.Focus();
        }

        void SqlInputAssistant_BackSpaceKeyEnter(object sender, EventArgs e)
        {
            _sqlTask.Filter(string.Empty);
            _sqlTask.ClearFilter();
            ChangeFocus();
        }
        
        #endregion

        #region Observer
        public void Update(object sender)
        {           
        }

        public void Complete(object sender)
        {
            BindSqlInputAssistant();
        }

        public void Error(object sender, Exception e)
        {
        }
        #endregion
    }
}
