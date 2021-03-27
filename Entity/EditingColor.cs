using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class EditingColor
    {
        Brush _defaultColor = new SolidBrush(Color.Empty);
        Brush _updateColor = null;
        Brush _deleteColor = null;
        Brush _insertColor = null;

        public Brush DefaultColor
        {
            get { return _defaultColor; }
            set { _defaultColor = value; }
        }

        public Brush UpdateColor
        {
            get { return _updateColor ?? _defaultColor; }
            set { _updateColor = value; }
        }

        public Brush DeleteColor
        {
            get { return _deleteColor ?? _defaultColor; }
            set { _deleteColor = value; }
        }

        public Brush InsertColor
        {
            get { return _insertColor ?? _defaultColor; }
            set { _insertColor = value; }
        }

        public EditType GetEditType(Brush brush)
        {
            if (brush == _updateColor)
                return EditType.Update;

            if (brush == _deleteColor)
                return EditType.Delete;

            if (brush == _insertColor)
                return EditType.Insert;

            throw new Exception("No applicable edit type found");
        }
    }
}
