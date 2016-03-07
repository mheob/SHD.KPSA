namespace SHD.KPSA.Utils.WpfConverter
{
    using System;
    using System.Windows.Markup;

    /// <summary>
    /// A BaseConverter class to should easier implements converter in the XAML-code.
    /// values.
    /// </summary>
    public abstract class BaseConverter : MarkupExtension
    {
        /// <summary>
        /// When implemented in a derived class, returns an object that is provided as the value of the target property
        /// for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}