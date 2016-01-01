using System.Windows;
using DataTools.NavigationModule;
using Microsoft.Practices.ServiceLocation;
using Prism.Modularity;
using Prism.Unity;

namespace DataTools.Infrastructure
{
    public class BootStrapper : UnityBootstrapper
    {
        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }

        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog catalog = (ModuleCatalog) ModuleCatalog;

            // Nicht geeignet! Nehmen Sie stattdessen die Möglichkeit den catalog per XAML auszulesen.
            catalog.AddModule(typeof (NavigationModuleInitializer));
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
        }
    }
}