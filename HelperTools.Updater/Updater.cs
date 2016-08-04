namespace HelperTools.Updater
{
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Microsoft.Practices.Unity;
    using Prism.Regions;
    using Views;

    /// <summary>The Updater.</summary>
    public class Updater : PrismBaseModule
    {
        /// <summary>Initializes a new instance of the <see cref="Updater" /> class.</summary>
        /// <param name="unityContainer">The unity container.</param>
        /// <param name="regionManager">The region manager.</param>
        public Updater(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.MAIN_REGION, typeof(UpdateChecker));
        }
    }
}