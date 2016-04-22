namespace HelperTools.Navigation.ViewModels
{
    using Infrastructure.Base;
    using Microsoft.Practices.Unity;
    using Prism.Logging;

    /// <summary>The LeftTitlebarCommandsViewModel</summary>
    /// <seealso cref="ViewModelBase" />
    public class LeftTitlebarCommandsViewModel : ViewModelBase
    {
        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="LeftTitlebarCommandsViewModel" /> class.</summary>
        public LeftTitlebarCommandsViewModel()
        {
            Container.Resolve<ILoggerFacade>().Log("LeftTitlebarCommandsViewModel created", Category.Info, Priority.None);
        }
        #endregion Constructor
    }
}