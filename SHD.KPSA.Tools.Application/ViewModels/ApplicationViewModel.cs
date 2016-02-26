namespace SHD.KPSA.Tools.Application.ViewModels
{
    using System.Windows;
    using System.Windows.Input;
    using Changelog.Models;
    using Changelog.ViewModels;
    using Clean3Ds.ViewModels;
    using MahApps.Metro;
    using MatFileGenerator.ViewModels;
    using Properties;
    using Utils;

    /// <summary>
    /// The ViewModel for the ApplicationWindow.
    /// </summary>
    public class ApplicationViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private string theme;

        private bool isNavigationView;
        private bool canBackNav;

        private IPageViewModel currentPageViewModel;
        private IPageViewModel navigateFromPageViewModel;

        private ICommand changePageCommand;
        private ICommand changeThemeCommand;
        #endregion Fields

        #region Constructor
        /// <summary>
        /// Initialize a new instance of the ApplicationViewModel class.
        /// </summary>
        public ApplicationViewModel()
        {
            Theme = Theme != null ? ThemeManager.DetectAppStyle(Application.Current).Item1.Name : ThemeDark;

            HomePageViewModel = this;
            ChangelogPageViewModel = new ChangelogViewModel(new ChangelogModel());
            MatFileGenPageViewModel = new MatFileGenViewModel();
            Clean3DsPageViewModel = new Clean3DsViewModel();

            CurrentPageViewModel = HomePageViewModel;
            IsNavigationView = true;
            CanBackNav = false;
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Gets the Title of the ChangelogView.
        /// </summary>
        public string Title => Resources.TitleHome;

        /// <summary>
        /// Gets the accent color in dark blue.
        /// </summary>
        public string AccentDarkBlue => "DarkBlue";

        /// <summary>
        /// Gets the dark theme of the application.
        /// </summary>
        public string ThemeDark => "Dark";

        /// <summary>
        /// Gets the light theme of the application.
        /// </summary>
        public string ThemeLight => "Light";

        /// <summary>
        /// Gets the ViewModel of the NavigationPage.
        /// </summary>
        public IPageViewModel HomePageViewModel { get; }

        /// <summary>
        /// Gets the ViewModel of the ChangelogPage.
        /// </summary>
        public IPageViewModel ChangelogPageViewModel { get; }

        /// <summary>
        /// Gets the ViewModel of the MatFileGenPage.
        /// </summary>
        public IPageViewModel MatFileGenPageViewModel { get; }

        /// <summary>
        /// Gets the ViewModel of the Clean3DsPage.
        /// </summary>
        public IPageViewModel Clean3DsPageViewModel { get; }

        /// <summary>
        /// Gets and sets the Theme.
        /// </summary>
        public string Theme
        {
            get { return theme; }
            set
            {
                if (theme == value) return;
                theme = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the CanBackNav-property.
        /// </summary>
        public bool CanBackNav
        {
            get { return canBackNav; }
            set
            {
                if (canBackNav == value) return;
                canBackNav = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the IsNavigation-property.
        /// </summary>
        public bool IsNavigationView
        {
            get { return isNavigationView; }
            set
            {
                if (isNavigationView == value) return;
                isNavigationView = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the CurrentPageViewModel.
        /// </summary>
        public IPageViewModel CurrentPageViewModel
        {
            get { return currentPageViewModel; }
            set
            {
                if (currentPageViewModel == value) return;
                currentPageViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the navigateFromPageViewModel.
        /// </summary>
        public IPageViewModel NavigateFromPageViewModel
        {
            get { return navigateFromPageViewModel; }
            set
            {
                if (navigateFromPageViewModel == value) return;
                navigateFromPageViewModel = value;
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
                if (changeThemeCommand != null) return changeThemeCommand;

                changeThemeCommand = new RelayCommand(
                    param => ChangeTheme((string) param),
                    param => param != null);

                return changeThemeCommand;
            }
        }

        /// <summary>
        /// Gets the command to change pages.
        /// </summary>
        public ICommand ChangePageCommand
        {
            get
            {
                if (changePageCommand != null) return changePageCommand;

                changePageCommand = new RelayCommand(
                    param => ChangeViewModel((IPageViewModel) param),
                    param => param is IPageViewModel);

                return changePageCommand;
            }
        }
        #endregion Commands

        #region Methods
        private void ChangeTheme(string newTheme)
        {
            Theme = newTheme != ThemeDark ? ThemeDark : ThemeLight;

            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(AccentDarkBlue),
                ThemeManager.GetAppTheme(Theme));
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (CurrentPageViewModel.Equals(viewModel)) return;

            NavigateFromPageViewModel = CurrentPageViewModel;

            IsNavigationView = viewModel.Equals(this);
            CanBackNav = true;

            CurrentPageViewModel = viewModel;
        }
        #endregion Methods
    }
}