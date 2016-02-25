namespace SHD.KPSA.Utils.Converter
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converter-class to set the visibility from an boolean value.
    /// </summary>
    /// <example>
    /// This sample shows how to call the converter in the XAML-Code.
    /// <code><local:BoolToVisibleConverter x:Key="BoolToVisibleConverter" Collapse="True" Reverse="True" /></code>
    /// </example>
    public class BoolToVisibleConverter : IValueConverter
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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = (bool) value;

            if (bValue != Reverse)
                return Visibility.Visible;

            return Collapse ? Visibility.Collapsed : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility) value;

            if (visibility == Visibility.Visible)
                return !Reverse;

            return Reverse;
        }
        #endregion IValueConverter Members
    }
}