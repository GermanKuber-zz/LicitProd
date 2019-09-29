using System;

namespace LicitProd.UI.Uwp.Services
{
    public static class NavigationService
    {

        private static Action<Type> _navigateCallback;
        private static Action<Type> _navigatePop;
        private static Action _closeCallBack;

        public static void Register(Action<Type> navigateCallback,
            Action<Type> navigatePop,
            Action closeCallBack)
        {
            _navigateCallback = navigateCallback;
            _navigatePop = navigatePop;
            _closeCallBack = closeCallBack;
        }
        public static void Navigate<TPage>() => _navigateCallback?.Invoke(typeof(TPage));
        public static void NavigatePop<TPage>() => _navigatePop?.Invoke(typeof(TPage));
        public static void Close() => _closeCallBack?.Invoke();
    }
}