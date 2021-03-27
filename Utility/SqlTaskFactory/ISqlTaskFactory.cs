using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MasudaManager.Utility
{
    public interface ISqlTaskFactory
    {
        ISqlTask GetSqlTask(object guid);

        IEnumerable<ISqlTask> GetSqlTasks();

        ISqlTask CreateSqlTask(SqlTaskRequest request);

        void DisposeTask(object guid);

        void DisposeTask();
    }
}
