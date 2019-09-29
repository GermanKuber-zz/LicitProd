using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Services;

namespace LicitProd.UI.Uwp.Controls
{
    public class TextBoxTranslatable : TextBox
    {
        private readonly ControlsTranslatable _controlsTranslatable;

        public TextBoxTranslatable()
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