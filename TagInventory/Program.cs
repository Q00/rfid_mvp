using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TagInventory
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainFrm mainFrm = new MainFrm();
            SubFrm subFrm = new SubFrm();
            mainFrm.Show();
            subFrm.Show();
            Application.Run();
        }
    }
}