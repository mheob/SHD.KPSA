namespace HelperTools.Navigation.Views
{
    using Infrastructure.Constants;
    using Infrastructure.Interfaces;

    /// <summary>Interaction logic for HomeTiles.xaml</summary>
    /// <seealso cref="HelperTools.Infrastructure.Interfaces.IContentView" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// <seealso cref="System.Windows.Controls.UserControl" />
    public partial class HomeTiles : IContentView
    {
        /// <summary>Initializes a new instance of the <see cref="HomeTiles" /> class.</summary>
        public HomeTiles()
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