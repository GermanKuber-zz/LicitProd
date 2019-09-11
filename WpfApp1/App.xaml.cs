using MvvmCross.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Core;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        {
            this.RegisterSetupType<MvxWpfSetup<ViewModels.App>>();
        }
    }
}