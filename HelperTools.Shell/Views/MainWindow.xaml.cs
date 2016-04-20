namespace HelperTools.Shell.Views
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using Infrastructure.Constants;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Logging;
    using Prism.Regions;

    /// <summary>Interaction logic for MainWindow.xaml</summary>
    /// <seealso cref="MahApps.Metro.Controls.MetroWindow" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow
    {
        /// <summary>Initializes a new instance of the <see cref="MainWindow" /> class.</summary>
        /// <param name="regionManager">The region manager.</param>
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            // The RegionManager.RegionName attached property XAML-Declaration doesn't work for this scenario (object declared outside
            // the logical tree) theses objects are not part of the logical tree, hence the parent that has the region manager to use
            // (the Window) cannot be found using LogicalTreeHelper.FindParent therefore the regionManager is never found and cannot be
            // assigned automatically by Prism.  This means we have to handle this ourselves
            if (regionManager == null) return;

            SetRegionManager(regionManager, LeftWindowCommandsRegion, RegionNames.LEFT_WINDOW_COMMANDS_REGION);
            SetRegionManager(regionManager, RightWindowCommandsRegion, RegionNames.RIGHT_WINDOW_COMMANDS_REGION);
            SetRegionManager(regionManager, FlyoutsControlRegion, RegionNames.FLYOUT_REGION);

            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            container.Resolve<ILoggerFacade>().Log("MainWindow created", Category.Info, Priority.None);
        }

        [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
        private void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }
    }
}