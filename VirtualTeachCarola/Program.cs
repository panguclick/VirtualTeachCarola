using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualTeachCarola.Base;

namespace VirtualTeachCarola
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
            MainForm mainForm = new MainForm();
            Manager.GetInstance().MMainForm = mainForm;
            Manager.GetInstance().InitConfig();
            Application.Run(mainForm);
        }
    }
}
