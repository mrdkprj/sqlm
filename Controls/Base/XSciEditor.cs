using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace MasudaManager.Controls
{
    public partial class XSciEditor : Scintilla
    {

        public XSciEditor()
        {
            InitializeComponent();
        }

        public bool DisplayControlChar { get; set; }
        
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (this.DisplayControlChar)
                return;

            if (e.KeyChar < 32)
            {
                //// Prevent control characters from getting inserted into the text buffer
                e.Handled = true;
                return;
            }
        }
    }
}
