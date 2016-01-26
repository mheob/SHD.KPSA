using Prism.Commands;
using Prism.Regions;
using System.Windows;

namespace DataTools.ViewModels.Sections
{
    public class HeaderViewModel : DependencyObject, INavigationAware, IHeaderViewModel
    {
        private IRegionNavigationJournal _navigationJournal;
        //private string _title;

        //public string Title
        //{
        //    get { return _title; }
        //    set { SetProperty(ref _title, value); }
        //}

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title",
            typeof (string), typeof (HeaderViewModel), new FrameworkPropertyMetadata(string.Empty));

        public string Title
        {
            get { return GetValue(TitleProperty).ToString(); }
            set { SetValue(TitleProperty, value); }
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
        //string Title { get; set; }
        DelegateCommand NavigateBackCommand { get; set; }
    }

    public class HeaderDataViewModel : DependencyObject, IHeaderViewModel
    {
        public DelegateCommand NavigateBackCommand { get; set; }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title",
            typeof (string), typeof (HeaderViewModel), new FrameworkPropertyMetadata(string.Empty));

        public string Title
        {
            get { return GetValue(TitleProperty).ToString(); }
            set { SetValue(TitleProperty, value); }
        }

        public HeaderDataViewModel()
        {
            Title = "I am the Header";
        }
    }
}