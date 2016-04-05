namespace HelperTools.Shell.Views
{
    using Infrastructure.Constants;
    using Infrastructure.Interfaces;

    /// <summary>Interaction logic for SettingsFlyout.xaml</summary>
    /// <seealso cref="MahApps.Metro.Controls.Flyout" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// <seealso cref="HelperTools.Infrastructure.Interfaces.IFlyoutView" />
    public partial class SettingsFlyout : IFlyoutView
    {
        /// <summary>Initializes a new instance of the <see cref="SettingsFlyout" /> class.</summary>
        public SettingsFlyout()
        {
            InitializeComponent();
        }

        /// <summary>Gets the name of the flyout.</summary>
        /// <value>The name of the flyout.</value>
        public string FlyoutName => FlyoutNames.SHELL_SETTINGS_FLYOUT;
    }
}