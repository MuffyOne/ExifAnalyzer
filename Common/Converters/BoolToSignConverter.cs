using System;
using System.Globalization;
using System.Windows.Data;

namespace ExifAnalyzer.Common.Converters
{
    public class BoolToSignConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && (bool)value)
            {
                return "<";
            }
            return ">";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("not implemented");
        }
    }
}