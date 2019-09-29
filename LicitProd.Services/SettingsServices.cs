using LicitProd.Entities;

namespace LicitProd.Services
{
    public static class SettingsServices
    {

        public static Idioma Idioma;
        public static void SetIdioma(Idioma idioma)
        {
            Idioma = idioma;
            TranslationService.ChangeLanguage(new Translations(idioma));
        }

    }
}