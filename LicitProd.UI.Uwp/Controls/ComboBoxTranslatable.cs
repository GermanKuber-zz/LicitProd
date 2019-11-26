using Windows.UI.Xaml.Controls;

namespace LicitProd.UI.Uwp.Controls
{
    public class ComboBoxTranslatable : ComboBox
    {
        private readonly ControlsTranslatable _controlsTranslatable;

        public ComboBoxTranslatable()
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