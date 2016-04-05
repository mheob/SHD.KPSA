namespace HelperTools.Infrastructure.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>An implementation of <see cref="IValueConverter" /> that converts the string formats.</summary>
    [ValueConversion(typeof (object), typeof (string))]
    public class StringFormatConverter : BaseConverter, IValueConverter
    {
        #region IValueConverter Members
        /// <summary>Attempts to convert the specified value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string format = parameter as string;

            return !string.IsNullOrEmpty(format) ? string.Format(culture, format, value) : value.ToString();
        }

        /// <summary>Attempts to convert the specified value back.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        #endregion IValueConverter Members
    }
}