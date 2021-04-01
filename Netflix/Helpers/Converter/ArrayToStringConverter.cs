using System;
using System.Globalization;
using Xamarin.Forms;

namespace Netflix.Helpers.Converter
{
    public class ArrayToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => string.Join(" • ", value as string[]);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
