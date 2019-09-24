using System;
using Windows.UI.Popups;

namespace LicitProd.UI.Uwp.Services
{
    public static class NavigationService
    {

        private static Action<Type> _navigateCallback;
        private static Action<Type> _navigatePop;

        public static void Register(Action<Type> navigateCallback,
            Action<Type> navigatePop)
        {
            _navigateCallback = navigateCallback;
            _navigatePop = navigatePop;
        }
        public static void Navigate<TPage>( ) => _navigateCallback?.Invoke(typeof(TPage));
        public static void NavigatePop<TPage>( ) => _navigatePop?.Invoke(typeof(TPage));
    }
    public static class MessageDialogService
    {


        public static void Create(string message,
                                  Action<IUICommand> commandAccept = null, 
                                  Action<IUICommand> commandCancel = null)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog(message);
            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            if (commandAccept != null)
                messageDialog.Commands.Add(new UICommand(
                   "Aceptar",
                   new UICommandInvokedHandler(commandAccept)));
            if (commandCancel != null)
                messageDialog.Commands.Add(new UICommand(
                "Close",
                new UICommandInvokedHandler(commandCancel)));


            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            messageDialog.ShowAsync();
        }


    }
    public static class LoadingService
    {
        private static Action _callbackLoadingStart;
        private static Action _callbackLoadingStop;

        public static void SetLoadingCallBack(Action callbackLoadingStart,
            Action callbackLoadingStop)
        {
            _callbackLoadingStart = callbackLoadingStart;
            _callbackLoadingStop = callbackLoadingStop;
        }

        public static void LoadingStart()
        {
            _callbackLoadingStart?.Invoke();
        }
        public static void LoadingStop()
        {
            _callbackLoadingStop?.Invoke();
        }
    }
}
