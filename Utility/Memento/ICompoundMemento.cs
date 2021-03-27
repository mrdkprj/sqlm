using System.Collections.Generic;

namespace MasudaManager.Utility
{
    public interface ICompoundMemento<T> : IMemento<T>, IEnumerable<IMemento<T>>
    {
        void Clear();
        void Add(IMemento<T> memento);
        void Remove(IMemento<T> memento);
    }
}
