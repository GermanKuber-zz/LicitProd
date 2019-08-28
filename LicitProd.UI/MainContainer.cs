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
            new LogManager().LogInformacion("Logout");

            new UsuarioService().Logout();
            this.Hide();
            new Login().Show();
        }

        private void LogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var logs = new Logs();
            logs.MdiParent = this;
            logs.Show();
        }

        private void ProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var logs = new DashboardProveedores();
            logs.MdiParent = this;
            logs.Show();
        }
    }
}
