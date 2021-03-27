using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MasudaManager
{
    public class EditData : IEquatable<EditData>
    {
        readonly string _toStringFormat = "{0]: Cell={1}, BackColor={2}, Value={3}";
        List<EditData> _children = null;

        public EditData()
        {
        }

        public EditData(EditType type, Cell cell, Brush backColor, object cellValue)
        {
            this.Type = type;
            this.Cell = cell;
            this.BackColor = backColor;
            this.Value = cellValue;
        }

        public EditType Type { get; set; }
        public Cell Cell { get; set; }
        public Brush BackColor { get; set; }
        public object Value { get; set; }
        public List<EditData> Children { get { return _children; } }
        public bool HasChildren { get { return _children != null; } }
        public EditData UndoEditData { get; set; }

        public void Add(EditData child)
        {
            _children = _children.LazyInitialize();
            _children.Add(child);
        }

        public void AddRange(IEnumerable<EditData> children)
        {
            _children = _children.LazyInitialize();
            _children.AddRange(children);
        }

        public bool Equals(EditData other)
        {
            if (this.Type != other.Type)
                return false;

            if (this.Cell != other.Cell)
                return false;

            if (this.BackColor != other.BackColor)
                return false;

            if (this.Value != other.Value)
                return false;

            return true;
        }

        public override string ToString()
        {
            return String.Format(_toStringFormat, this.Type.ToString(), this.Cell.ToString(), this.BackColor, this.Value);
        }
    }
}
