using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace LicitProd.UI.Uwp.Converters
{
 public   class DateTimeToDateTimeOfSetConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return DateTime.SpecifyKind((DateTime)value, DateTimeKind.Local);

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((DateTimeOffset) value).DateTime;

        }
    }
}
