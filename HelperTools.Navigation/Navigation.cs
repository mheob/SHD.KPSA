namespace HelperTools.Navigation
{
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Microsoft.Practices.Unity;
    using Prism.Regions;
    using Views;

    /// <summary>The Navigation.</summary>
    /// <seealso cref="Navigation" />
    public class Navigation : PrismBaseModule
    {
        /// <summary>Initializes a new instance of the <see cref="Navigation" /> class.</summary>
        /// <param name="unityContainer">The unity container.</param>
        /// <param name="regionManager">The region manager.</param>
        public Navigation(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.LEFT_WINDOW_COMMANDS_REGION, typeof(LeftTitlebarCommands));
            regionManager.RegisterViewWithRegion(RegionNames.FLYOUT_REGION, typeof(NavigationFlyout));
            regionManager.RegisterViewWithRegion(RegionNames.MAIN_REGION, typeof(HomeTiles));
        }
    }
}