using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.DbObject
{
    public class DbObjectPropertyContext
    {
        IDbObjectStrategy _strategy;
        Stack<IDbObjectStrategy> _tempStack = new Stack<IDbObjectStrategy>();
        Queue<IDbObjectStrategy> _waitQueue = new Queue<IDbObjectStrategy>();
        Dictionary<DbObjectPropertyType, IDbObjectStrategy> _propertyStrategies = new Dictionary<DbObjectPropertyType, IDbObjectStrategy>();
        bool _canVisit = false;

        public IDbObjectStrategy Strategy { get { return _strategy; } }

        public void Prepare(IDbObjectStrategy objectStrategy)
        {
            _canVisit = true;
            PreparePropertyStrategies(objectStrategy);
            _strategy = _propertyStrategies.FirstOrDefault().Value;
        }

        void PreparePropertyStrategies(IDbObjectStrategy strategy)
        {
            _waitQueue.Clear();
            _propertyStrategies.Clear();

            _tempStack.Push(strategy.ChildStrategy);

            while (_tempStack.Count > 0)
            {
                IDbObjectStrategy current = _tempStack.Pop();
                _propertyStrategies.Add(current.PropertyType, current);
                if (current.ChildStrategy != null)
                    _tempStack.Push(current.ChildStrategy);
            }
        }

        public void ChangeStrategy(DbObjectPropertyType propertyType)
        {
            _strategy = _propertyStrategies.GetValueOrDefault(propertyType);
        }

        public bool CanVisitProperties(IDbObjectStrategy objectStrategy)
        {
            //if (_waitQueue.Count > 0)
            //    return true;
            //else
            //    EnqueueAllProperties();

            EnqueueAllProperties();

            //if (_waitQueue.Count > 0 && _canVisit == false)
            //    throw new Exception();

            return _canVisit;
        }

        void EnqueueAllProperties()
        {
            foreach (var pair in _propertyStrategies)
            {
                _waitQueue.Enqueue(pair.Value);
            }
        }

        public bool VisitProperty()
        {
            if (_waitQueue.Count > 0)
            {
                _canVisit = false;
                _strategy = _waitQueue.Dequeue();
                return true;
            }
            else
            {
                _canVisit = true;
                return false;
            }
        }

        public IEnumerable<IDbObjectStrategy> GetPropertyStrategies()
        {
            foreach (var pair in _propertyStrategies)
            {
                yield return pair.Value;
            }
        }

        public DbObjectPropertyType GetPropertyTypeFlags()
        {
            DbObjectPropertyType propertyType = DbObjectPropertyType.None;
            foreach (var pair in _propertyStrategies)
            {
                propertyType = propertyType | pair.Value.PropertyType;
            }

            return propertyType;
        }
    }
}