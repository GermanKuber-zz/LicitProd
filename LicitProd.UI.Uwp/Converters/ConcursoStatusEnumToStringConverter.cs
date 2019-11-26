using System;
using Windows.UI.Xaml.Data;
using LicitProd.Entities;

namespace LicitProd.UI.Uwp.Converters
{
    public class ConcursoStatusEnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            return ((ConcursoStatusEnum)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {

            return ((ConcursoStatusEnum)value).ToString();
        }
    }
}