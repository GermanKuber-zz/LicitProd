using LicitProd.Data;
using LicitProd.Entities;
using LicitProd.Services;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public partial class DashboardProveedores : BaseForm<IDashboardProveedoresForm>
    {
        public DashboardProveedores()
        {
            InitializeComponent();

        }

        public override void ChangeLanguage(IDashboardProveedoresForm translation)
        {
            lblTituloPrinciapal.Text = translation.DashboardProveedoresLabelTituloPrincipal;
        }
    }
}
