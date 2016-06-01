namespace HelperTools.Shell.ViewModels
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using Infrastructure.Base;
    using Infrastructure.Events;

    /// <summary>The MainWindowViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private string statusBarVersion = string.Empty;
        private string statusBarMessage = string.Empty;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="MainWindowViewModel" /> class.</summary>
        public MainWindowViewModel()
        {
            EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Subscribe(OnStatusBarMessageUpdateEvent);

            StatusBarVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets the status bar version.</summary>
        /// <value>The status bar version.</value>
        public string StatusBarVersion
        {
            get { return statusBarVersion; }
            set { SetProperty(ref statusBarVersion, value); }
        }

        /// <summary>Gets or sets the status bar message.</summary>
        /// <value>The status bar message.</value>
        public string StatusBarMessage
        {
            get { return statusBarMessage; }
            set { SetProperty(ref statusBarMessage, value); }
        }
        #endregion Properties

        #region Event-Handler
        [ExcludeFromCodeCoverage] // TODO: could maybe remove after creating a test of this
        private void OnStatusBarMessageUpdateEvent(string statusBarMsg)
        {
            StatusBarMessage = statusBarMsg;
        }
        #endregion Event-Handler
    }
}