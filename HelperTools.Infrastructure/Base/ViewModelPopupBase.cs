namespace HelperTools.Infrastructure.Base
{
    using System.Windows.Media;

    /// <summary>The ViewModelDialogPopupBase.</summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public abstract class ViewModelDialogPopupBase : BindableBase
    {
        /// <summary>Gets the title.</summary>
        /// <value>The title.</value>
        public abstract string Title { get; }

        /// <summary>Gets the icon.</summary>
        /// <value>The icon.</value>
        public abstract ImageSource Icon { get; }
    }
}