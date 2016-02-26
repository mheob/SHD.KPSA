namespace SHD.KPSA.Utils.Converter
{
    using System;
    using System.Windows.Markup;

    /// <summary>
    /// A BaseConverter class to should easier implements converter in the XAML-code.
    /// values.
    /// </summary>
    public abstract class BaseConverter : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}