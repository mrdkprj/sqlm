using MasudaManager.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.DataAccess.HiRDB
{
    public class HiRDBDataAccess : IDataAccess
    {
        const string WITHOUT_LOCK_NOWAIT = "WITHOUT LOCK NOWAIT";
        readonly string _connectionStringFormat = "dsn={0};uid={1};pwd={2};";
        readonly string _defaultColumnSizeFormat = "({0})";
        readonly string _numericColumnSizeFormat = "({0},{1})";
        readonly string log1 = Environment.CurrentDirectory + "\\pderr1.trc";
        readonly string log2 = Environment.CurrentDirectory + "\\pderr2.trc";
        readonly string _parameterString = "?";
        readonly string _masterSchemaName = "MASTER";
        int _defaultCommandTimeout = 30;

        OdbcConnection _activeConnection = null;
        OdbcTransaction _activeTransaction = null;
        ISqlLibrary _sqlLibrary = new HiRDBSqlLibrary();
        ConnectionData _connectionData = new ConnectionData();

        #region Property

        public RdbmsType DatabaseType { get { return RdbmsType.HiRDB; } }

        public string DataDictioanryOwner { get { return _masterSchemaName; } }

        public IEnumerable<string> ModeList { get { return null; } }

        public ConnectionData CurrentConnectionData { get { return _connectionData; } }

        public IDbCommand DbCommand { get { return new OdbcCommand(); } }

        public ISqlLibrary SqlLibrary { get { return _sqlLibrary; } }

        public IDataAccessConverter Converter { get { return HiRDBDataAccessConverter.GetInstance(); } }

        public int DefaultCommandTimeout
        {
            get { return _defaultCommandTimeout; }
            set { _defaultCommandTimeout = value; }
        }
        #endregion

        #region Constructor
        public HiRDBDataAccess()
        {
        }
        #endregion

        #region Parameter string
        public string GetParameterString()
        {
            return _parameterString;
        }

        public string GetParameterString(string parameterName) { return GetParameterString(); }
        public string GetParameterString(int parameterIndex) { return GetParameterString(); }
        #endregion

        #region Connect
        public bool Connect(ConnectionData connectionData)
        {
            Disconnect();

            _connectionData = connectionData;
            _connectionData.ConnectionString = String.Format(_connectionStringFormat, _connectionData.DataSource, _connectionData.UserId, _connectionData.Password);

            _activeConnection = new OdbcConnection();
            _activeConnection.ConnectionString = _connectionData.ConnectionString;
            _activeConnection.ConnectionTimeout = 0;

            try
            {
                _activeConnection.Open();
                _connectionData.IsConnected = true;
                return true;
            }
            catch (OdbcException ex)
            {
                SetStatusToDisconnected();
                throw ex;
            }
        }
        #endregion

        #region Try connect
        public bool TryConnect(ConnectionData connectionData)
        {
            var connection = new OdbcConnection();
            connection.ConnectionString = String.Format(_connectionStringFormat, connectionData.DataSource, connectionData.UserId, connectionData.Password);
            connection.ConnectionTimeout = 0;

            try
            {
                connection.Open();
                return true;
            }
            catch (OdbcException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Dispose();
            }
        }
        #endregion

        #region GetNewOpenedConnection
        OdbcConnection GetNewOpenedConnection()
        {
            OdbcConnection connection = new OdbcConnection(_connectionData.ConnectionString);
            try
            {
                connection.Open();
                _activeConnection = connection;
                return connection;
            }
            catch(Exception e)
            {
                connection.Close();
                throw e;
            }

        }
        #endregion

        #region Disconnect
        public bool Disconnect()
        {
            if (_activeConnection == null || _activeConnection.State == ConnectionState.Closed)
                return true;

            try
            {
                RollbackTransaction();

                _activeConnection.Close();

                SetStatusToDisconnected();
                return true;
            }
            catch (OdbcException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SetStatusToDisconnected
        void SetStatusToDisconnected()
        {
            DeleteHiRDBErrorLogFiles();
            _connectionData.IsConnected = false;
        }

        void DeleteHiRDBErrorLogFiles()
        {
            if (File.Exists(this.log1))
                File.Delete(this.log1);

            if (File.Exists(this.log2))
                File.Delete(this.log2);
        }
        #endregion

        //#region ThrowIfNotConnected
        //void ThrowIfNotConnected()
        //{
        //    if (_activeConnection == null)
        //        throw new Exception("Connection is closed");

        //    if (_activeConnection.State == ConnectionState.Closed || _activeConnection.State != ConnectionState.Open)
        //        throw new Exception("Connection is closed");
        //}
        //#endregion

        #region Query

        #region Without lock sql
        string GetSqlWithoutLock(string sql)
        {
            if(!sql.EndsWith(WITHOUT_LOCK_NOWAIT))
                return sql + Constants.StringSpace + WITHOUT_LOCK_NOWAIT;

            return sql;
        }
        #endregion

        #region ExecuteQuerySql
        public DataTable ExecuteQuerySql(string sql)
        {
            return ExecuteQueryCommand(new OdbcCommand(sql));
        }
        #endregion

        #region ExecuteDbDataReader
        public DbDataReader ExecuteDbDataReader(string sql)
        {
            using (var odbcCommand = new OdbcCommand(GetSqlWithoutLock(sql)))
            {
                try
                {
                    odbcCommand.Connection = GetNewOpenedConnection();
                    odbcCommand.Transaction = _activeTransaction;
                    odbcCommand.CommandType = CommandType.Text;
                    odbcCommand.Prepare();

                    OdbcDataReader reader = odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    return reader;
                }
                catch (OdbcException ex)
                {
                    odbcCommand.Connection.Close();
                    throw ex;
                }
                catch (Exception e)
                {
                    odbcCommand.Connection.Close();
                    throw e;
                }
            }

        }

        public DbDataReader ExecuteDbDataReader(IDbCommand command)
        {
            using (OdbcCommand odbcCommand = (OdbcCommand)command)
            {
                try
                {
                    odbcCommand.Connection = GetNewOpenedConnection();
                    odbcCommand.Transaction = _activeTransaction;
                    odbcCommand.CommandType = CommandType.Text;
                    odbcCommand.Prepare();

                    OdbcDataReader reader = odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    return reader;
                }
                catch (OdbcException ex)
                {
                    odbcCommand.Connection.Close();
                    throw ex;
                }
                catch (Exception e)
                {
                    odbcCommand.Connection.Close();
                    throw e;
                }
            }
        }
        #endregion

        #region ExecuteQueryCommand
        public DataTable ExecuteQueryCommand(IDbCommand command)
        {
            command.CommandText = GetSqlWithoutLock(command.CommandText);
            
            using (var connection = GetNewOpenedConnection())
            {
                using (OdbcCommand odbcCommand = (OdbcCommand)command)
                {
                    try
                    {
                        odbcCommand.Connection = connection;
                        odbcCommand.Transaction = _activeTransaction;
                        odbcCommand.CommandType = CommandType.Text;
                        odbcCommand.Prepare();

                        DataTable table = new DataTable();
                        OdbcDataAdapter adapter = new OdbcDataAdapter(odbcCommand);
                        adapter.Fill(table);

                        return table;
                    }
                    catch (OdbcException ex)
                    {
                        throw ex;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

        }
        #endregion

        #region Test
        void Test()
        {
            List<List<string>> a = new List<List<string>>();
            var command = new OdbcCommand("select * from MMTOKUP without lock no wait", _activeConnection);

            System.IO.StreamWriter sw = new StreamWriter(@"C:\Users\A13926\Desktop\test.csv", false, System.Text.Encoding.GetEncoding("Shift-Jis"));
            string frm = "{0},{1},{2}";
            sw.WriteLine(String.Format(frm, "Column", "Value", "Type"));
            //DataTable schemaTable = _connection.GetSchema("Tables", new string[] { "", "CIS23", "MMSEIHP" });
            DataTable schemaTable = command.ExecuteReader(CommandBehavior.KeyInfo).GetSchemaTable();
            foreach (DataRow myField in schemaTable.Rows)
            {
                List<string> b = new List<string>();
                foreach (DataColumn myProperty in schemaTable.Columns)
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
            using (var connection = GetNewOpenedConnection())
            {
                using (var odbcCommand = new OdbcCommand(sql))
                {
                    try
                    {
                        odbcCommand.Connection = connection;
                        odbcCommand.Transaction = _activeTransaction;
                        odbcCommand.CommandType = CommandType.Text;

                        return odbcCommand.ExecuteReader(commandBehavior).GetSchemaTable();
                    }
                    catch (OdbcException ex)
                    {
                        throw ex;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
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
            return (Type)row[System.Data.Common.SchemaTableColumn.DataType];
        }
        #endregion

        #region Schematable column size
        public int GetSchemaTableColumnSize(DataRow row)
        {
            if (row[SchemaTableColumn.NumericPrecision] == null)
                return (int)row[SchemaTableColumn.ColumnSize];
            else
                return (short)row[SchemaTableColumn.NumericPrecision];
        }
        #endregion

        #region GetSchemaTableColumnTypeName
        public string GetSchemaTableColumnTypeName(DataRow row)
        {
            OdbcType type = (OdbcType)(int)row[SchemaTableColumn.ProviderType];
            return type.ToString();
        }
        #endregion

        #region GetSchemaTableColumnSizeText
        public string GetSchemaTableColumnSizeText(DataRow row)
        {
            OdbcType type = (OdbcType)(int)row[SchemaTableColumn.ProviderType];

            switch (type)
            {
                case OdbcType.Decimal:
                case OdbcType.Numeric:
                    return String.Format(_numericColumnSizeFormat, GetSchemaTableColumnSize(row), GetSchemaTableColumnScale(row));
                default:
                    return String.Format(_defaultColumnSizeFormat, GetSchemaTableColumnSize(row));
            }
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
            return (string)row[SchemaTableColumn.BaseTableName];
        }
        #endregion

        #endregion

        #region transaction

        #region トランザクション開始
        public bool BeginTransaction()
        {
            if (_activeTransaction != null && _activeTransaction.Connection != null)
                return true;

            try
            {
                _activeTransaction = _activeConnection.BeginTransaction();
                return true;
            }
            catch (OdbcException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DML実行(SQL)
        public int ExecuteNonQuerySql(string dml)
        {
            //// DDL実行時、前のトランザクションがコミットされていない場合エラーとなるため
            //// DDL実行前にコミット発行
            if (SqlTypeParser.Parse(dml) == SqlType.DDL)
                CommitTransaction();

            try
            {
                using (var command = new OdbcCommand(dml, _activeConnection, _activeTransaction))
                {
                    return command.ExecuteNonQuery();
                }
            }
            catch (OdbcException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DML実行(Command)
        public int ExecuteNonQueryCommand(IDbCommand command)
        {
            //// DDL実行時、前のトランザクションがコミットされていない場合エラーとなるため
            //// DDL実行前にコミット発行
            if (SqlTypeParser.Parse(command.CommandText) == SqlType.DDL)
                CommitTransaction();

            try
            {
                using (OdbcCommand odbcCommand = (OdbcCommand)command)
                {
                    odbcCommand.Connection = _activeConnection;
                    odbcCommand.Transaction = _activeTransaction;

                    return odbcCommand.ExecuteNonQuery();
                }
            }
            catch (OdbcException ex)
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
            catch (OdbcException ex)
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
            catch (OdbcException ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

    }
}
