using LicitProd.UI.Uwp.Pages;
using LicitProd.UI.Uwp.Services;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using LicitProd.Data.Infrastructure.DataBase;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace LicitProd.UI.Uwp
{
    sealed partial class App : Application
    {
        public App()
        {

            InitializeComponent();
            Suspending += OnSuspending;
            AppCenter.Start("1fbb0789-1f0f-4157-8181-9b52ef1846e7",
                typeof(Analytics), typeof(Crashes));
            AppCenter.Start("1fbb0789-1f0f-4157-8181-9b52ef1846e7",
                typeof(Analytics), typeof(Crashes));
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            new Migrations().Run();
            Frame rootFrame = Window.Current.Content as Frame;
            UnhandledException += App_UnhandledException;
            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(Login), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        private void App_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            LoadingService.LoadingStart();
            MessageDialogService.Create("Error Global. Contacte al administrador", c =>
            {
                LoadingService.LoadingStop();
                NavigationService.NavigatePop<Dashboard>();
            }, null);
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
