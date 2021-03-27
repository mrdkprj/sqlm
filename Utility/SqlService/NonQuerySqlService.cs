using MasudaManager.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public class NonQuerySqlService : SqlService
    {
        readonly string _transactionFailedMessage = "Transaction start failed.";
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        public override IEnumerable<SqlResult> ExecuteSql(string sql)
        {
            if (!_dataAccess.BeginTransaction())
            {
                Rollback();
                throw new Exception(_transactionFailedMessage);
            }

            this.CurrentSqlResult = new SqlResult();
            this.CurrentSqlResult.AffectedRowCount = _dataAccess.ExecuteNonQuerySql(sql);

            yield return this.CurrentSqlResult;
        }

        protected override async Task Execute(IDbCommand dbCommand, CancellationToken token)
        {
            try
            {
                this.Task = Task.Run(() =>
                {
                    ExecuteCommand(dbCommand, token);
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

        void ExecuteCommand(IDbCommand dbCommand, CancellationToken token)
        {
            this.CurrentSqlResult = new SqlResult();

            if (!_dataAccess.BeginTransaction())
            {
                Rollback();
                throw new Exception(_transactionFailedMessage);
            }

            token.ThrowIfCancellationRequested();

            this.CurrentSqlResult.AffectedRowCount = _dataAccess.ExecuteNonQueryCommand(dbCommand);

            token.ThrowIfCancellationRequested();

            Notify();
        }

    }
}
