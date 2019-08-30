using LicitProd.Entities;
using LicitProd.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public partial class MainContainer : Form
    {
        private Translations translation;

        public MainContainer()
        {
            TranslationService.Subscribe(trans =>
            {
                translation = trans;
                ChangeLanguage(trans);
            });
            InitializeComponent();
        }
        protected override void InitLayout()
        {
            base.InitLayout();
            CheckTraducciones();
        }
        private void CheckTraducciones() {
            TranslationService.GetTranslation()
                         .Success(x => ChangeLanguage(x));
        }
        protected override void OnMdiChildActivate(EventArgs e)
        {
            base.OnMdiChildActivate(e);
            CheckTraducciones();

        }

        public void ChangeLanguage(Translations translation)
        {
            translation._translations.ForEach(x =>
            {
                var controls = this.Controls.Find(x.Key, true);
                foreach (var control in controls)
                {
                    if (control is Label)
                        control.Text = x.Value;
                    if (control is Button)
                        control.Text = x.Value;
                }
            });
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
