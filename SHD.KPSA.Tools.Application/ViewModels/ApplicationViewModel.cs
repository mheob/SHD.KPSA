namespace SHD.KPSA.Tools.Application.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Changelog.Models;
    using Changelog.ViewModels;
    using MahApps.Metro;
    using Models;
    using Properties;
    using Utils;

    /// <summary>
    /// The ViewModel for the ApplicationWindow.
    /// </summary>
    public class ApplicationViewModel : ObservableObject
    {
        #region Fields
        private string _theme;

        private List<Pages> _pageCollection;
        private List<IPageViewModel> _pageViewModels;
        private IPageViewModel _currentPageViewModel;
        private IPageViewModel _navigateToPageViewModel;
        private IPageViewModel _navigateToChangelogViewModel;


        private ICommand _changeThemeCommand;
        private ICommand _changePageCommand;
        #endregion Fields

        #region Constructor
        /// <summary>
        /// Initialize a new instance of the ApplicationViewModel class.
        /// </summary>
        public ApplicationViewModel()
        {
            GetPages();
            GetPageViewModels();

            Theme = Theme != null ? ThemeManager.DetectAppStyle(Application.Current).Item1.Name : ThemeDark;

            NavigateToChangelogViewModel = PageViewModels[0];
            CurrentPageViewModel = PageViewModels[1];
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Gets and sets the Theme.
        /// </summary>
        public string Theme
        {
            get { return _theme; }
            set
            {
                if (_theme == value) return;
                _theme = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the accent color in dark blue.
        /// </summary>
        public string AccentDarkBlue { get; } = "DarkBlue";

        /// <summary>
        /// Gets the dark theme of the application.
        /// </summary>
        public string ThemeDark { get; } = "Dark";

        /// <summary>
        /// Gets the light theme of the application.
        /// </summary>
        public string ThemeLight { get; } = "Light";

        /// <summary>
        /// Gets the list of all implemented PageViewModels.
        /// </summary>
        public List<IPageViewModel> PageViewModels => _pageViewModels ?? (_pageViewModels = new List<IPageViewModel>());

        /// <summary>
        /// Gets the list of all implemented PageViewModels.
        /// </summary>
        public List<Pages> PageCollection => _pageCollection ?? (_pageCollection = new List<Pages>());

        /// <summary>
        /// Gets and sets the CurrentPageViewModel.
        /// </summary>
        public IPageViewModel CurrentPageViewModel
        {
            get { return _currentPageViewModel; }
            set
            {
                if (_currentPageViewModel == value) return;
                _currentPageViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the NavigateToPageViewModel.
        /// </summary>
        public IPageViewModel NavigateToPageViewModel
        {
            get { return _navigateToPageViewModel; }
            set
            {
                if (_navigateToPageViewModel == value) return;
                _navigateToPageViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the NavigateToChangelogViewModel.
        /// </summary>
        public IPageViewModel NavigateToChangelogViewModel
        {
            get { return _navigateToChangelogViewModel; }
            set
            {
                if (_navigateToChangelogViewModel == value) return;
                _navigateToChangelogViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion Properties

        #region Commands
        /// <summary>
        /// Gets the command to change the application theme.
        /// </summary>
        public ICommand ChangeThemeCommand
        {
            get
            {
                if (_changeThemeCommand != null) return _changeThemeCommand;

                _changeThemeCommand = new RelayCommand(
                    param => ChangeTheme((string) param),
                    param => param != null);

                return _changeThemeCommand;
            }
        }

        /// <summary>
        /// Gets the command to change pages.
        /// </summary>
        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand != null) return _changePageCommand;

                _changePageCommand = new RelayCommand(
                    param => ChangeViewModel((IPageViewModel) param),
                    param => param is IPageViewModel);

                return _changePageCommand;
            }
        }
        #endregion Commands

        #region Methods
        private void GetPages()
        {
            PageCollection.Add(new Pages
            {
                //PageViewModel = new Tools3DsViewModel(),
                PageViewModel = new ChangelogViewModel(new ChangelogModel()),
                PageTitle = Resources.TitleTools3Ds
            });

            //PageCollection.Add(new Pages
            //{
            //    PageViewModel = new MatFileGenerator(),
            //    PageTitle = "MatFiles generieren"
            //});
        }

        private void GetPageViewModels()
        {
            PageViewModels.Add(new ChangelogViewModel(new ChangelogModel()));
            PageViewModels.Add(new Tools3DsViewModel());
        }

        private void ChangeTheme(string theme)
        {
            Theme = theme != ThemeDark ? ThemeDark : ThemeLight;

            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(AccentDarkBlue),
                ThemeManager.GetAppTheme(Theme));
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }
        #endregion
    }
}