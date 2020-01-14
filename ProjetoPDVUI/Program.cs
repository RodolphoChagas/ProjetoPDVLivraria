using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PetaPoco;

using ProjetoPDVModelos;
using ProjetoPDVDao;

namespace ProjetoPDVUI
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


            frmLogin frmlogin = new frmLogin();
            frmlogin.ShowDialog();
            if(frmlogin.LogonSuccessful)
            {
                Application.Run(new frmMenuPrincipal());
            }
        }
    }
}
