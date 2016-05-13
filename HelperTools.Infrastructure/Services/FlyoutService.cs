namespace HelperTools.Infrastructure.Services
{
    using System.Linq;
    using System.Windows.Input;
    using Constants;
    using Events;
    using Interfaces;
    using MahApps.Metro.Controls;
    using Microsoft.Practices.ServiceLocation;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Regions;
    using Properties;

    /// <summary>The FlyoutService.</summary>
    /// <seealso cref="IFlyoutService" />
    public class FlyoutService : IFlyoutService
    {
        #region Fields
        private readonly IRegionManager regionManager;
        private IEventAggregator eventAggregator;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="FlyoutService" /> class.</summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="applicationCommands">The application commands.</param>
        public FlyoutService(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            this.regionManager = regionManager;

            ShowFlyoutCommand = new DelegateCommand<string>(ShowFlyout, CanShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(ShowFlyoutCommand);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets the show flyout command.</summary>
        /// <value>The show flyout command.</value>
        public ICommand ShowFlyoutCommand { get; }
        #endregion Properties

        #region Implementation of IFlyoutService
        /// <summary>Shows the flyout.</summary>
        /// <param name="flyoutName">Name of the flyout.</param>
        public void ShowFlyout(string flyoutName)
        {
            var region = regionManager.Regions[RegionNames.FLYOUT_REGION];

            var flyout = region?.Views?.FirstOrDefault(v => v is IFlyoutView && ((IFlyoutView) v).FlyoutName.Equals(flyoutName)) as Flyout;

            if (flyout == null) return;

            flyout.IsOpen = !flyout.IsOpen;

            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(Resources.StatusBarFlayoutDisplayed, flyoutName));
        }

        /// <summary>Determines whether this instance [can show flyout] the specified flyout name.</summary>
        /// <param name="flyoutName">Name of the flyout.</param>
        /// <returns>The value of can show flyout.</returns>
        public bool CanShowFlyout(string flyoutName)
        {
            return true;
        }
        #endregion Implementation of IFlyoutService
    }
}