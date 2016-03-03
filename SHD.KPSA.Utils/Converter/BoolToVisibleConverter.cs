namespace SHD.KPSA.Utils.Converter
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// An implementation of <see cref="IValueConverter" /> that converts boolean values to <see cref="Visibility" />
    /// values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>BoolToVisibleConverter</c> class can be used to convert boolean values (or values that can be
    /// converted to boolean values) to <see cref="Visibility" /> values.
    /// By default, <see langword="true" /> is converted to <see cref="Visibility.Visible" /> and
    /// <see langword="false" /> is converted to <see cref="Visibility.Hidden" />.
    /// However, the <see cref="Collapse" /> property can be set to <see langword="true" /> in order to return
    /// <see cref="Visibility.Collapsed" /> instead of <see cref="Visibility.Hidden" />.
    /// In addition, the <see cref="Reverse" /> property can be set to <see langword="true" /> to reverse the
    /// returned values.
    /// </para>
    /// </remarks>
    /// <example>
    /// This sample shows how to call the converter in the XAML-Code.
    /// <code lang="xml">
    /// <![CDATA[
    /// <converter:BoolToVisibleConverter x:Key="BoolToVisibleConverter" Collapse="True" Reverse="True"/>
    /// <TextBox Visibility="{Binding IsTrue, Converter={StaticResource BoolToVisibleConverter}}"/>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// This sample shows how to call the converter in the XAML-Code only with the default values.
    /// <code lang="xml">
    /// <![CDATA[
    /// <TextBox Visibility="{Binding IsTrue, Converter={converter:BoolToVisibleConverter}}"/>
    /// ]]>
    /// </code>
    /// </example>
    [ValueConversion(typeof (bool), typeof (Visibility))]
    public class BoolToVisibleConverter : BaseConverter, IValueConverter
    {
        #region Constructor
        /// <summary>
        /// Initialize a new instance of the BoolToVisibleOrHiddenConverter class.
        /// </summary>
        public BoolToVisibleConverter()
        {
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Gets and sets the option for collapsing.
        /// </summary>
        public bool Collapse { get; set; } = false;

        /// <summary>
        /// Gets and sets the option for reversing the boolean parameter.
        /// </summary>
        public bool Reverse { get; set; } = false;
        #endregion Properties

        #region IValueConverter Members
        /// <summary>
        /// Attempts to convert the specified value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = (bool) value;

            if (bValue != Reverse)
            {
                return Visibility.Visible;
            }

            return Collapse ? Visibility.Collapsed : Visibility.Hidden;
        }

        /// <summary>
        /// Attempts to convert the specified value back.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility) value;

            if (visibility == Visibility.Visible)
            {
                return !Reverse;
            }

            return Reverse;
        }
        #endregion IValueConverter Members
    }
}