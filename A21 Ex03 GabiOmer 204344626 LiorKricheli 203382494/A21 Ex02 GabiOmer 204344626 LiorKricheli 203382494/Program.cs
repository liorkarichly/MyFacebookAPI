using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;



// $G$ THE-001 (-32) your grade on diagrams document - 68 please see comments inside the document. (50% of your grade).



namespace A21_Ex03_GabiOmer_204344626_LiorKricheli_203382494
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
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());

        }

    }

}
