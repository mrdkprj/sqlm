using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace MasudaManager.Utility
{
    public interface ISqlTaskManager : IObserver
    {
        ISqlTaskFactory Factory { get; }

        Action<ISqlTask> TaskStartAction { get; set; }

        Action<ISqlTask, TaskCompleteEventArgs> TaskCompleteAction { get; set; }

        ISqlTask CurrentTask { get; }

        SqlTaskOnErrorActionType DefaultErrorAction { get; set; }

        Task RunSqlTask(object guid, string sql, bool attachObserver);

        Task RunSqlTask(object guid, IDbCommandBuilder statementBuilder, bool attachObserver);

        void Cancel();

        bool IsProcessBusy(object guid);

        bool IsProcessBusy();

        void Release(IObserver observer);

        void FilterTaskResult(object guid, string keyString);

        void ClearFilter(object guid);

    }
}
