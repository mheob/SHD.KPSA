using System;
using DataTools.NavigationModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DataTools.NavigationModule
{
    public class NavigationModuleInitializer : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public NavigationModuleInitializer(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<object, MainView>("MainView");
            //_container.RegisterType<object, ViewOne>("ViewOne");
            //_container.RegisterType<object, ViewTwo>("ViewTwo");

            _regionManager.Regions["MainRegion"].RequestNavigate(new Uri("MainView", UriKind.Relative));
        }
    }
}