using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility
{
    public class SchemaTableSqlService : SqlService
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        public override IEnumerable<SqlResult> ExecuteSql(string sql)
        {
            return GetSqlResultsFromSchemaTable(_dataAccess.GetSchemaTable(sql, CommandBehavior.SchemaOnly), false);
        }

        protected override async Task Execute(IDbCommand dbCommand, CancellationToken token)
        {
            try
            {
                this.Task = Task.Run(() =>
                {
                    ExecuteCommandAsync(dbCommand, token);
                });

                await this.Task;

                NotifyComplete();
            }
            catch (OperationCanceledException cancelled)
            {
                NotifyError(cancelled);
            }
            catch (Exception ex)
            {
                NotifyError(ex);
            }
        }

        void ExecuteCommandAsync(IDbCommand command, CancellationToken token)
        {
            this.CurrentSqlResult = new SqlResult();

            DataTable dataTable = _dataAccess.GetSchemaTable(command.CommandText, CommandBehavior.SchemaOnly);

            foreach (DataRow row in dataTable.Rows)
            {
                this.CurrentSqlResult.ColumnNames.Add(_dataAccess.GetSchemaTableColumnName(row));
                this.CurrentSqlResult.ColumnTypes.Add(_dataAccess.GetSchemaTableColumnType(row));
                this.CurrentSqlResult.RowValues.Add(_dataAccess.GetSchemaTableColumnTypeName(row) + _dataAccess.GetSchemaTableColumnSizeText(row));
            }

            Notify();
        }

        public IEnumerable<SqlResult> GetKeyColumnInfoFromTable(string tableName)
        {
            DataTable dataTable = _dataAccess.GetSchemaTableFromTableName(tableName, CommandBehavior.KeyInfo);
            return GetSqlResultsFromSchemaTable(dataTable, true);
        }

        public IEnumerable<SqlResult> GetColumnInfoFromTable(string tableName)
        {
            DataTable dataTable = _dataAccess.GetSchemaTableFromTableName(tableName, CommandBehavior.SchemaOnly);
            return GetSqlResultsFromSchemaTable(dataTable, false);
        }

        public IEnumerable<SqlResult> GetColumnInfoFromSql(string sql)
        {
            DataTable dataTable = _dataAccess.GetSchemaTable(sql, CommandBehavior.SchemaOnly);
            return GetSqlResultsFromSchemaTable(dataTable, false);
        }

        public string GetTableNameFromSql(string sql)
        {
            DataTable dataTable = _dataAccess.GetSchemaTable(sql, CommandBehavior.KeyInfo);
            if (dataTable.Rows.Count > 0)
                return _dataAccess.GetSchemaTableBaseTableName(dataTable.Rows[0]);

            return null;
        }

        IEnumerable<SqlResult> GetSqlResultsFromSchemaTable(DataTable dataTable, bool keyColumn)
        {
            SqlResult result = new SqlResult();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (!keyColumn || _dataAccess.IsKeyColumn(dataRow))
                {
                    result.ColumnNames.Add(_dataAccess.GetSchemaTableColumnName(dataRow));
                    result.ColumnTypes.Add(_dataAccess.GetSchemaTableColumnType(dataRow));
                    result.RowValues.Add(_dataAccess.GetSchemaTableColumnTypeName(dataRow) + _dataAccess.GetSchemaTableColumnSizeText(dataRow));
                }
            }

            yield return result;
        }
    }
}
