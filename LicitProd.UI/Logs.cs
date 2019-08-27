using LicitProd.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public partial class Logs : Form
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
            var logs = new LogRepository().Get();
            var source = new BindingSource();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            source.DataSource = logs;
            dataGridView1.DataSource = source;
        }
    }
}
