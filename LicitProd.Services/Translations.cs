using System;
using System.Collections.Generic;
using System.Linq;

namespace LicitProd.Services
{
    public class Translations
    {
        public List<Translation> _translations = new List<Translation>();

        public Translations(List<Translation> translations)
        {
            _translations = translations ?? throw new ArgumentNullException(nameof(translations));
        }

        public string GetTranslation(string key) =>
            _translations.First(x => x.Key == key).Value;
    }
}
