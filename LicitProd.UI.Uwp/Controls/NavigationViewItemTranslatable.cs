using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Services;

namespace LicitProd.UI.Uwp.Controls
{
    public class NavigationViewItemTranslatable : NavigationViewItem
    {
        private readonly ControlsTranslatable _controlsTranslatable;
        private readonly ControlsPermissionsVisible _controlsPermissionsVisible;
        public NavigationViewItemTranslatable()
        {
            _controlsTranslatable = new ControlsTranslatable(ChangeLanguage);
            _controlsPermissionsVisible= new ControlsPermissionsVisible(DoesNotHavePermissionCallback);
        }

        private void DoesNotHavePermissionCallback()
        {
            this.Visibility = Visibility.Collapsed;
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

        public string PermissionKey
        {
            get => _controlsPermissionsVisible.Permission;
            set => _controlsPermissionsVisible.Permission = value;
        }
    }
}