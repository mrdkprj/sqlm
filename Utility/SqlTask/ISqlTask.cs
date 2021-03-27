using MasudaManager.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MasudaManager
{
    public interface ISqlTask : IDisposable, IObserver, IObservable
    {
        event EventHandler TaskStart;
        event EventHandler<TaskCompleteEventArgs> TaskComplete;

        object Guid { get; set; }

        bool Cancelable { get; set; }

        bool IsServiceBusy { get; }

        ISqlService SqlService { get; set; }

        SqlBaseInfo ExcutedSqlInfo { get; }

        IEnumerable<SqlResult> SqlResults { get; }

        DynamicSortableBindingList<SqlResult, QueryResult> BindingModel { get; }

        SqlTaskStatus Status { get; }

        void Clear();

        Task Run(IDbCommandBuilder commands, CancellationToken token);

        bool Commit();

        bool Rollback();
    }
}
