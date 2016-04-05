namespace HelperTools.Shell.ViewModels
{
    using Infrastructure.Base;
    using Infrastructure.Events;
    using Microsoft.Practices.Unity;
    using Prism.Logging;

    /// <summary>The MainWindowViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private string statusBarMessage;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="MainWindowViewModel" /> class.</summary>
        public MainWindowViewModel()
        {
            EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Subscribe(OnStatusBarMessageUpdateEvent);

            Container.Resolve<ILoggerFacade>().Log("MainViewModel created", Category.Info, Priority.None);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets the status bar message.</summary>
        /// <value>The status bar message.</value>
        public string StatusBarMessage
        {
            get { return statusBarMessage; }
            set { SetProperty(ref statusBarMessage, value); }
        }
        #endregion Properties

        #region Event-Handler
        private void OnStatusBarMessageUpdateEvent(string statusBarMsg)
        {
            StatusBarMessage = statusBarMsg;
        }
        #endregion Event-Handler
    }
}