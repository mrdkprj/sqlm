using MasudaManager.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public class QuerySqlService : SqlService
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        List<string> _columnNames;
        List<Type> _columnTypes;

        public override IEnumerable<SqlResult> ExecuteSql(string sql)
        {
            return GetSqlResultsFromDataTable(_dataAccess.ExecuteQuerySql(sql));
        }

        protected override async Task Execute(IDbCommand dbCommand, CancellationToken token)
        {
            try
            {
                if (dbCommand.Parameters.Count > 0)
                {
                    this.Task = Task.Run(() =>
                    {
                        ExecuteCommandAsync(dbCommand, token);
                    });
                }
                else
                {
                    this.Task = Task.Run(() =>
                    {
                        ExecuteReaderAsync(dbCommand, token);
                    });
                }

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

        void Prepare(CancellationToken token)
        {
            this.CurrentSqlResult = new SqlResult();

            token.ThrowIfCancellationRequested();
        }

        void ExecuteCommandAsync(IDbCommand command, CancellationToken token)
        {
            Prepare(token);

            DataTable dataTable = _dataAccess.ExecuteQueryCommand(command);

            token.ThrowIfCancellationRequested();

            _columnNames = GetColumnNamesFromDataTable(dataTable).ToList();
            _columnTypes = GetColumnTypesFromDataTable(dataTable).ToList();

            if (dataTable.Rows.Count <= 0)
                OnNoRecordFound();

            foreach (DataRow row in dataTable.Rows)
            {
                token.ThrowIfCancellationRequested();

                this.CurrentSqlResult = new SqlResult();
                this.CurrentSqlResult.ColumnNames = _columnNames;
                this.CurrentSqlResult.ColumnTypes = _columnTypes;
                this.CurrentSqlResult.RowValues = GetVaulesFromDataRow(row).ToList();

                Notify();
            }
        }

        void ExecuteReaderAsync(IDbCommand command, CancellationToken token)
        {
            Prepare(token);

            using (DbDataReader reader = _dataAccess.ExecuteDbDataReader(command))
            {
                token.ThrowIfCancellationRequested();

                _columnNames = GetColumnNamesFromReader(reader).ToList();
                _columnTypes = GetColumnTypesFromReader(reader).ToList();

                if (!reader.HasRows)
                    OnNoRecordFound();

                while (reader.Read())
                {
                    token.ThrowIfCancellationRequested();

                    this.CurrentSqlResult = new SqlResult();
                    this.CurrentSqlResult.ColumnNames = _columnNames;
                    this.CurrentSqlResult.ColumnTypes = _columnTypes;
                    this.CurrentSqlResult.RowValues = GetValuesFromReader(reader).ToList();

                    Notify();
                }
            }
        }

        void OnNoRecordFound()
        {
            this.CurrentSqlResult.ColumnNames = _columnNames;
            this.CurrentSqlResult.ColumnTypes = _columnTypes;

            Notify();
        }
    }
}
