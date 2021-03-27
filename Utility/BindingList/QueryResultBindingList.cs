using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MasudaManager.Utility
{
    
    public class QueryResultBindingList : DynamicSortableBindingList<SqlResult, QueryResult>
    {       
        int _rowCount = 0;

        public QueryResultBindingList(ISynchronizeInvoke syncObject)
            : base(syncObject)
        {
            _rowCount = 0;
        }

        public override int RowCount { get { return _rowCount; } }

        public IEnumerable<QueryResult> QueryResults { get { return this.Items; } }

        #region CreateEmptyResult
        public override QueryResult CreateEmptyResult()
        {
            QueryResult emptyResult = new QueryResult();
            for (int i = 0; i < _propertyNames.Count; i++)
            {
                emptyResult[i] = null;
            }
            return emptyResult;
        }
        #endregion

        #region CreateResultWithValues
        public override QueryResult CreateResultWithValues(IEnumerable<object> orderedValues)
        {
            if (orderedValues == null)
                return CreateEmptyResult();

            var orderedValueList = orderedValues.ToList();
            QueryResult result = new QueryResult();
            for (int i = 0; i < orderedValueList.Count; i++)
            {
                result[i] = orderedValueList[i].ToStringOrNull();
            }
            return result;

        }
        #endregion

        #region Get field value
        public override string GetFieldValue(Cell cell)
        {
            return this.Items[cell.RowIndex][cell.ColumnIndex];
        }

        public override string GetFieldValue(int rowIndex, int columnIndex)
        {
            return this.Items[rowIndex][columnIndex];
        }
        #endregion

        #region Set field value
        public override void SetFieldValue(Cell cell, string value)
        {
            this.Items[cell.RowIndex][cell.ColumnIndex] = value;
        }

        public override void SetFieldValue(int rowIndex, int columnIndex, string value)
        {
            this.Items[rowIndex][columnIndex] = value;
        }
        #endregion

        #region AddSource
        public override void AddSource(SqlResult source)
        {
            if (!CanAdd(source))
                return;

            SetProperty(source);

            if (source.RowValues.Count != _propertyNames.Count)
                return;

            Add(GetQueryResult(source));

            _rowCount++;
        }

        bool CanAdd(SqlResult source)
        {
            if (source == null)
                throw new ArgumentNullException("Source is null");

            if (source.ColumnNames.Count <= 0)
                return false;

            return true;
        }
        
        void SetProperty(SqlResult source)
        {
            if (_propertyNames.Count <= 0)
            {
                _propertyNames = source.ColumnNames;
                _propertyTypes = source.ColumnTypes;
            }
        }

        QueryResult GetQueryResult(SqlResult sqlResult)
        {
            var queryResult = new QueryResult();

            for (int i = 0; i < sqlResult.RowValues.Count; i++)
            {
                queryResult[i] = sqlResult.RowValues[i];
            }

            return queryResult;
        }

        #endregion

        #region AddSourceRange
        public override void AddSourceRange(IEnumerable<SqlResult> sourceItems)
        {
            foreach (var sourceItem in sourceItems)
            {
                this.AddSource(sourceItem);
            }
        }
        #endregion

        #region Filtering
        public override void Filter(string propertyName, string filterString)
        {
            if (String.IsNullOrEmpty(filterString))
                Restore();
            else
                FilterItems(_propertyNames.IndexOf(propertyName), filterString);
        }

        public override void Filter(int columnIndex, string filterString)
        {
            if (String.IsNullOrEmpty(filterString))
                Restore();
            else
                FilterItems(columnIndex, filterString);
        }

        void FilterItems(int filterIndex, string filterString)
        {
            var filteredItems = this.SavedList.Where(s => SearchStringComparator.Compare(s.Values.ElementAt(filterIndex), filterString));

            this.Items.Clear();
            AddRange(filteredItems);
        }

        public override void Save()
        {
            this.SavedList = new List<QueryResult>(this.Items);
            this.CanRestore = true;
        }

        public override void Restore()
        {
            this.Items.Clear();
            AddRange(this.SavedList);
            this.CanRestore = false;
        }
        #endregion

        #region ITypedList implementation
        public override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors == null || listAccessors.Length == 0)
            {
                PropertyDescriptor[] properties = new PropertyDescriptor[_propertyNames.Count];

                for (int i = 0; i < properties.Length; i++)
                {
                    var property = new QueryResultPropertyDescriptor(_propertyNames[i], i);

                    property.SetPropertyType(_propertyTypes[i]);
                    properties[i] = property;
                }

                return new PropertyDescriptorCollection(properties, true);
            }

            throw new NotImplementedException("Relations not implemented");
        }

        public override string GetListName(PropertyDescriptor[] listAccessors)
        {
            return this.Items.ToString();
        }
        #endregion

        #region Source
        public override SqlResult GetSourceAt(int index)
        {
            return new SqlResult
            {
                RowValues = this.Items[index].Values.ToList(),
                ColumnNames = _propertyNames, 
                ColumnTypes = _propertyTypes, 
                AffectedRowCount = 0
            };
        }

        public override List<SqlResult> ToSourceList()
        {
            var resultSetList = this.Items.Select(item => new SqlResult 
            {
                RowValues = item.Values.ToList(),
                ColumnNames = _propertyNames, 
                ColumnTypes = _propertyTypes, 
                AffectedRowCount = 0 
            });

            return resultSetList.ToList();
        }

        public override List<SqlResult> ToSavedSourceList()
        {
            var resultSetList = this.SavedList.Select(item => new SqlResult
            {
                RowValues = item.Values.ToList(),
                ColumnNames = _propertyNames,
                ColumnTypes = _propertyTypes,
                AffectedRowCount = 0
            });

            return resultSetList.ToList();
        }
        #endregion

    }
}
