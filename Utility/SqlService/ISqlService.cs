using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MasudaManager
{
    public interface ISqlService : IObservable, IDisposable
    {
        bool IsBusy { get; }

        bool NotifyOnUpdate { get; set; }

        SqlResult SqlResult{ get; }

        IEnumerable<SqlResult> ExecuteSql(string sql);

        Task ExecuteSqlAsync(IDbCommand dbCommand, CancellationToken token);

        bool Commit();

        bool Rollback();
    }
}
