namespace HelperTools.Infrastructure.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using Services;

    /// <summary>An implementation of <see cref="IValueConverter" /> that converts <see cref="ColorToStringConverter" />
    /// values to <see cref="string" />
    /// </summary>
    /// <seealso cref="BaseConverter" />
    /// <seealso cref="IValueConverter" />
    [ValueConversion(typeof (Color), typeof (string))]
    public class ColorToStringConverter : BaseConverter, IValueConverter
    {
        #region Enum
        /// <summary>An enumeration with the choices of color display.</summary>
        public enum ColorDisplay
        {
            /// <summary>The color as hexadecimal.</summary>
            Hex,

            /// <summary>The Red part of the RGB.</summary>
            Red,

            /// <summary>The Green part of the RGB.</summary>
            Green,

            /// <summary>The Blue part of the RGB.</summary>
            Blue
        }
        #endregion Enum

        #region Constructor
        #endregion Constructor

        #region Properties
        /// <summary>Gets and sets the option for collapsing.</summary>
        /// <value>The show color format.</value>
        public ColorDisplay ShowColorFormat { get; set; } = ColorDisplay.Hex;
        #endregion Properties

        #region IValueConverter Members
        /// <summary>Converts a <see cref="Color" /> to a <see cref="string" />.</summary>
        /// <param name="value">The <see cref="Color" /> produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted <see cref="string" />. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color) value;
            var rgb = new[] {color.R, color.G, color.B};

            switch (ShowColorFormat)
            {
                case ColorDisplay.Hex:
                    return $"#{rgb[0].ToString("X2")}{rgb[1].ToString("X2")}{rgb[2].ToString("X2")}";
                case ColorDisplay.Red:
                    return rgb[0].ToString();
                case ColorDisplay.Green:
                    return rgb[1].ToString();
                case ColorDisplay.Blue:
                    return rgb[2].ToString();
                default:
                    return value;
            }
        }

        /// <summary>Converts a <see cref="string" /> back to <see cref="Color" />.</summary>
        /// <param name="value">The <see cref="string" /> that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <remarks>Currently not used in toolkit, but provided for developer use in their own projects</remarks>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (string) value;
            var rgb = ColorConverterService.GetRgbfromHex(color);

            switch (ShowColorFormat)
            {
                case ColorDisplay.Hex:
                    return value.ToString();
                case ColorDisplay.Red:
                    return Color.FromRgb(rgb[0], rgb[1], rgb[2]);
                case ColorDisplay.Green:
                    return Color.FromRgb(rgb[0], rgb[1], rgb[2]);
                case ColorDisplay.Blue:
                    return Color.FromRgb(rgb[0], rgb[1], rgb[2]);
                default:
                    return value;
            }
        }
        #endregion IValueConverter Members
    }
}