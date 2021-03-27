using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace MasudaManager
{
    public class EditorColorData : IEquatable<EditorColorData>
    {
        [Category("HighlightColor")]
        public Color BracketForeColor { get; set; }

        [Category("HighlightColor")]
        public Color BracketBackColor { get; set; }
        
        [Category("HighlightColor")]
        public Color Comment { get; set; }

        [Category("HighlightColor")]
        public Color Character { get; set; }

        [Category("HighlightColor")]
        public Color Number { get; set; }

        [Category("HighlightColor")]
        public Color String { get; set; }

        [Category("HighlightColor")]
        public Color Operator { get; set; }

        [Category("HighlightColor")]
        public Color Keyword { get; set; }

        public bool Equals(EditorColorData other)
        {
            if (this.BracketBackColor != other.BracketBackColor)
                return false;

            if (this.BracketForeColor != other.BracketForeColor)
                return false;

            if (this.Character != other.Character)
                return false;

            if (this.Comment != other.Comment)
                return false;

            if (this.Keyword != other.Keyword)
                return false;

            if (this.Number != other.Number)
                return false;

            if (this.Operator != other.Operator)
                return false;

            if (this.String != other.String)
                return false;

            return true;
        }
    }
}
