namespace HelperTools.Shell
{
    using System.Windows;
    using Changelog;
    using Clean3Ds;
    using Infrastructure;
    using Infrastructure.Constants;
    using Infrastructure.Interfaces;
    using Infrastructure.Services;
    using Logging;
    using Microsoft.Practices.Unity;
    using Navigation;
    using Prism.Logging;
    using Prism.Modularity;
    using Prism.Regions;
    using Prism.Unity;
    using Views;

    /// <summary>The Bootstrapper.</summary>
    /// <seealso cref="UnityBootstrapper" />
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>Creates the shell or main window of the application.</summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        /// <see cref="T:Prism.Bootstrapper" /> will attach the default <see cref="T:Prism.Regions.IRegionManager" /> of the application
        /// in its <see cref="F:Prism.Regions.RegionManager.RegionManagerProperty" /> attached property in order to be able to add
        /// regions by using the <see cref="F:Prism.Regions.RegionManager.RegionNameProperty" />
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            Container.RegisterInstance(typeof (Window), WindowNames.MAIN_WINDOW_NAME, Container.Resolve<MainWindow>(),
                new ContainerControlledLifetimeManager());

            return Container.Resolve<Window>(WindowNames.MAIN_WINDOW_NAME);
        }

        /// <summary>Initializes the shell.</summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            // Register views
            var regionManager = Container.Resolve<IRegionManager>();

            if (regionManager != null)
            {
                regionManager.RegisterViewWithRegion(RegionNames.RIGHT_WINDOW_COMMANDS_REGION, typeof (RightTitlebarCommands));
                regionManager.RegisterViewWithRegion(RegionNames.FLYOUT_REGION, typeof (SettingsFlyout));
            }

            // Register services
            RegisterServices();

            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures the <see cref="T:Microsoft.Practices.Unity.IUnityContainer" />. May be overwritten in a derived class to
        /// add specific type mappings required by the application.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IApplicationCommands, ApplicationCommandsProxy>();
            Container.RegisterInstance<IContentService>(Container.Resolve<ContentService>());
            Container.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());
            Container.RegisterInstance(typeof (ILocalizerService), ServiceNames.LOCALIZER_SERVICE, new LocalizerService("de-DE"),
                new ContainerControlledLifetimeManager());
        }

        /// <summary>Configures the <see cref="T:Prism.Modularity.IModuleCatalog" /> used by Prism.</summary>
        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog moduleCatalog = (ModuleCatalog) ModuleCatalog;

            moduleCatalog.AddModule(typeof (Navigation));
            moduleCatalog.AddModule(typeof (Changelog));
            moduleCatalog.AddModule(typeof (Clean3Ds));
        }

        /// <summary>Create the <see cref="T:Prism.Logging.ILoggerFacade" /> used by the Bootstrapper.</summary>
        /// <returns></returns>
        /// <remarks>The base implementation returns a new TextLogger.</remarks>
        protected override ILoggerFacade CreateLogger()
        {
            return new NLogLogger();
        }

        private void RegisterServices()
        {
            // MessageDisplayService
            Container.RegisterInstance<IMetroMessageDisplayService>(ServiceNames.METRO_MESSAGE_DISPLAY_SERVICE,
                Container.Resolve<MetroMessageDisplayService>(), new ContainerControlledLifetimeManager());
        }
    }
}