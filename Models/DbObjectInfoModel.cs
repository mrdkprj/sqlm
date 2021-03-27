using MasudaManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasudaManager
{
    public class DbObjectInfoModel : IModel
    {
        public List<string> SchemaObjectList { get; set; }
        public string SelectedDbObjectName { get; set; }
        Dictionary<object, Action<Action<object>, IDbObjectSqlTask>> _presenterDataSourceSetters = new Dictionary<object, Action<Action<object>, IDbObjectSqlTask>>();
        Dictionary<object, Action<object>> _viewDataSourceSetters = new Dictionary<object, Action<object>>();

        public DbObjectInfoModel()
        {
            this.SchemaObjectList = GetObjectListDataSource().ToList();
        }

        IEnumerable<string> GetObjectListDataSource()
        {
            foreach (string schemaObjectName in DbObjectToken.Tokens)
            {
                yield return schemaObjectName;
            }
        }
        
        public void ReleaseModel()
        {
            _presenterDataSourceSetters.Clear();
            _viewDataSourceSetters.Clear();
        }

        public void RegisterBindDataSourceMethod(object guid, Action<Action<object>, IDbObjectSqlTask> bindMethod, Action<object> datasourceMethod)
        {
            if (!_presenterDataSourceSetters.ContainsKey(guid))
            {
                _presenterDataSourceSetters.Add(guid, bindMethod);
                _viewDataSourceSetters.Add(guid, datasourceMethod);
            }
        }

        public void InvokeBindDataSourceMethod(object guid, IDbObjectSqlTask sqlTask)
        {
            if (_presenterDataSourceSetters.ContainsKey(guid))
            {
                Action<object> viewMethod = _viewDataSourceSetters.GetValueOrDefault(guid);
                _presenterDataSourceSetters.GetValueOrDefault(guid).Invoke(viewMethod, sqlTask);
            }
        }

    }

}
