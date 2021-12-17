using FacebookWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A21_Ex01_Gabi_204344626_Lior_203382494
{

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            FacebookService.s_UseForamttedToStrings = true;
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

    }

}
