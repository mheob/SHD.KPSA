namespace SHD.KPSA.Utils.Converter
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// An implementation of <see cref="IValueConverter" /> that converts the casing of the input string.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>CaseConverter</c> class can be used to convert input strings into upper or lower case according to the
    /// <see cref="Casing" /> property.
    /// Setting <see cref="Casing" /> is a shortcut for setting both <see cref="SourceCasing" /> and
    /// <see cref="TargetCasing" />.
    /// It is therefore possible to specify that the source and target properties be converted to different casings.
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how a <c>CaseConverter</c> can be used to convert a bound value to upper-case:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding Name, Converter={CaseConverter Upper}}"/>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// The following example shows how a <c>CaseConverter</c> can be used to convert a bound value to upper-case,
    /// but display it in lower-case:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding Name, Converter={CaseConverter SourceCasing=Upper, TargetCasing=Lower}}"/>
    /// ]]>
    /// </code>
    /// </example>
    [ValueConversion(typeof (string), typeof (string))]
    public class CaseConverter : BaseConverter, IValueConverter
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the CaseConverter class.
        /// </summary>
        public CaseConverter()
        {
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Initializes a new instance of the CaseConverter class with the specified source and target casings.
        /// </summary>
        /// <param name="casing">The source and target casings for the converter (see <see cref="Casing" />).</param>
        public CaseConverter(CharacterCasing casing)
        {
            Casing = casing;
        }

        /// <summary>
        /// Initializes a new instance of the CaseConverter class with the specified source and target casings.
        /// </summary>
        /// <param name="sourceCasing">The source casing for the converter (see <see cref="SourceCasing" />).</param>
        /// <param name="targetCasing">The target casing for the converter (see <see cref="TargetCasing" />).</param>
        public CaseConverter(CharacterCasing sourceCasing, CharacterCasing targetCasing)
        {
            SourceCasing = sourceCasing;
            TargetCasing = targetCasing;
        }

        /// <summary>
        /// Gets or sets the source casing for the converter.
        /// </summary>
        [ConstructorArgument("sourceCasing")]
        public CharacterCasing SourceCasing { get; set; }

        /// <summary>
        /// Gets or sets the target casing for the converter.
        /// </summary>
        [ConstructorArgument("targetCasing")]
        public CharacterCasing TargetCasing { get; set; }

        /// <summary>
        /// Sets both the source and target casings for the converter.
        /// </summary>
        [ConstructorArgument("casing")]
        public CharacterCasing Casing
        {
            set
            {
                SourceCasing = value;
                TargetCasing = value;
            }
        }
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
            var str = value as string;

            if (str == null) return DependencyProperty.UnsetValue;

            culture = culture ?? CultureInfo.CurrentCulture;

            switch (TargetCasing)
            {
                case CharacterCasing.Lower:
                    return str.ToLower(culture);
                case CharacterCasing.Upper:
                    return str.ToUpper(culture);
                case CharacterCasing.Normal:
                    return str;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
            var str = value as string;

            if (str == null) return DependencyProperty.UnsetValue;

            culture = culture ?? CultureInfo.CurrentCulture;

            switch (SourceCasing)
            {
                case CharacterCasing.Lower:
                    return str.ToLower(culture);
                case CharacterCasing.Upper:
                    return str.ToUpper(culture);
                case CharacterCasing.Normal:
                    return str;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion IValueConverter Members
    }
}