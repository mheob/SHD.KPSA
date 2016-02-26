namespace SHD.KPSA.Tools.Changelog.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using Models;
    using Properties;
    using Utils;

    /// <summary>
    /// The ViewModel for the ChangelogViewModel.
    /// </summary>
    public class ChangelogViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private readonly ChangelogModel changelog;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets the Title of the ChangelogView.
        /// </summary>
        public string Title => Resources.TitleChangelog;

        /// <summary>
        /// Gets the date of the build.
        /// </summary>
        public string BuildNote => changelog.BuildNote;

        /// <summary>
        /// Creates a new ObservableCollection of Labels.
        /// </summary>
        public ObservableCollection<Label> ChangelogLines => changelog.ChangelogLines;
        #endregion Properties

        #region Constructor
        /// <summary>
        /// Initialize a new instance of the ChangelogModel class.
        /// </summary>
        /// <param name="changelog">The ViewModel of the current page.</param>
        public ChangelogViewModel(ChangelogModel changelog)
        {
            this.changelog = changelog;
        }
        #endregion Constructor
    }
}