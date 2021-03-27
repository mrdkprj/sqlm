using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MasudaManager.Utility
{
    public class SortComparer<T> : IComparer<T>
    {
        public PropertyDescriptor CurrentProperty { get; set; }
        public ListSortDirection CurrentDirection { get; set; }

        public int Compare(T x, T y)
        {
            object valueX = this.CurrentProperty.GetValue(x);
            object valueY = this.CurrentProperty.GetValue(y);

            IComparable comparableX = valueX as IComparable;
            IComparable comparableY = valueY as IComparable;

            if (comparableX == null && comparableY == null)
                return 0;

            if (comparableX == null && comparableY != null)
                return this.CurrentDirection == ListSortDirection.Ascending ? -1 : 1;

            if (comparableX != null && comparableY == null)
                return this.CurrentDirection == ListSortDirection.Ascending ? 1 : -1;

            if (this.CurrentDirection == ListSortDirection.Ascending)
                return comparableX.CompareTo(comparableY);

            return -comparableX.CompareTo(comparableY);
        }
    }
}
