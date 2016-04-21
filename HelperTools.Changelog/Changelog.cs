namespace HelperTools.Changelog
{
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Microsoft.Practices.Unity;
    using Prism.Regions;
    using Views;

    /// <summary>The Changelog.</summary>
    /// <seealso cref="PrismBaseModule" />
    public class Changelog : PrismBaseModule
    {
        /// <summary>Initializes a new instance of the <see cref="PrismBaseModule" /> class.</summary>
        /// <param name="unityContainer">The unity container.</param>
        /// <param name="regionManager">The region manager.</param>
        public Changelog(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.MAIN_REGION, typeof(ChangelogList));
        }
    }
}