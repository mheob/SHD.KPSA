namespace HelperTools.Shell.Model
{
    using System.Windows.Media;
    using Infrastructure.Base;

    /// <summary>The model for the AccentColor.</summary>
    /// <seealso cref="BindableBase" />
    public class AccentColor : BindableBase
    {
        private string name;
        private Brush colorBrush;

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
    }
}