using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using LicitProd.Mappers;

namespace LicitProd.UI.Uwp.Services
{
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
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => { messageDialog.ShowAsync(); });
        }


    }
}