using System.Collections.ObjectModel;
using System.Windows.Controls;
using SHD.KPSA.Tools.Changelog.Models;
using SHD.KPSA.Tools.Changelog.Properties;
using SHD.KPSA.Tools.Utils;

namespace SHD.KPSA.Tools.Changelog.ViewModels
{
    public class ChangelogViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private readonly ChangelogModel _changelog;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets the Title of the ChangelogView.
        /// </summary>
        public string Title => Resources.TitleChangelog;

        /// <summary>
        /// Gets the date of the build.
        /// </summary>
        public string BuildNote => _changelog.BuildNote;

        /// <summary>
        /// Creates a new ObservableCollection of Labels.
        /// </summary>
        public ObservableCollection<Label> ChangelogLines => _changelog.ChangelogLines;
        #endregion Properties

        #region Constructor
        /// <summary>
        /// Initialize a new instance of the ChangelogModel class.
        /// </summary>
        /// <param name="changelog">The ViewModel of the current page.</param>
        public ChangelogViewModel(ChangelogModel changelog)
        {
            _changelog = changelog;
        }
        #endregion Constructor
    }
}