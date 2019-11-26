using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LicitProd.Services;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.UI.Uwp.Services;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Animation;
using LicitProd.Data;
using LicitProd.Data.Repositories;
using LicitProd.Entities;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public bool EnableToLogin { get; set; } = false;
        private string password = string.Empty;
        private string email = string.Empty;
        private Usuario usuarioSelected;

        public string Email
        {
            get => email; set
            {
                email = value;
                CheckIfIfEnableToLogin();
            }
        }
        public string Password
        {
            get => password; set
            {
                password = value;
                CheckIfIfEnableToLogin();

            }
        }
        public ObservableCollection<Usuario> Usuarios { get; set; } = new ObservableCollection<Usuario>();
        public Usuario UsuarioSelected
        {
            get => usuarioSelected; set
            {
                usuarioSelected = value;
                CheckIfIfEnableToLogin();
            }
        }
        private readonly Regex _validEmailRegex = new Regex(
                                      @"^(([^<>()[\]\\.,;:\s@\""]+"
                                      + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                      + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                      + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                      + @"[a-zA-Z]{2,}))$",
                                      RegexOptions.Compiled);


        public Login()
        {
            InitializeComponent();
            //Task.Run(async () => await ValidateConsistency()).Wait();
            LocalDb.CreateLocalDb("LicitProd2", new List<string> { "Creation.sql" }, true);
            LoadData();
        }
        private void CheckIfIfEnableToLogin()
        {
            if (!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(email))
                btnLogin.IsEnabled = true;
            else
                btnLogin.IsEnabled = false;

            if (UsuarioSelected != null)
                btnLogin.IsEnabled = true;

        }
        private async Task LoadData()
        {
            (await new UsuarioRepository().Get())
                .Success(users => users?.ForEach(x => Usuarios.Add(x)));
        }

        public async Task<Response<string>> ValidateConsistency()
        {
            LoadingService.LoadingStart();
            return (await new ConsistencyValidator().Validate())
                .Error(errors =>
               {

                   MessageDialogService.Create("La base de datos se encuentra corrompida. Verifique con el administrador", c =>
                   {
                       LoadingService.LoadingStop();
                       Application.Current.Exit();

                   }, null);
               });
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Loading.IsLoading = true;
            var consistency = await ValidateConsistency();

            if (consistency.SuccessResult)
            {
                Response<Usuario> result;
                if (UsuarioSelected != null)
                    result = (await new UsuarioService().LoginAsync(UsuarioSelected.Email));
                else
                    result = (await new UsuarioService().LoginAsync(Email, Password));

                if (result.SuccessResult)
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                        CoreDispatcherPriority.Normal,
                        () => Frame.Navigate(typeof(MainContainerPage), null, new DrillInNavigationTransitionInfo()));
                }
                else
                {
                    MessageDialogService.Create("Verifique los datos ingresados!!");
                    //Loading.IsLoading = false;

                }

            }
            //().Success(async s =>
            //{





            //#if !DEBUG
            //                        if (validEmailRegex.IsMatch(txtEmail.Text))
            //                        {
            //#endif
            //                (await new UsuarioService()
            //#if DEBUG
            //                             .LoginAsync("german.kuber@outlook.com"))
            //#else
            //                             .Login(txtEmail.Text, txtPassword.Text)
            //#endif
            //                             .Success(usuario =>
            //                             {

            //                                 Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            //                                     () =>
            //                                     {
            //                                         Frame.Navigate(typeof(MainContainerPage));

            //                                     }
            //                                 );

            //                             })
            //                     .Error(errors =>
            //                     {
            //                         MessageDialogService.Create("Verifique los datos ingresados!!");
            //                     });
            //#if !DEBUG
            //                        }
            //                        else
            //                        {
            //                            MessageBox.Show("No ingreso un email valido", "Error!");
            //                            return;
            //                        }
            //#endif
            //});
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {


        }


    }
}
