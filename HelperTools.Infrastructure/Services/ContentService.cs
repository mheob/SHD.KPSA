namespace HelperTools.Infrastructure.Services
{
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Constants;
    using Events;
    using Interfaces;
    using Microsoft.Practices.ServiceLocation;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Regions;
    using Properties;

    /// <summary>The ContentService.</summary>
    /// <seealso cref="IContentService" />
    public class ContentService : IContentService
    {
        #region Fields
        private readonly IRegionManager regionManager;
        private IEventAggregator eventAggregator;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="ContentService" /> class.</summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="applicationCommands">The application commands.</param>
        public ContentService(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            this.regionManager = regionManager;

            ShowContentCommand = new DelegateCommand<string>(ShowContent, CanShowContent);
            applicationCommands.ShowContentCommand.RegisterCommand(ShowContentCommand);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets the show content command.</summary>
        /// <value>The show content command.</value>
        public ICommand ShowContentCommand { get; }
        #endregion Properties

        #region Implementation of IContentService
        /// <summary>Shows the content.</summary>
        /// <param name="contentName">Name of the content.</param>
        public void ShowContent(string contentName)
        {
            var region = regionManager.Regions[RegionNames.MAIN_REGION];

            var content = region?.Views?.FirstOrDefault(v => v is IContentView && ((IContentView) v).ContentName.Equals(contentName)) as UserControl;

            if (content == null)
            {
                return;
            }

            regionManager.RequestNavigate(RegionNames.MAIN_REGION, contentName, NavigationCompleted);

            eventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(Resources.StatusBarViewChanged, contentName));
        }

        /// <summary>Determines whether this instance [can show the content] the specified content name.</summary>
        /// <param name="contentName">Name of the content.</param>
        /// <returns><c>treu</c>, if the content can be shown, else <c>false</c></returns>
        public bool CanShowContent(string contentName)
        {
            return true;
        }
        #endregion Implementation of IContentService

        #region Methods
        private void NavigationCompleted(NavigationResult navigationResult)
        {
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAggregator.GetEvent<SelectedPathUpdateEvent>().Publish(null);
        }
        #endregion Methods
    }
}