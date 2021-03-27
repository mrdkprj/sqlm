using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MasudaManager.Utility
{
    public abstract class DynamicSortableBindingList<TSource, TResult> : SortableBindingList<TResult>, ITypedList
    {
        protected List<string> _propertyNames = new List<string>();
        protected List<Type> _propertyTypes = new List<Type>();

        public DynamicSortableBindingList(ISynchronizeInvoke syncObject)
            : base(syncObject)
        {
        }

        public List<string> PropertyNames { get { return _propertyNames; } }

        public List<Type> PropertyTypes { get { return _propertyTypes; } }

        public abstract int RowCount { get; }


        public abstract void AddSource(TSource source);

        public abstract void AddSourceRange(IEnumerable<TSource> sourceItems);
        
        public abstract TResult CreateEmptyResult();

        public abstract TResult CreateResultWithValues(IEnumerable<object> orderedValues);

        public abstract void Filter(int columnIndex, string filterString);

        public abstract string GetFieldValue(Cell cell);

        public abstract string GetFieldValue(int rowIndex, int columnIndex);

        public abstract PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);

        public abstract string GetListName(PropertyDescriptor[] listAccessors);
        
        public abstract TSource GetSourceAt(int index);
        
        public abstract void SetFieldValue(Cell cell, string value);

        public abstract void SetFieldValue(int rowIndex, int columnIndex, string value);

        public abstract List<TSource> ToSourceList();

        public abstract List<TSource> ToSavedSourceList();
    }
}
