using System;
using LicitProd.Services;
using Microsoft.Toolkit.Uwp.UI.Controls;

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
            var value = trans.GetTranslation(_translatableKey);
            changeValue(value);
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

    public class DataGridTextColumnTranslatable : DataGridTextColumn
    {
        private readonly ControlsTranslatable _controlsTranslatable;

        public DataGridTextColumnTranslatable()
        {
            _controlsTranslatable = new ControlsTranslatable(ChangeLanguage);

        }

        private void ChangeLanguage(string value)
        {
            Header = value;
        }


        public string TranslatableKey
        {
            get => _controlsTranslatable.TranslatableKey;
            set => _controlsTranslatable.TranslatableKey = value;
        }
    }
}