using LicitProd.Entities;
using LicitProd.Services;
using System;
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

        private void InglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TranslationService.ChangeLanguage(new Translation
            {
                DashboardProveedoresLabelTituloPrincipal = "Dashboard Providers"
            });
        }

        private void EspañolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TranslationService.ChangeLanguage(new Translation
            {
                DashboardProveedoresLabelTituloPrincipal = "Dashboard Proveedores"
            });
        }
    }
}
