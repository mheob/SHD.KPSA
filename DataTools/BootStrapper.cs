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

            Container.RegisterType<object, NavigationView>(typeof (NavigationView).Name);
            Container.RegisterType<object, Changelog>(typeof (Changelog).Name);
            Container.RegisterType<object, Tools3DsView>(typeof (Tools3DsView).Name);

            regionManager.RequestNavigate(Constants.RegionContent, typeof (NavigationView).Name);

            window.Show();
        }
    }
}