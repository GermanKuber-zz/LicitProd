using Windows.UI.Xaml.Controls;

namespace LicitProd.UI.Uwp.Controls
{

    public class RadioButtonCustom : RadioButton
    {
        private readonly ControlsTranslatable _controlsTranslatable;

        public RadioButtonCustom( )
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
    public class AppBarButtonTranslatable : AppBarButton
    {
        private readonly ControlsTranslatable _controlsTranslatable;

        public AppBarButtonTranslatable()
        {
            _controlsTranslatable = new ControlsTranslatable(ChangeLanguage);
        }

        private void ChangeLanguage(string value)
        {
            Label = value;
        }

        public string TranslatableKey
        {
            get => _controlsTranslatable.TranslatableKey;
            set => _controlsTranslatable.TranslatableKey = value;
        }
    }
}