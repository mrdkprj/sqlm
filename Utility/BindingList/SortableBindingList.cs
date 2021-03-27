using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MasudaManager.Utility
{
    public class SortableBindingList<T> : BindingList<T>, IList<T>
    {
        ISynchronizeInvoke _syncObject;
        Action<ListChangedEventArgs> _listChanged;
        SortComparer<T> _sortComparer = new SortComparer<T>();
        List<T> _savedList = new List<T>();

        public SortableBindingList(ISynchronizeInvoke syncObject)
        {
            _syncObject = syncObject;
            _listChanged = base.OnListChanged;
            
            this.SupportSort = true;
            this.RaiseListChangedEvents = true;
        }

        public bool CanRestore { get; set; }
        public virtual bool SupportSort { get; set; }
        protected override bool SupportsSortingCore { get { return this.SupportSort; } }
        protected List<T> SavedList
        {
            get { return _savedList; }
            set { _savedList = value; }
        }

        protected override void OnListChanged(ListChangedEventArgs args)
        {
            if (_syncObject == null)
                base.OnListChanged(args);
            else
                _syncObject.Invoke(_listChanged, new object[] { args });
        }
        
        public void AddRange(IEnumerable<T> items)
        {
            List<T> list = (List<T>)this.Items;
            list.AddRange(items);
            ResetBindings();
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            List<T> list = (List<T>)this.Items;
            list.InsertRange(index, collection);
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        public void RemoveAll(Predicate<T> match)
        {
            List<T> list = (List<T>)this.Items;
            list.RemoveAll(match);
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        #region Sort
        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            List<T> items = new List<T>(this.Items);
            _sortComparer.CurrentProperty = property;
            _sortComparer.CurrentDirection = direction;
            this.Items.Clear();

            this.RaiseListChangedEvents = false;
            this.AddRange(items.OrderBy(x => x, _sortComparer));
            this.RaiseListChangedEvents = true;
        }

        protected override void RemoveSortCore()
        {

        }
        #endregion

        #region Filter
        public virtual void Filter(string propertyName, string filterString)
        {
            if (String.IsNullOrEmpty(filterString))
                Restore();
            else
                FilterItems(propertyName, filterString);
        }

        void FilterItems(string propertyName, string filterString)
        {
            var filteredItems = _savedList.Where(item => CompareSearchString(GetPropertyDataString(item, propertyName), filterString));

            this.Items.Clear();
            this.AddRange(filteredItems);
        }

        bool CompareSearchString(string right, string left)
        {
            return SearchStringComparator.Compare(right, left);
        }

        string GetPropertyDataString(T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item).ToString();
        }

        public virtual void Save()
        {
            _savedList = new List<T>(this.Items);
            this.CanRestore = true;
        }

        public virtual void Restore()
        {
            this.Items.Clear();
            this.AddRange(_savedList);
            this.CanRestore = false;
        }


        #endregion
    }
}
