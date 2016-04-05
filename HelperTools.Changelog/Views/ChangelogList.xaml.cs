namespace HelperTools.Changelog.Views
{
    using Infrastructure.Constants;
    using Infrastructure.Interfaces;

    /// <summary>Interaction logic for ChangelogList.xaml</summary>
    /// <seealso cref="HelperTools.Infrastructure.Interfaces.IContentView" />
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ChangelogList : IContentView
    {
        /// <summary>Initializes a new instance of the <see cref="ChangelogList" /> class.</summary>
        public ChangelogList()
        {
            InitializeComponent();
        }

        #region Implementation of IContentView
        /// <summary>Gets the name of the content.</summary>
        /// <value>The name of the content.</value>
        public string ContentName => ContentNames.CHANGELOG_CONTENT;
        #endregion
    }
}