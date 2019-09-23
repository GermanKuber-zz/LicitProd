using System;
using System.Collections.Generic;
using System.Linq;

namespace LicitProd.Services
{
    public class Translations
    {
        public List<Translation> TranslationList = new List<Translation>();

        public Translations(List<Translation> translations)
        {
            TranslationList = translations ?? throw new ArgumentNullException(nameof(translations));
        }

        public string GetTranslation(string key) =>
            TranslationList.First(x => x.Key == key).Value;
    }
}
