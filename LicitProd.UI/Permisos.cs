using LicitProd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LicitProd.Data.Repositories;
using System.Threading.Tasks;

namespace LicitProd.UI
{
    public partial class Permisos : Form
    {
        private bool _editando = false;
        private Usuario _usuario;
        public Permisos()
        {
            InitializeComponent();
        }

        private void Permisos_Load(object sender, EventArgs e)
        {
            ConfigurateDataGridView();
            AddPermisosAsync();
            FillAddUsuariosAsync();
        }

        private async Task FillAddUsuariosAsync() =>
            (await new UsuarioRepository().Get())
               .Success(usuarios =>
               {
                   var source = new BindingSource();
                   dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                   source.DataSource = usuarios;
                   dataGridView1.DataSource = source;
                   dataGridView1.Rows[0].Selected = true;
               })
               .Error(x => MessageBox.Show("No hay logs"));
        private void ConfigurateDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            AddColumn("Id");
            AddColumn("Email");

            treeView1.CheckBoxes = true;
        }
        private void AddColumn(string name, string header = null)
        {
            if (header == null)
                header = name;
            var dataGridViewColumn = new DataGridViewTextBoxColumn();

            dataGridViewColumn.DataPropertyName = name;
            dataGridViewColumn.HeaderText = header;
            dataGridViewColumn.Name = name;
            dataGridView1.Columns.Add(dataGridViewColumn);
        }
        private async Task AddPermisosAsync()
        {
            var permisosToAdd = new List<Permission>();
            permisosToAdd.AddRange((await new RolRepository()
                                    .GetAllAsync()).Result);
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
                parentNode = new TreeNode(rol.Nombre.ToString());

            }
            if (rol is Rol)
            {

                foreach (var item in ((Rol)rol).Permissions)
                {
                    var permission = new TreeNode(item.Nombre.ToString());
                    var nodeToAdd = AddParentNode(permission, item);
                    parentNode.Nodes.Add(nodeToAdd.Text, nodeToAdd.Text);
                }

            }
            return parentNode;
        }
        private async Task SelectPermissionsAsync()
        {
            if (_usuario == null)
                return;
            UnselectNodes(treeView1.Nodes);
            (await new RolRepository().GetByUsuarioIdAsync(_usuario.Id))
                  .Success(rol => ProccessPermission(rol));
        }
        private void UnselectNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode item in nodes)
            {
                if (item.Nodes.Count > 0)
                    UnselectNodes(item.Nodes);
                item.Checked = false;
            }
        }
        private void ProccessPermission(Permission permiso)
        {
            if (permiso is Rol)
            {
                var rol = (Rol)permiso;
                if (rol.Permissions.Any())
                    rol.Permissions.ForEach(p => ProccessPermission(p));
                CheckNode(rol.Nombre.ToString());
            }
            else if (permiso is SinglePermission)
                CheckNode(permiso.Nombre.ToString());
        }
        private void CheckNode(string name)
        {
            TreeNode node = default;
            var nodes = treeView1.Nodes.Find(name, true);
            if (nodes.Length > 0)
                node = nodes.First();
            if (node == null)
            {
                foreach (TreeNode item in treeView1.Nodes)
                {
                    if (item.Text == name)
                        node = item;
                };
            }
            if (node != null)
            {
                node.Checked = true;
            }
        }
        private TreeNode GetNode(string key, TreeNodeCollection nodes)
        {
            TreeNode n = null;
            if (nodes.ContainsKey(key))
                n = nodes[key];
            else
            {
                foreach (TreeNode tn in nodes)
                {
                    n = GetNode(key, tn.Nodes);
                    if (n != null) break;
                }
            }

            return n;
        }
        private void TreeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            //if (!_editando)
            //    e.Cancel = true;
        }
        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void UpdatePermissions(Permission permiso)
        {
            if (permiso is Rol)
            {
                var rol = (Rol)permiso;
                if (rol.Permissions.Any())
                    rol.Permissions.ForEach(p => UpdatePermissions(p));
                //if (ExistNode(rol.Name.ToString()))
            }
            else if (permiso is SinglePermission)
                CheckNode(permiso.Nombre.ToString());
        }
        List<Permission> _permisos = new List<Permission>();
        private bool ExistNode(string name)
        {
            TreeNode node = default;

            var nodes = treeView1.Nodes.Find(name, true);
            if (nodes.Length > 0)
                node = nodes.First();

            if (node != null)
                return true;
            return false;
        }
        private void DataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;
            _usuario = (Usuario)e.Row.DataBoundItem;
            SelectPermissionsAsync();

        }
    }
}
