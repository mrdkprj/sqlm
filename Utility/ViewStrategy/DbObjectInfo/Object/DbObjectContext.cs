using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Utility.DbObject;
using System.Collections.Concurrent;

namespace MasudaManager.Utility
{
    public class DbObjectStrategyContext
    {
        IDbObjectStrategy _strategy;
        IDbObjectStrategy _defaultStrategy = NullObjectStrategy.Instance;
        DbObjectPropertyContext _propertyContext = new DbObjectPropertyContext();

        public DbObjectStrategyContext(IDbObjectStrategy initialStrategy = null)
        {
            if (initialStrategy != null)
                SetStrategy(initialStrategy);
            else
                SetStrategy(_defaultStrategy);
        }

        public IDbObjectStrategy Strategy { get { return _strategy; } }

        public IDbObjectStrategy Property { get { return _propertyContext.Strategy; } }

        public void Reset()
        {
            SetStrategy(_defaultStrategy);
        }

        public void ChangeStrategy(DbObjectType objectType)
        {
            if (objectType == _strategy.ObjectType)
                return;

            IDbObjectStrategy strategy = _defaultStrategy;

            switch (objectType)
            {
                case DbObjectType.Table:
                    strategy = TableObjectStrategy.Instance;
                    break;
                case DbObjectType.View:
                    strategy = ViewObjectStrategy.Instance;
                    break;
                case DbObjectType.Index:
                    strategy = IndexObjectStrategy.Instance;
                    break;
                case DbObjectType.Package:
                    strategy = PackageObjectStrategy.Instance;
                   break;
                case DbObjectType.PackageBody:
                   strategy = PackageBodyObjectStrategy.Instance;
                    break;
                case DbObjectType.Procedure:
                    strategy = ProcedureObjectStrategy.Instance;
                   break;
                case DbObjectType.Function:
                   strategy = FunctionObjectStrategy.Instance;
                    break;
                case DbObjectType.Type:
                    strategy = TypeObjectStrategy.Instance;
                    break;
                case DbObjectType.Trigger:
                    strategy = TriggerObjectStrategy.Instance;
                    break;
                case DbObjectType.Synonym:
                    strategy = SynonymObjectStrategy.Instance;
                    break;
                case DbObjectType.Sequence:
                    strategy = SequenceObjectStrategy.Instance;
                    break;
            }

            SetStrategy(strategy);
        }
        
        public void ChangeProperty(DbObjectPropertyType propertyType)
        {
            _propertyContext.ChangeStrategy(propertyType);
        }

        public bool CanChangeProperties()
        {
            return _propertyContext.CanVisitProperties(_strategy);
        }

        public bool MoveNextProperty()
        {
            return _propertyContext.VisitProperty();
        }

        public void ResetProperty()
        {
            _propertyContext.Prepare(_strategy);
        }

        public IEnumerable<IDbObjectStrategy> GetPropertyStrategies()
        {
            return _propertyContext.GetPropertyStrategies();
        }

        public DbObjectPropertyType GetPropertyTypeFlags()
        {
            return _propertyContext.GetPropertyTypeFlags();
        }

        void SetStrategy(IDbObjectStrategy strategy)
        {
            _strategy = strategy;
            _propertyContext.Prepare(strategy);
        }
    }
}
