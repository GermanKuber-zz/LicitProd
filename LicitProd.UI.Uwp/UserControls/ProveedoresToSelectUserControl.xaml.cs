using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Pages.Concursos;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LicitProd.UI.Uwp.UserControls
{
    public sealed partial class ProveedoresToSelectUserControl : UserControl
    {
        public ObservableCollection<ProveedorSelectionViewModel> InternalProveedores { get; set; } = new ObservableCollection<ProveedorSelectionViewModel>();
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Proveedores", typeof(List<Proveedor>), typeof(ProveedoresToSelectUserControl), new PropertyMetadata(""));
        public List<Proveedor> Proveedores { 
            set
            {

                value.ForEach(x=> InternalProveedores.Add(new  ProveedorSelectionViewModel(x)));


            }
        }

        public ProveedoresToSelectUserControl()
        {
            this.InitializeComponent();
        }
    }
}
