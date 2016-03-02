
#pragma warning disable 1591

namespace SHD.KPSA.Utils.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class InverseBoolConverter : BaseConverter, IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}