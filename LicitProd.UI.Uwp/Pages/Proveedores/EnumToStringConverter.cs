using System;
using System.ComponentModel;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace LicitProd.UI.Uwp.Pages.Proveedores
{
    public class EnumToStringConverter : IValueConverter
    {
      
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return GetDescription((Enum)value);
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

    }
}