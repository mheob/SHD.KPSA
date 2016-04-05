namespace HelperTools.Infrastructure.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>An implementation of <see cref="IValueConverter" /> that converts an <see cref="int" /> the
    /// <see cref="string" /> formats.</summary>
    /// <seealso cref="BaseConverter" />
    /// <seealso cref="IValueConverter" />
    [ValueConversion(typeof (object), typeof (string))]
    public class IntegerToStringConverter : BaseConverter, IValueConverter
    {
        /// <summary>Gets and sets a helper value.</summary>
        public int EmptyStringValue { get; set; }

        #region IValueConverter Members
        /// <summary>Attempts to convert the specified value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value is string) return value;
            if (value is int && (int) value == EmptyStringValue) return string.Empty;

            return value.ToString();
        }

        /// <summary>Attempts to convert the specified value back.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string)) return value;

            string s = (string) value;
            int tmpInt;

            return int.TryParse(s, out tmpInt) ? tmpInt : EmptyStringValue;
        }
        #endregion IValueConverter Members
    }
}