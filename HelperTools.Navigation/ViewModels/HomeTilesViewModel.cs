namespace HelperTools.Navigation.ViewModels
{
    using Infrastructure.Base;
    using Microsoft.Practices.Unity;
    using Prism.Logging;

    /// <summary>The HomeTilesViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class HomeTilesViewModel : ViewModelBase
    {
        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="HomeTilesViewModel" /> class.</summary>
        public HomeTilesViewModel()
        {
            Container.Resolve<ILoggerFacade>().Log("HomeTilesViewModel created", Category.Info, Priority.None);
        }
        #endregion Constructor
    }
}