namespace HelperTools.Updater.Views
{
    using Infrastructure.Constants;
    using Infrastructure.Interfaces;

    /// <summary></summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// <seealso cref="IContentView" />
    public partial class UpdateChecker : IContentView
    {
        /// <summary>Initializes a new instance of the <see cref="UpdateChecker" /> class.</summary>
        public UpdateChecker()
        {
            InitializeComponent();
        }

        #region Implementation of IContentView
        /// <summary>Gets the name of the content.</summary>
        /// <value>The name of the content.</value>
        public string ContentName => ContentNames.HOME_TILES_CONTENT;
        #endregion
    }
}