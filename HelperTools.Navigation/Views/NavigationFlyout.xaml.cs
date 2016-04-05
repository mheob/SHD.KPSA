namespace HelperTools.Navigation.Views
{
    using Infrastructure.Constants;
    using Infrastructure.Interfaces;

    /// <summary>Interaction logic for NavigationFlyout.xaml</summary>
    /// <seealso cref="MahApps.Metro.Controls.Flyout" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// <seealso cref="HelperTools.Infrastructure.Interfaces.IFlyoutView" />
    public partial class NavigationFlyout : IFlyoutView
    {
        /// <summary>Initializes a new instance of the <see cref="NavigationFlyout" /> class.</summary>
        public NavigationFlyout()
        {
            InitializeComponent();
        }

        /// <summary>Gets the name of the flyout.</summary>
        /// <value>The name of the flyout.</value>
        public string FlyoutName => FlyoutNames.NAVIGATION_FLYOUT;
    }
}