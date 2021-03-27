using System.Collections.Generic;
using System.Linq;
using System;

namespace MasudaManager.Utility
{
    public class EditResultEdition : IOriginator<EditData>
    {
        List<EditData> _editDataList = new List<EditData>();
        EditData _currentEditData;

        public void Add(EditData editData)
        {
            _editDataList.Add(editData);
            _currentEditData = editData;
        }

        public void AddRange(IEnumerable<EditData> items)
        {
            _editDataList.AddRange(items);
            _currentEditData = items.First();
        }

        public IMemento<EditData> CreateMemento()
        {
            return new EditResultMemento(_currentEditData);
        }

        public IMemento<EditData> CreateMemento(EditData item)
        {
            return new EditResultMemento(item);
        }

        public void RestoreRedo(IMemento<EditData> memento)
        {
            _editDataList.Add(memento.Restore());
        }

        public void RestoreUndo(IMemento<EditData> memento)
        {
            if (memento.Restore().UndoEditData.HasChildren)
                _editDataList.AddRange(memento.Restore().UndoEditData.Children);
            else
                _editDataList.Add(memento.Restore().UndoEditData);
        }

        public void Clear()
        {
            _editDataList.Clear();
        }

        public int Count()
        {
            return _editDataList.Count;
        }

        public void RestoreMemento(IMemento<EditData> memento)
        {
            _editDataList.Add(memento.Restore());
        }

        public IEnumerator<EditData> GetEnumerator()
        {
            return _editDataList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerable<EditData> Reassemble(IEnumerable<EditData> source)
        {
            return source.GroupBy(s => s.Cell.RowIndex).Select(s => Merge(s));
        }

        EditData Merge(IEnumerable<EditData> editDataList)
        {
            EditData headEditData = editDataList.First();

            if (headEditData.Type != EditType.Update)
                return headEditData;

            foreach (var editData in editDataList)
            {
                headEditData.Add(editData);
            }

            return headEditData;
        }
    }
}
