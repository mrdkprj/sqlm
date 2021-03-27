using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MasudaManager.Views;
using System.Reflection;
using System.IO;
using System.Globalization;
using System.Text;

namespace MasudaManager
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuView());
        }
    }
}