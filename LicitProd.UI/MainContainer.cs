using LicitProd.Entities;
using LicitProd.Services;
using System;
using System.Collections.Generic;
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
            TranslationService.ChangeLanguage(new Translations(new List<Services.Translation> {
                new Services.Translation("DashboardProveedoresLabelTituloPrincipal", "Dashboard Providers"),
                new Services.Translation("DashboardProveedoresTitle", "Dashboard Providers"),
                new Services.Translation("LogsTitle", "List of Logs"),
                new Services.Translation("LogsBtnBuscar", "Search")

            }));
        }

        private void EspañolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TranslationService.ChangeLanguage(new Translations(new List<Services.Translation> {
                new Services.Translation("DashboardProveedoresLabelTituloPrincipal", "Dashboard Proveedores"),
                new Services.Translation("DashboardProveedoresTitle", "Dashboard Proveedores"),
                new Services.Translation("LogsTitle", "Lista de Logs"),
                new Services.Translation("LogsBtnBuscar", "Buscar")                
            }));
        }
    }
}
