using LicitProd.Entities;

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
