using LicitProd.Data;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public partial class DashboardProveedores : Form
    {
        public DashboardProveedores()
        {
            InitializeComponent();

            var proveedoresRepository = new ProveedoresRepository();
            var a = proveedoresRepository.Get();
        }
    }
}
