namespace SHD.KPSA.Tools.Utils.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class HasTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type paramType = parameter as Type;
            return value != null && value.GetType() == paramType;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}