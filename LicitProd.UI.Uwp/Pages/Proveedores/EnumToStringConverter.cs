using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace LicitProd.UI.Uwp.Pages.Proveedores
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !(value is Enum))
                return null;

            var @enum = value as Enum;
            var description = @enum.ToString();

            var attrib = GetAttribute<DisplayAttribute>(@enum);
            if (attrib != null)
            {
                var resource = new ResourceLoader();
                if (!string.IsNullOrWhiteSpace(resource.GetString(attrib.Name)))
                    description = resource.GetString(attrib.Name);
                else
                    description = attrib.Name;
            }

            return description;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            string language)
        {
            throw new NotImplementedException();
        }

        private T GetAttribute<T>(Enum enumValue) where T : Attribute
        {
            return enumValue.GetType().GetTypeInfo()
                .GetDeclaredField(enumValue.ToString())
                .GetCustomAttribute<T>();
        }
    }
}