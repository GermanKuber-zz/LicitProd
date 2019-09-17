using LicitProd.Data;
using LicitProd.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public partial class Permisos : Form
    {
        private bool _editando = false;
        private Usuario usuario;
        public Permisos()
        {
            InitializeComponent();
        }

        private void Permisos_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Email", "Email");
            treeView1.CheckBoxes = true;
            AddPermisos();
            new UsuarioRepository().Get()
               .Success(usuarios =>
               {
                   var source = new BindingSource();
                   dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                   source.DataSource = usuarios;
                   dataGridView1.DataSource = source;
               })
               .Error(x => MessageBox.Show("No hay logs"));
        }

        private void AddPermisos()
        {
            var permisosToAdd = new List<Permission>();
            permisosToAdd.AddRange(new RolRepository()
                                    .GetAll().Result);
            AddTreeViewChildNodes(permisosToAdd);
        }

        private void AddTreeViewChildNodes(List<Permission> roles)
        {
            foreach (var rol in roles)
                treeView1.Nodes.Add(AddParentNode(null, rol));
        }
        private TreeNode AddParentNode(TreeNode parentNode, Permission rol)
        {
            if (parentNode == null)
            {
                parentNode = new TreeNode(rol.Name.ToString());
            }
            if (rol is Rol)
            {

                foreach (var item in ((Rol)rol).Permissions)
                {
                    var permission = new TreeNode(item.Name.ToString());
                    parentNode.Nodes.Add(AddParentNode(permission, item));
                }

            }
            return parentNode;
        }

        private void TreeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!_editando)
                e.Cancel = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _editando = !_editando;
        }

        private void DataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;
            usuario = (Usuario)e.Row.DataBoundItem;
            
        }
    }
}
