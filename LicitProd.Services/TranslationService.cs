using System;
using System.Collections.Generic;

namespace LicitProd.Services
{
    public static class TranslationService
    {
        private static List<Action<Translations>> _changeLanguageCallback = new List<Action<Translations>>();
        private static Translations _translations;
        public static Response<Translations> GetTranslation()
        {
            if (_translations == null)
                return Response<Translations>.Error();
            return Response<Translations>.Ok(_translations);
        }
        public static void Subscribe(Action<Translations> changeLanguage)
        {
            _changeLanguageCallback.Add(changeLanguage);
        }
        public static void ChangeLanguage(Translations translation)
        {
            _translations = translation;
            _changeLanguageCallback.ForEach(callback => callback(translation));
        }
    }
}
