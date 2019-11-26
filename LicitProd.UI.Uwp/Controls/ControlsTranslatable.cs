using System;
using LicitProd.Services;

namespace LicitProd.UI.Uwp.Controls
{
    public class ControlsTranslatable
    {
        private readonly Action<string> _changeValue;
        private string _translatableKey;

        public ControlsTranslatable(Action<string> changeValue)
        {
            _changeValue = changeValue;
            TranslationService.Subscribe(trans => ChangeLanguage(trans, changeValue));
        }

        private void ChangeLanguage(Translations trans, Action<string> changeValue)
        {
            if (!string.IsNullOrWhiteSpace(_translatableKey))
            {
                var value = trans.GetTranslation(_translatableKey);

                changeValue(value);
            }
        }


        public string TranslatableKey
        {
            get => _translatableKey;
            set
            {
                if (string.IsNullOrWhiteSpace(_translatableKey))
                    TranslationService.GetTranslation()
                        .Success(t =>
                        {
                            var tra = t.GetTranslation(value);
                            _changeValue(tra);
                        });
                _translatableKey = value;
            }
        }
    }
}