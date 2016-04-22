namespace HelperTools.Navigation.ViewModels
{
    using Infrastructure.Base;
    using Microsoft.Practices.Unity;
    using Prism.Logging;

    /// <summary>The NavigationFlyoutViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class NavigationFlyoutViewModel : ViewModelBase
    {
        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="NavigationFlyoutViewModel" /> class.</summary>
        public NavigationFlyoutViewModel()
        {
            Container.Resolve<ILoggerFacade>().Log("NavigationFlyoutViewModel created", Category.Info, Priority.None);
        }
        #endregion Constructor
    }
}