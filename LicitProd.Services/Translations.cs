using System.Collections.Generic;
using System.Linq;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class Translations
    {
        private readonly Idioma _idioma;
        public IReadOnlyCollection<TraduccionValue> TranslationList => _idioma.Traducciones.AsReadOnly();

        public Translations(Idioma idioma)
        {
            _idioma = idioma;
        }

        public string GetTranslation(string key) =>
            TranslationList.FirstOrDefault(x => x.KeyValue == key)?.Traduccion;
    }
}
