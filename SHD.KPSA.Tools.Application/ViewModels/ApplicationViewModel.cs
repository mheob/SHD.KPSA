using MahApps.Metro;
using SHD.KPSA.Tools.Changelog.Models;
using SHD.KPSA.Tools.Changelog.ViewModels;
using SHD.KPSA.Tools.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

// ReSharper disable ObjectCreationAsStatement

namespace SHD.KPSA.Tools.Application.ViewModels
{
    /// <summary>
    /// The ViewModel for the ApplicationWindow.
    /// </summary>
    public class ApplicationViewModel : ObservableObject
    {
        #region Fields
        private string _theme;

        private Visibility _isVisibleBackNav;

        private List<IPageViewModel> _pageViewModels;
        private IPageViewModel _currentPageViewModel;
        private IPageViewModel _previousPageViewModel;
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
            PageViewModels.Add(new ChangelogViewModel(new ChangelogModel()));
            PageViewModels.Add(new Tools3DsViewModel());

            Theme = Theme != null ? ThemeManager.DetectAppStyle(System.Windows.Application.Current).Item1.Name : ThemeDark;

            NavigateToChangelogViewModel = PageViewModels[0];
            CurrentPageViewModel = PageViewModels[1];

            IsVisibleBackNav = Visibility.Hidden;
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
        /// Gets and sets the visibility of the NavigateBackCommand
        /// </summary>
        public Visibility IsVisibleBackNav
        {
            get { return _isVisibleBackNav; }
            set
            {
                if (_isVisibleBackNav == value) return;
                _isVisibleBackNav = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the list of all implemented PageViewModels.
        /// </summary>
        public List<IPageViewModel> PageViewModels => _pageViewModels ?? (_pageViewModels = new List<IPageViewModel>());

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
        /// Gets and sets the PreviousPageViewModel.
        /// </summary>
        public IPageViewModel PreviousPageViewModel
        {
            get { return _previousPageViewModel; }
            set
            {
                if (_previousPageViewModel == value) return;
                _previousPageViewModel = value;
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
        private void ChangeTheme(string theme)
        {
            Theme = theme != ThemeDark ? ThemeDark : ThemeLight;

            ThemeManager.ChangeAppStyle(System.Windows.Application.Current, ThemeManager.GetAccent(AccentDarkBlue),
                ThemeManager.GetAppTheme(Theme));
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            var tmpViewModel = CurrentPageViewModel;

            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);

            if (CurrentPageViewModel != tmpViewModel)
            {
                PreviousPageViewModel = tmpViewModel;
            }

            if (PreviousPageViewModel != null)
            {
                IsVisibleBackNav = Visibility.Visible;
            }
        }
        #endregion
    }
}