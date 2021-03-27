using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MasudaManager.Utility.DbObject;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility
{
    class DbObjectSqlTaskFactory : ISqlTaskFactory
    {
        IObserver _observer;
        ISynchronizeInvoke _invoker;
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        Dictionary<object, ISqlTask> _taskDictionary = new Dictionary<object, ISqlTask>();
        IDbObjectStrategy _defaultStrategy = NullObjectStrategy.Instance;

        public DbObjectSqlTaskFactory(IObserver observer, ISynchronizeInvoke invoker)
        {
            _observer = observer;
            _invoker = invoker;
        }

        public ISqlTask GetSqlTask(object guid)
        {
            return GetOrCreateSqlTask(GetStrategy(guid));
        }

        public IEnumerable<ISqlTask> GetSqlTasks()
        {
            return _taskDictionary.Values;
        }

        public ISqlTask CreateSqlTask(SqlTaskRequest request)
        {
            return GetOrCreateSqlTask(GetStrategy(request.Guid));
        }

        IDbObjectStrategy GetStrategy(object guid)
        {
            IDbObjectStrategy strategy = guid as IDbObjectStrategy;

            if (strategy == null)
                return NullObjectStrategy.Instance;

            return strategy;
        }

        ISqlTask GetOrCreateSqlTask(IDbObjectStrategy strategy)
        {
            if (!_dataAccess.CurrentConnectionData.IsConnected)
                return CreateInstance(_defaultStrategy);

            if (strategy.ObjectType == DbObjectType.Empty)
                return CreateInstance(strategy);
        
            if (_taskDictionary.ContainsKey(strategy))
                return _taskDictionary.GetValueOrDefault(strategy);

            return RegisterSqlTask(strategy);
        }

        ISqlTask RegisterSqlTask(IDbObjectStrategy strategy)
        {
            if (_taskDictionary.ContainsKey(strategy))
                return _taskDictionary.GetValueOrDefault(strategy);

            _taskDictionary.Add(strategy, CreateInstance(strategy));

            return _taskDictionary.GetValueOrDefault(strategy);
        }

        ISqlTask CreateInstance(IDbObjectStrategy strategy)
        {
            IDbObjectSqlTask sqlTask;

            if (strategy.ObjectType == DbObjectType.Empty)
                sqlTask = NullSqlTask.GetInstance();
            else
                sqlTask = new DbObjectSqlTask(_invoker, new QuerySqlService());

            sqlTask.Strategy = strategy;
            sqlTask.Cancelable = false;
            sqlTask.SqlService.NotifyOnUpdate = true;
            sqlTask.Attach(_observer); 
            
            return sqlTask;
        }

        public void DisposeTask(object guid)
        {
            ISqlTask sqlTask = _taskDictionary.GetValueOrDefault(guid);

            if (sqlTask == null)
                return;

            sqlTask.Clear();
            sqlTask = null;
            _taskDictionary.Remove(guid);
        }

        public void DisposeTask()
        {
            foreach (var pair in _taskDictionary)
            {
                ISqlTask sqlTask = pair.Value;
                sqlTask.Clear();
                sqlTask = null;
            }

            _taskDictionary.Clear();
        }
    }
}
