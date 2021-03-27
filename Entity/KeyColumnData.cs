using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class KeyColumnData
    {
        List<string> _names = new List<string>();
        List<int> _indexes = new List<int>();
        List<Type> _types = new List<Type>();
        Dictionary<int, object> _indexValuePairs = new Dictionary<int, object>();

        public List<string> ColumnNames { get { return _names; } }
        public List<int> ColumnIndices { get { return _indexes; } }
        public List<Type> ColumnTypes { get { return _types; } }
        public List<object> Values { get { return _indexValuePairs.Values.ToList(); } }

        public KeyColumnData() { }
        public KeyColumnData(IEnumerable<string> columnNames, IEnumerable<int> columnIndexes, IEnumerable<Type> columnTypes)
        {
            _names = columnNames.ToList();
            _indexes = columnIndexes.ToList();
            _types = columnTypes.ToList();
        }

        public int GetColumnIndex(string name)
        {
            if (!_names.Contains(name))
                return -1;

            if (_indexes.Count <= 0)
                return -1;

            return _indexes[_names.IndexOf(name)];
        }

        public string GetColumnName(int index)
        {
            if (!_indexes.Contains(index))
                return null;

            if (_names.Count <= 0)
                return null;

            return _names[_indexes.IndexOf(index)];
        }

        public Type GetColumnType(int index)
        {
            if (!_indexes.Contains(index))
                return null;

            if (_types.Count <= 0)
                return null;

            return _types[_indexes.IndexOf(index)];
        }

        public Type GetColumnType(string name)
        {
            if (!_names.Contains(name))
                return null;

            if (_types.Count <= 0)
                return null;

            return _types[_names.IndexOf(name)];
        }

        public void SetColumnIndexes(IEnumerable<int> columnIndexes)
        {
            _indexes = columnIndexes.ToList();
        }
        
        public void SetColumnNames(IEnumerable<string> columnNames)
        {
            _names = columnNames.ToList();
        }

        public void SetColumnTypes(IEnumerable<Type> columnTypes)
        {
            _types = columnTypes.ToList();
        }

        public void SetValue(int index, object value)
        {
            if (!_indexValuePairs.ContainsKey(index))
                _indexValuePairs.Add(index, value);
        }

        public void SetValue(string name, object value)
        {
            if (!_names.Contains(name))
                return;

            SetValue(_names.IndexOf(name), value);
        }

        public void SetValues(IEnumerable<object> values)
        {
            if (_indexes.Count <= 0)
                return;

            _indexValuePairs.Clear();
            _indexValuePairs = _indexes.Zip(values, (index, value) => new { index, value }).ToDictionary(d => d.index, d => d.value);
        }

        public SqlResult ToSqlResult()
        {
            SqlResult sqlResult = new SqlResult();
            sqlResult.ColumnNames = _names;
            sqlResult.ColumnTypes = _types;
            sqlResult.RowValues = GetStringValues().ToList();
            return sqlResult;
        }

        IEnumerable<string> GetStringValues()
        {
            return _indexValuePairs.Values.Select(o => o.ToStringOrNull());
        }
    }
}
