using System.Collections.Generic;

namespace MasudaManager.Utility
{
    public interface IOriginator<T> : IEnumerable<T>
    {
        void Add(T item);

        void AddRange(IEnumerable<T> items);

        IMemento<T> CreateMemento();

        void Clear();

        int Count();

        void RestoreMemento(IMemento<T> memento);

        void RestoreUndo(IMemento<T> memento);

        void RestoreRedo(IMemento<T> memento);

        IEnumerable<T> Reassemble(IEnumerable<T> source);
    }
}
