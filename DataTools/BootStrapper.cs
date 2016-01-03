using System.Windows;
using DataTools.Utils;
using DataTools.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;

namespace DataTools
{
    public class BootStrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new MainWindow();
        }

        protected override void InitializeShell()
        {
            var window = (Window) Shell;
            Application.Current.MainWindow = window;

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Constants.RegionContent, typeof (NavigationView));
            regionManager.RegisterViewWithRegion(Constants.RegionContent, typeof (Tools3DsView));

            window.Show();
        }
    }
}