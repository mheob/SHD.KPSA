namespace HelperTools.Shell.Models
{
    using System.Windows.Media;
    using Infrastructure.Base;

    /// <summary>The model for the ApplicationTheme.</summary>
    /// <seealso cref="BindableBase" />
    public class ApplicationTheme : BindableBase
    {
        private string name;
        private Brush colorBrush;
        private Brush borderColorBrush;

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        /// <summary>Gets or sets the color brush.</summary>
        /// <value>The color brush.</value>
        public Brush ColorBrush
        {
            get { return colorBrush; }
            set { SetProperty(ref colorBrush, value); }
        }

        /// <summary>Gets or sets the border color brush.</summary>
        /// <value>The border color brush.</value>
        public Brush BorderColorBrush
        {
            get { return borderColorBrush; }
            set { SetProperty(ref borderColorBrush, value); }
        }
    }
}