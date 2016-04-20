namespace HelperTools.Shell.Views
{
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Logging;

    /// <summary>Interaction logic for RightTitlebarCommands.xaml</summary>
    public partial class RightTitlebarCommands
    {
        /// <summary>Initializes a new instance of the <see cref="RightTitlebarCommands" /> class.</summary>
        public RightTitlebarCommands()
        {
            InitializeComponent();

            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            container.Resolve<ILoggerFacade>().Log("RightTitlebarCommandsView created", Category.Info, Priority.None);
        }
    }
}