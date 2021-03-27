using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.DataAccess
{
    public interface IDataAccess
    {
        RdbmsType DatabaseType { get; }
        string DataDictioanryOwner { get; }
        IEnumerable<string> ModeList { get; }
        ConnectionData CurrentConnectionData { get; }
        IDbCommand DbCommand { get; }
        ISqlLibrary SqlLibrary { get; }
        IDataAccessConverter Converter { get; }
        int DefaultCommandTimeout { get; set; }

        string GetParameterString();
        string GetParameterString(string parameterName);
        string GetParameterString(int parameterIndex);

        #region Connection

        bool Connect(ConnectionData connectionInfo);

        bool TryConnect(ConnectionData connectionInfo);

        bool Disconnect();

        #endregion

        #region Query

        DbDataReader ExecuteDbDataReader(string sql);

        DbDataReader ExecuteDbDataReader(IDbCommand command);

        DataTable ExecuteQuerySql(string sql);

        DataTable ExecuteQueryCommand(IDbCommand command);

        #endregion

        #region SchemaTable

        DataTable GetSchemaTable(string sql, CommandBehavior commandBehavior);

        DataTable GetSchemaTableFromTableName(string tableName, CommandBehavior commandBehavior);
        
        string GetSchemaTableColumnName(DataRow row);

        Type GetSchemaTableColumnType(DataRow row);

        int GetSchemaTableColumnSize(DataRow row);
        
        string GetSchemaTableColumnTypeName(DataRow row);

        string GetSchemaTableColumnSizeText(DataRow row);

        bool IsKeyColumn(DataRow row);

        string GetSchemaTableBaseTableName(DataRow row);

        #endregion

        #region transaction

        bool BeginTransaction();

        int ExecuteNonQuerySql(string dml);

        int ExecuteNonQueryCommand(IDbCommand command);

        bool CommitTransaction();

        bool RollbackTransaction();

        #endregion

    }
}
