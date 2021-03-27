using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public class NullSqlTaskFactory : ISqlTaskFactory
    {
        static NullSqlTaskFactory _instance = new NullSqlTaskFactory();
        static NullSqlTaskFactory() { }
        private NullSqlTaskFactory() { }
        public static NullSqlTaskFactory Instance { get { return _instance; } }

        public ISqlTask GetSqlTask(object guid)
        {
            return NullSqlTask.GetInstance();
        }

        public IEnumerable<ISqlTask> GetSqlTasks()
        {
            yield return NullSqlTask.GetInstance();
        }

        public ISqlTask CreateSqlTask(SqlTaskRequest request)
        {
            return NullSqlTask.GetInstance();
        }

        public void DisposeTask(object guid)
        {
        }

        public void DisposeTask()
        {
        }
    }
}
