using Windows.UI.Xaml.Controls;

namespace LicitProd.UI.Uwp.Controls
{
    public class AppBarButtonTranslatable : AppBarButton
    {
        private readonly ControlsTranslatable _controlsTranslatable;

        public AppBarButtonTranslatable()
        {
            _controlsTranslatable = new ControlsTranslatable(ChangeLanguage);
        }

        private void ChangeLanguage(string value)
        {
            if (value != null)
                Label = value;
        }

        public string TranslatableKey
        {
            get => _controlsTranslatable.TranslatableKey;
            set => _controlsTranslatable.TranslatableKey = value;
        }
    }
}