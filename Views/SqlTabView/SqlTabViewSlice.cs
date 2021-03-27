using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMvp.Forms;
using MasudaManager.Controls.Base;

namespace MasudaManager.Views
{
    public partial class SqlTabViewSlice : MvpUserControl<SqlTabModel>
    {
        public SqlTabViewSlice()
        {
            InitializeComponent();
        }
    }
}
