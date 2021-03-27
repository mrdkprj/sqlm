using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace MasudaManager.DataAccess.OracleDatabase
{
    public class OracleDataAccess : IDataAccess
    {
        //public event EventHandler ConnectionInactivated;

        readonly string _connectionString = "User Id={0}; Password={1}; Data Source={2}; DBA Privilege={3};";
        readonly string _defaultColumnSizeFormat = "({0})";
        readonly string _scaledColumnSizeFormat = "({0},{1})";
        readonly string _parameterString = ":";
        readonly string _sysSchemaName = "SYS";

        OracleConnection _activeConnection = null;
        OracleTransaction _activeTransaction = null;
        ISqlLibrary _sqlLibrary = new OracleSqlLibrary();
        ConnectionData _currentConnectionData = new ConnectionData();
        List<string> _modeList = new List<string>(new string[] { "", "SYSDBA", "SYSOPER" });
        DataTable _dataTable = new DataTable();
        int _defaultCommandTimeout = 30;

        #region Property
        public RdbmsType DatabaseType { get { return RdbmsType.Orace; } }
        public string DataDictioanryOwner { get { return _sysSchemaName; } }
        public IEnumerable<string> ModeList { get { return _modeList; } }
        public ConnectionData CurrentConnectionData { get { return _currentConnectionData; } }
        public IDbCommand DbCommand { get { return new OracleCommand(); } }
        public ISqlLibrary SqlLibrary { get { return _sqlLibrary; } }
        public IDataAccessConverter Converter { get { return OracleDataAccessConverter.GetInstance(); } }
        public int DefaultCommandTimeout
        {
            get { return _defaultCommandTimeout; }
            set { _defaultCommandTimeout = value; }
        }
        #endregion

        #region Parameter string
        public string GetParameterString()
        {
            throw new NotSupportedException("Oracle command required parameter with name or index");
        }

        public string GetParameterString(string parameterName)
        {
            return _parameterString + parameterName;
        }
        public string GetParameterString(int parameterIndex)
        {
            return _parameterString + parameterIndex;
        }
        #endregion

        #region Constructor
        public OracleDataAccess()
        {
        }
        #endregion

        #region Connect
        public bool Connect(ConnectionData connectionData)
        { 

            Disconnect();

            _activeConnection = new OracleConnection();

            try
            {
                _activeConnection.ConnectionString = String.Format(_connectionString, connectionData.UserId, connectionData.Password, connectionData.DataSource, connectionData.Mode);
                _activeConnection.Open();

                _currentConnectionData = connectionData;
                _currentConnectionData.IsConnected = true;
                return true;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetConnection
        OracleConnection GetConnection()
        {
            if (_activeConnection.State == ConnectionState.Open)
                return _activeConnection;

            OracleConnection connection = new OracleConnection(_currentConnectionData.ConnectionString);
            connection.Open();
            _activeConnection = connection;

            return connection;
        }
        #endregion

        #region TryConnect
        public bool TryConnect(ConnectionData connectionData)
        {
            using (var connection = new OracleConnection())
            {
                try
                {
                    connection.ConnectionString = String.Format(_connectionString, connectionData.UserId, connectionData.Password, connectionData.DataSource, connectionData.Mode);
                    connection.Open();
                    return true;
                }
                catch (OracleException ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Disconnect
        public bool Disconnect()
        {
            //if (_activeConnection == null || _activeConnection.State == ConnectionState.Closed)
            //    return true;
            if (_activeConnection == null)
                return true;

            try
            {
                RollbackTransaction();
                
                _activeConnection.Close();

                OracleConnection.ClearAllPools();

                _currentConnectionData.IsConnected = false;

                return true;
            }
            catch(OracleException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Query

        #region ExecuteQuerySql
        public DataTable ExecuteQuerySql(string sql)
        {
            return ExecuteQueryCommand(new OracleCommand(sql));
        }
        #endregion

        #region ExecuteDbDataReader
        public DbDataReader ExecuteDbDataReader(string sql)
        {
            using (var command = new OracleCommand(sql, GetConnection()))
            {
                try
                {
                    command.InitialLONGFetchSize = -1;
                    OracleDataReader reader = command.ExecuteReader();            
                    return reader;
                }
                catch (OracleException ex)
                {
                    throw ex;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }

        public DbDataReader ExecuteDbDataReader(IDbCommand command)
        {
            using (OracleCommand oracleCommand = (OracleCommand)command)
            {
                try
                {
                    oracleCommand.Connection = GetConnection();
                    //oracleCommand.Transaction = _transaction;
                    oracleCommand.InitialLONGFetchSize = -1;
                    oracleCommand.CommandType = CommandType.Text;
                    oracleCommand.Prepare();

                    OracleDataReader reader = oracleCommand.ExecuteReader();
                    return reader;
                }
                catch (OracleException ex)
                {
                    throw ex;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }
        #endregion   
        
        #region ExecuteQueryCommand
        public DataTable ExecuteQueryCommand(IDbCommand command)
        {
            using (OracleCommand oracleCommand = (OracleCommand)command)
            {
                try
                {
                    _dataTable = new DataTable();

                    oracleCommand.Connection = GetConnection();
                    //oracleCommand.Transaction = _transaction;
                    oracleCommand.InitialLONGFetchSize = -1;

                    OracleDataAdapter adapter = new OracleDataAdapter(oracleCommand);
                    adapter.ReturnProviderSpecificTypes = true;
                    adapter.Fill(_dataTable);
                    return _dataTable;
                }
                catch (OracleException ex)
                {
                    throw ex;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }
        #endregion

        #region Test
        void Test()
        {
            List<List<string>> a = new List<List<string>>();
            var command = new OracleCommand("select * from acct_category_test", _activeConnection);

            string frm = "{0},{1},{2}";
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\Users\Daiki\Desktop\ad\test.csv", false, System.Text.Encoding.GetEncoding("Shift-Jis"));
            _dataTable = command.ExecuteReader(CommandBehavior.KeyInfo).GetSchemaTable(); 
            foreach (DataRow myField in _dataTable.Rows)
            {
                List<string> b = new List<string>();
                foreach (DataColumn myProperty in _dataTable.Columns)
                {
                    b.Add(myProperty.ColumnName + " = " + myField[myProperty].ToString() + " as " + myField[myProperty].GetType().Name);
                    sw.WriteLine(String.Format(frm, myProperty.ColumnName, myField[myProperty].ToString(), myField[myProperty].GetType().Name));
                }
                a.Add(b);
            }
            sw.Close();
        }
        #endregion
        
        #region GetSchemaTable
        public DataTable GetSchemaTable(string sql, CommandBehavior commandBehavior)
        {
            using (var command = new OracleCommand(sql, GetConnection()))
            {
                try
                {
                    return command.ExecuteReader(commandBehavior).GetSchemaTable();             
                }
                catch (OracleException ex)
                {
                    throw ex;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        #endregion

        #region GetSchemaTableFromTableName
        public DataTable GetSchemaTableFromTableName(string tableName, CommandBehavior commandBehavior)
        {
            return GetSchemaTable(_sqlLibrary.FormatSelectAllFromTable(tableName), commandBehavior);
        }
        #endregion

        #region Schematable column name
        public string GetSchemaTableColumnName(DataRow row)
        {
            return row[SchemaTableColumn.ColumnName].ToString();
        }
        #endregion

        #region Schematable column Type
        public Type GetSchemaTableColumnType(DataRow row)
        {
            return (Type)row[SchemaTableColumn.DataType];
        }
        #endregion

        #region GetSchemaTableColumnSize
        public int GetSchemaTableColumnSize(DataRow row)
        {
            if (row[SchemaTableColumn.NumericPrecision] is DBNull)
            {
                return (int)row[SchemaTableColumn.ColumnSize];
            }
            else
            {
                if ((bool)row[SchemaTableColumn.IsExpression])
                    return 0;
                else
                    return (short)row[SchemaTableColumn.NumericPrecision];
            }
        }
        #endregion

        #region GetSchemaTableColumnTypeName
        public string GetSchemaTableColumnTypeName(DataRow row)
        {
            OracleDbType type = (OracleDbType)(int)row[SchemaTableColumn.ProviderType];

            #region switch
            switch (type)
            {
                case OracleDbType.Array:
                case OracleDbType.BFile:
                case OracleDbType.Blob:
                case OracleDbType.Byte:
                case OracleDbType.Char:
                case OracleDbType.Clob:
                case OracleDbType.Date:
                case OracleDbType.Long:
                case OracleDbType.NChar:
                case OracleDbType.NClob:
                case OracleDbType.NVarchar2:
                case OracleDbType.Object:
                case OracleDbType.Raw:
                case OracleDbType.Ref:
                case OracleDbType.TimeStamp:
                case OracleDbType.Varchar2:
                    return type.ToString().ToUpper();
                case OracleDbType.BinaryDouble:
                    return "BINARY_DOUBLE";
                case OracleDbType.BinaryFloat:
                    return "BINARY_FLOAT";
                case OracleDbType.Decimal:
                case OracleDbType.Int16:
                case OracleDbType.Int32:
                case OracleDbType.Int64:
                    return "NUMBER";
                case OracleDbType.Double:
                case OracleDbType.Single:
                    return "FLOAT";
                case OracleDbType.IntervalDS:
                    return "INTERVAL DAY TO SECOND";
                case OracleDbType.IntervalYM:
                    return "INTERVAL YEAR TO MONTH";
                case OracleDbType.LongRaw:
                    return "LONG RAW";
                case OracleDbType.RefCursor:
                    return "REF CURSOR";
                case OracleDbType.TimeStampLTZ:
                    return "TIMESTAMP WITH LOCAL TIME ZONE";
                case OracleDbType.TimeStampTZ:
                    return "TIMESTAMP WITH TIME ZONE";
                case OracleDbType.XmlType:
                    return "XMLType";
            }
            #endregion

            throw new Exception("Unexpected OracleDbType");
        }
        #endregion

        #region GetSchemaTableColumnSizeText
        public string GetSchemaTableColumnSizeText(DataRow row)
        {
            int size = GetSchemaTableColumnSize(row);
            int scale = GetSchemaTableColumnScale(row);

            if (size <= 0)
                return null;

            if (scale > 0)
                return String.Format(_scaledColumnSizeFormat, size, scale);
            else
                return String.Format(_defaultColumnSizeFormat, size);
        }

        int GetSchemaTableColumnScale(DataRow row)
        {
            if (row[SchemaTableColumn.NumericScale] is DBNull)
                return 0;
            else
                return (short)row[SchemaTableColumn.NumericScale];
        }
        #endregion

        #region Schematable IsKeyColumn
        public bool IsKeyColumn(DataRow row)
        {
            return (bool)row[SchemaTableColumn.IsKey];
        }
        #endregion

        #region Schematable BaseTableName
        public string GetSchemaTableBaseTableName(DataRow row)
        {
            return (string)row[SchemaTableColumn.BaseTableName].ToStringOrNull();
        }
        #endregion

        #endregion

        #region transaction

        #region BeginTransaction
        public bool BeginTransaction()
        {
            if (_activeTransaction != null && _activeTransaction.Connection != null)
                return true;

            try
            {
                var connection = GetConnection();
                _activeTransaction = connection.BeginTransaction();
                return true;
            }
            catch (OracleException ex)
            {
                throw ex;
            }           
        }
        #endregion

        #region ExecuteNonQuerySql
        public int ExecuteNonQuerySql(string dml)
        {
            try
            {
                using (var command = new OracleCommand(dml, _activeTransaction.Connection))
                {
                    return command.ExecuteNonQuery();
                }          
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ExecuteNonQueryCommand
        public int ExecuteNonQueryCommand(IDbCommand command)
        {
            try
            {
                using (OracleCommand odbcCommand = (OracleCommand)command)
                {
                    odbcCommand.Connection = _activeTransaction.Connection;
                    odbcCommand.Transaction = _activeTransaction;

                    return odbcCommand.ExecuteNonQuery();
                }
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Commit
        public bool CommitTransaction()
        {
            if (_activeTransaction == null || _activeTransaction.Connection == null)
                return true;

            try
            {
                _activeTransaction.Commit();
                return true;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Rollback
        public bool RollbackTransaction()
        {
            if (_activeTransaction == null || _activeTransaction.Connection == null)
                return true;

            try
            {
                _activeTransaction.Rollback();
                return true;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

    }
}
