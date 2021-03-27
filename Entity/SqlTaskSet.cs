using System;
using System.Collections.Generic;

namespace MasudaManager
{
    public class SqlTaskSet : IEnumerable<ISqlTask>
    {
        Dictionary<Type, ISqlTask> _typeDictionary = new Dictionary<Type, ISqlTask>();
        ISqlTask _currentSqlTask;

        public ISqlTask CurrentSqlTask { get { return _currentSqlTask; } }

        public void Add(ISqlTask sqlTask)
        {
            _typeDictionary[sqlTask.GetType()] = sqlTask;
            _currentSqlTask = sqlTask;
        }

        public bool Contains(Type taskType)
        {
            return _typeDictionary.ContainsKey(taskType);
        }

        public ISqlTask GetSqlTask(Type taskType)
        {
            _currentSqlTask = _typeDictionary.GetValueOrDefault(taskType);

            return _currentSqlTask;
        }

        public void DisposeTaskSet()
        {
            foreach (var pair in _typeDictionary)
            {
                ISqlTask sqlTask = pair.Value;
                sqlTask.Clear();
                sqlTask = null;
            }

            _typeDictionary.Clear();
        }

        public void Remove(Type taskType)
        {
            if (!_typeDictionary.ContainsKey(taskType))
                return;

            _typeDictionary[taskType].Dispose();
            _typeDictionary.Remove(taskType);
        }

        public IEnumerator<ISqlTask> GetEnumerator()
        {
            return _typeDictionary.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
