using System;
using System.Collections.Generic;
using System.ComponentModel;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility
{
    class SqlTaskFactory : ISqlTaskFactory
    {       
        IObserver _observer;
        ISynchronizeInvoke _invoker;
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        Dictionary<object, SqlTaskSet> _taskSetDictionary = new Dictionary<object, SqlTaskSet>();
        SqlTaskRequest _request = new SqlTaskRequest();
        SqlTaskRequest _defaultRequst = new SqlTaskRequest(null, SqlType.Empty, false);

        public SqlTaskFactory(IObserver observer, ISynchronizeInvoke invoker)
        {
            _observer = observer;
            _invoker = invoker;
        }

        public ISqlTask GetSqlTask(object guid)
        {
            if (_taskSetDictionary.ContainsKey(guid))
                return _taskSetDictionary.GetValueOrDefault(guid).CurrentSqlTask;

            _request.RenewRequest(guid, SqlType.Empty, false);

            return GetOrCreateSqlTask(_request);
        }

        public IEnumerable<ISqlTask> GetSqlTasks()
        {
            foreach (SqlTaskSet sqlTaskSet in _taskSetDictionary.Values)
            {
                foreach (ISqlTask sqlTask in sqlTaskSet)
                {
                    yield return sqlTask;
                }
            }
        }

        public ISqlTask CreateSqlTask(SqlTaskRequest request)
        {
            return GetOrCreateSqlTask(request);
        }
        
        ISqlTask GetOrCreateSqlTask(SqlTaskRequest request)
        {
            if (!_dataAccess.CurrentConnectionData.IsConnected)
                return CreateInstance(_defaultRequst);

            if (request.ParsedSqlType == SqlType.Empty || request.ParsedSqlType == SqlType.Invalid)
                return CreateInstance(request);

            RegisterGuid(request.Guid);

            return RegisterSqlTask(request);
        }

        void RegisterGuid(object guid)
        {
            if (!_taskSetDictionary.ContainsKey(guid))
                _taskSetDictionary.Add(guid, new SqlTaskSet());
        }

        ISqlTask RegisterSqlTask(SqlTaskRequest request)
        {
            Type sqlTaskType = GetSqlTaskType(request.ParsedSqlType);

            if (!_taskSetDictionary[request.Guid].Contains(sqlTaskType))
                _taskSetDictionary[request.Guid].Add(CreateInstance(request));

            return _taskSetDictionary[request.Guid].GetSqlTask(sqlTaskType);
        }

        Type GetSqlTaskType(SqlType sqlType)
        {
            switch (SqlTypeParser.GetSqlCommandType(sqlType))
            {
                case SqlCommandType.Query:
                    return typeof(QuerySqlTask);
                case SqlCommandType.NonQuery:
                    return typeof(NonQuerySqlTask);
                case SqlCommandType.Invalid:
                    return typeof(InvalidSqlTask);
                default:
                    return typeof(NullSqlTask);
            }
        }
        
        ISqlTask CreateInstance(SqlTaskRequest request)
        {
            ISqlTask sqlTask;

            switch (SqlTypeParser.GetSqlCommandType(request.ParsedSqlType))
            {
                case SqlCommandType.Query:
                    sqlTask = GetQuerySqlTask();
                    break;
                case SqlCommandType.NonQuery:
                    sqlTask = GetNonQuerySqlTask();
                    break;
                case SqlCommandType.Invalid:
                    sqlTask = GetInvalidSqlTask();
                    break;
                default:
                    sqlTask = GetNullSqlTask();
                    break;
            }

            return ConfigureTask(request, sqlTask);
        }

        ISqlTask GetQuerySqlTask()
        {
            return new QuerySqlTask(_invoker, new QuerySqlService());
        }

        ISqlTask GetNonQuerySqlTask()
        {
            return new NonQuerySqlTask(_invoker, new NonQuerySqlService());
        }

        ISqlTask GetInvalidSqlTask()
        {
            return InvalidSqlTask.GetInstance();
        }

        ISqlTask GetNullSqlTask()
        {
            return NullSqlTask.GetInstance();
        }

        ISqlTask ConfigureTask(SqlTaskRequest request, ISqlTask sqlTask)
        {
            sqlTask.Guid = request.Guid;
            sqlTask.Cancelable = true;
            sqlTask.SqlService.NotifyOnUpdate = request.NotifyOnUpdate;
            if (request.NotifyOnUpdate)
                sqlTask.Attach(_observer);

            return sqlTask;
        }

        public void DisposeTask(object guid)
        {
            var taskSet = _taskSetDictionary.GetValueOrDefault(guid);

            if (taskSet != null)
                taskSet.DisposeTaskSet();
        }

        public void DisposeTask()
        {
            foreach (var sqlTaskSet in _taskSetDictionary.Values)
            {
                sqlTaskSet.DisposeTaskSet();
            }

            _taskSetDictionary.Clear();
        }
    }
}
