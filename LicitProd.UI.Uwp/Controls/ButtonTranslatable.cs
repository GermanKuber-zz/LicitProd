using Windows.UI.Xaml.Controls;

namespace LicitProd.UI.Uwp.Controls
{
    public class ButtonTranslatable : Button
    {
        private readonly ControlsTranslatable _controlsTranslatable;

        public ButtonTranslatable()
        {
            _controlsTranslatable = new ControlsTranslatable(ChangeLanguage);
        }

        private void ChangeLanguage(string value)
        {
            Content = value;
        }

        public string TranslatableKey
        {
            get => _controlsTranslatable.TranslatableKey;
            set => _controlsTranslatable.TranslatableKey = value;
        }
    }
}