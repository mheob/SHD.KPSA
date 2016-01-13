using DataTools.Utils;
using DataTools.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DataTools.ViewModels
{
    public class NavigationViewModel : BindableBase, INavigationViewModel
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand NavigateToMatFileGeneratorCommand { get; set; }
        public DelegateCommand NavigateToTools3DsCommand { get; set; }

        public NavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateToMatFileGeneratorCommand = new DelegateCommand(NavigateToMatFileGenerator);
            NavigateToTools3DsCommand = new DelegateCommand(NavigateToTools3Ds);
        }

        private void NavigateToMatFileGenerator()
        {
            _regionManager.RequestNavigate(Constants.RegionContent, typeof (MatFileGeneratorView).Name);
        }

        private void NavigateToTools3Ds()
        {
            _regionManager.RequestNavigate(Constants.RegionContent, typeof (Tools3DsView).Name);
        }
    }

    public interface INavigationViewModel
    {
        DelegateCommand NavigateToMatFileGeneratorCommand { get; set; }
        DelegateCommand NavigateToTools3DsCommand { get; set; }
    }

    public class NavigationDesignViewModel : INavigationViewModel
    {
        public DelegateCommand NavigateToMatFileGeneratorCommand { get; set; }
        public DelegateCommand NavigateToTools3DsCommand { get; set; }
    }
}