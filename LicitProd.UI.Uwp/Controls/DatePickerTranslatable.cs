using Windows.UI.Xaml.Controls;

namespace LicitProd.UI.Uwp.Controls
{
    public class DatePickerTranslatable : DatePicker
    {
        private readonly ControlsTranslatable _controlsTranslatable;

        public DatePickerTranslatable()
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