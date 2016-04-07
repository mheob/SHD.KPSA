namespace HelperTools.MatFileGen.Views
{
    using Infrastructure.Constants;
    using Infrastructure.Interfaces;

    /// <summary>Interaction logic for MatFileGen.xaml</summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// <seealso cref="HelperTools.Infrastructure.Interfaces.IContentView" />
    public partial class MatFileGen : IContentView
    {
        /// <summary>Initializes a new instance of the <see cref="MatFileGen" /> class.</summary>
        public MatFileGen()
        {
            InitializeComponent();
        }

        #region Implementation of IContentView
        /// <summary>Gets the name of the content.</summary>
        /// <value>The name of the content.</value>
        public string ContentName => ContentNames.MAT_FILE_GEN_CONTENT;
        #endregion
    }
}