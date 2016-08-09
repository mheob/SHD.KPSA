namespace HelperTools.Clean3Ds.Views
{
    using Infrastructure.Constants;
    using Infrastructure.Interfaces;

    /// <summary>Interaction logic for Clean3Ds.xaml</summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// <seealso cref="IContentView" />
    public partial class Clean3Ds : IContentView
    {
        /// <summary>Initializes a new instance of the <see cref="Clean3Ds" /> class.</summary>
        public Clean3Ds()
        {
            InitializeComponent();
        }

        #region Implementation of IContentView
        /// <summary>Gets the name of the content.</summary>
        /// <value>The name of the content.</value>
        public string ContentName => ContentNames.CLEAN3_DS_CONTENT;
        #endregion
    }
}