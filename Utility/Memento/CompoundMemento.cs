using System.Collections.Generic;

namespace MasudaManager.Utility
{
    public class CompoundMemento<T> : ICompoundMemento<T>
    {
        List<IMemento<T>> _mementoList = new List<IMemento<T>>();

        public CompoundMemento() { }

        public CompoundMemento(IMemento<T> memento)
        {
            _mementoList.Add(memento);
        }

        public void Clear()
        {
            _mementoList.Clear();
        }

        public void Add(IMemento<T> memento)
        {
            _mementoList.Add(memento);
        }

        public void Remove(IMemento<T> memento)
        {
            _mementoList.Remove(memento);
        }

        public IEnumerator<IMemento<T>> GetEnumerator()
        {
            return _mementoList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T Restore()
        {
            return default(T);
        }
    }
}
