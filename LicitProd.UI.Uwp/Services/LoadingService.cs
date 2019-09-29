using System;

namespace LicitProd.UI.Uwp.Services
{
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
