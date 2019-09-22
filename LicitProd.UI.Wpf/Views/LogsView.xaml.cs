using LicitProd.Data;
using System.Windows;
using System.Windows.Controls;

namespace LicitProd.UI.Wpf.Views
{
    /// <summary>
    /// Interaction logic for LogsView.xaml
    /// </summary>
    public partial class LogsView : UserControl
    {
        public LogsView()
        {
            InitializeComponent();
            new LogRepository().Get()
            .Success(logs =>
            {
                dgLogs.ItemsSource = logs;
            })
            .Error(x => MessageBox.Show("No hay logs"));
        }
    }
}
