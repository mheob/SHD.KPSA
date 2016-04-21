namespace HelperTools.Shell.ViewModels
{
    using Infrastructure.Base;
    using Microsoft.Practices.Unity;
    using Prism.Logging;

    /// <summary>The RightTitlebarCommandsViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class RightTitlebarCommandsViewModel : ViewModelBase
    {
        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="RightTitlebarCommandsViewModel" /> class.</summary>
        public RightTitlebarCommandsViewModel()
        {
            Container.Resolve<ILoggerFacade>().Log("RightTitlebarCommandsViewModel created", Category.Info, Priority.None);
        }
        #endregion Constructor
    }
}