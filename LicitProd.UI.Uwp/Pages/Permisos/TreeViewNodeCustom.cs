using LicitProd.Entities;
using Microsoft.UI.Xaml.Controls;

namespace LicitProd.UI.Uwp.Pages.Permisos
{
    public class TreeViewNodeCustom : TreeViewNode
    {
        public Permission Data { get; set; }

        public TreeViewNodeCustom(Permission permiso) : base()
        {
            Content = permiso.Nombre.ToString();
            Data = permiso;
        }

        public TreeViewNodeCustom()
        {

        }
    }
}