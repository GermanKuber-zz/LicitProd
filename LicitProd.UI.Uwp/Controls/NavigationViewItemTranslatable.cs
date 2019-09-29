using Windows.UI.Xaml.Controls;
using LicitProd.Services;

namespace LicitProd.UI.Uwp.Controls
{
    public class NavigationViewItemTranslatable : NavigationViewItem
    {
        private readonly ControlsTranslatable _controlsTranslatable;

        public NavigationViewItemTranslatable()
        {
            _controlsTranslatable = new ControlsTranslatable(ChangeLanguage);
        }

        private void ChangeLanguage(string value)
        {
            Content= value;
        }

        public string TranslatableKey
        {
            get => _controlsTranslatable.TranslatableKey;
            set => _controlsTranslatable.TranslatableKey = value;
        }
    }
}