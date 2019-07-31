using LicitProd.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public partial class MainContainer : Form
    {
        public MainContainer()
        {
            InitializeComponent();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UsuarioService().Logout();
            this.Hide();
            new Login().Show();
        }
    }
}
