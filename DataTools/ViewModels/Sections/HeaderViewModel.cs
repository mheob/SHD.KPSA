using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DataTools.ViewModels.Sections
{
    public class HeaderViewModel : BindableBase, INavigationAware, IHeaderViewModel
    {
        private IRegionNavigationJournal _navigationJournal;
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand NavigateBackCommand { get; set; }

        public HeaderViewModel()
        {
            NavigateBackCommand = new DelegateCommand(NavigateBack);
        }

        private void NavigateBack()
        {
            _navigationJournal.GoBack();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationJournal = navigationContext.NavigationService.Journal;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }

    public interface IHeaderViewModel
    {
        string Title { get; set; }
        DelegateCommand NavigateBackCommand { get; set; }
    }

    public class HeaderDataViewModel : IHeaderViewModel
    {
        public string Title { get; set; } = "I am the Header";
        public DelegateCommand NavigateBackCommand { get; set; }
    }
}