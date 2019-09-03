using LicitProd.Data;
using System;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public partial class Logs : BaseForm
    {
        public Logs()
        {
            InitializeComponent();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Logs_Load(object sender, EventArgs e)
        {
            new LogRepository().Get()
               .Success(logs =>
               {
                   var source = new BindingSource();
                   dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                   source.DataSource = logs;
                   dataGridView1.DataSource = source;
               })
               .Error(x => MessageBox.Show("No hay logs"));
        }
    }
}
