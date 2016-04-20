namespace HelperTools.Shell.Views
{
    using Infrastructure.Constants;
    using Infrastructure.Interfaces;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Logging;

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

            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            container.Resolve<ILoggerFacade>().Log("SettingsFlyoutView created", Category.Info, Priority.None);
        }

        /// <summary>Gets the name of the flyout.</summary>
        /// <value>The name of the flyout.</value>
        public string FlyoutName => FlyoutNames.SHELL_SETTINGS_FLYOUT;
    }
}