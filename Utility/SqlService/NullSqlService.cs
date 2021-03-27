using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public class NullSqlService : ISqlService
    {
        SqlResult _resultSet = new SqlResult();
        List<SqlResult> _resultList = new List<SqlResult>();

        public bool IsBusy
        {
            get { return false; }
        }

        public SqlResult SqlResult { get { return _resultSet; } }

        static NullSqlService _instance = new NullSqlService();
        static NullSqlService() { }
        private NullSqlService() { }

        public static NullSqlService GetInstance()
        {
            return _instance;
        }

        public IEnumerable<SqlResult> ExecuteSql(string sql)
        {
            return _resultList;
        }

        public Task ExecuteSqlAsync(System.Data.IDbCommand dbCommand, System.Threading.CancellationToken token)
        {
            return null;
        }

        public bool Commit()
        {
            return true;
        }

        public bool Rollback()
        {
            return true;
        }

        public void Attach(IObserver observer) { }

        public void Detach(IObserver observer) { }

        public void Notify() { }

        public void NotifyComplete() { }

        public void NotifyError(Exception e) { }

        public void Dispose() { }

        public bool NotifyOnUpdate { get; set; }
    }
}
