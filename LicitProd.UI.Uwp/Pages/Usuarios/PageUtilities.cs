using System;
using LicitProd.UI.Uwp.Services;

namespace LicitProd.UI.Uwp.Pages.Usuarios
{
    public class PageUtilities
    {
        public void ShowMessageDialog(string message, Action callBackSuccess = null)
        {
            MessageDialogService.Create(message, c =>
            {
                callBackSuccess?.Invoke();
                LoadingService.LoadingStop();
            }, null);
        }
    }
}