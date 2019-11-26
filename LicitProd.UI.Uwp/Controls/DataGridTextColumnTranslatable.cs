using Microsoft.Toolkit.Uwp.UI.Controls;

namespace LicitProd.UI.Uwp.Controls
{
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