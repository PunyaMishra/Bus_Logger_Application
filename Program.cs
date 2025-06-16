using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bus_Logger_Application
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

       
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //Showing the Login Screen when the application starts
            Login_Screen Login_Screen = new Login_Screen();
            Login_Screen.Show();

            Application.Run();
        } 
    }
}
