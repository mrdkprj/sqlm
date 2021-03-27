using System;
using System.Collections.Generic;
using System.Linq;

namespace MasudaManager.Utility
{
    public class EditResultHistory : IHistory<EditData>
    {
        Stack<IMemento<EditData>> _undoStack;
        Stack<IMemento<EditData>> _redoStack;
        CompoundMemento<EditData> _compoundMemento;

        public bool CanRedo { get { return _redoStack.Count > 0; } }
        public bool CanUndo { get { return _undoStack.Count > 0; } }

        public EditResultHistory(int capacity)
        {
            _undoStack = new Stack<IMemento<EditData>>(capacity);
            _redoStack = new Stack<IMemento<EditData>>(capacity);
        }

        public void BeginDo()
        {
            _compoundMemento = new CompoundMemento<EditData>();
        }

        public void EndDo()
        {
            ApplyDo(_compoundMemento);
            _compoundMemento = null;
        }

        public void CancelDo()
        {
            _compoundMemento = null;
        }

        public void Do(IOriginator<EditData> originater)
        {
            if (_compoundMemento == null)
                ApplyDo(originater.CreateMemento());
            else
                _compoundMemento.Add(originater.CreateMemento());
        }

        void ApplyDo(IMemento<EditData> memento)
        {
            _redoStack.Clear();
            _undoStack.Push(memento);
        }

        public void Undo(IOriginator<EditData> originater)
        {
            var topMemento = _undoStack.Pop();
            var compoundMemento = GetCompoundMemento(topMemento);
            foreach (var memento in compoundMemento.Reverse())
            {
                originater.RestoreUndo(memento);
            }
            _redoStack.Push(topMemento);
        }

        public void Redo(IOriginator<EditData> originater)
        {
            var topMemento = _redoStack.Pop();
            var compoundMemento = GetCompoundMemento(topMemento);
            foreach (var memento in compoundMemento)
            {
                originater.RestoreRedo(memento);
            }
            _undoStack.Push(topMemento);
        }
        
        CompoundMemento<EditData> GetCompoundMemento(IMemento<EditData> memento)
        {
            if (memento is CompoundMemento<EditData>)
                return memento as CompoundMemento<EditData>;
            else
                return new CompoundMemento<EditData>(memento);

        }

        public void Clear()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        public void Restore(IOriginator<EditData> originater)
        {
            originater.Clear();

            foreach (var stackedMemento in _undoStack)
            {
                foreach (var memento in GetCompoundMemento(stackedMemento))
                {
                    originater.RestoreMemento(memento);
                }
            }
        }

    }
}
