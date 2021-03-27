using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasudaManager.Controls
{
    public partial class XTabPage : TabPage
    {
        public XTabPage()
        {
            InitializeComponent();
        }

        public XTabPage(string text)
            : base(text)
        {
        }

        public object Guid { get; set; }
        public string InfoMessage { get; set; }
    }
}
