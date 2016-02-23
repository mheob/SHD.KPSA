namespace SHD.KPSA.Tools.Application.ViewModels
{
    using Properties;
    using Utils;

    public class NavigationViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        #endregion Fields

        #region Constructor
        public NavigationViewModel()
        {
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Gets the Title of the ChangelogView.
        /// </summary>
        public string Title => Resources.TitleNavigation;
        #endregion Properties

        #region Commands
        #endregion Commands

        #region Methods
        #endregion Methods
    }
}