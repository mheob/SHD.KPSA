namespace HelperTools.Clean3Ds
{
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Microsoft.Practices.Unity;
    using Prism.Regions;
    using SharedModules.Views;

    /// <summary>The Clean3Ds.</summary>
    /// <seealso cref="PrismBaseModule" />
    public class Clean3Ds : PrismBaseModule
    {
        /// <summary>Initializes a new instance of the <see cref="PrismBaseModule" /> class.</summary>
        /// <param name="unityContainer">The unity container.</param>
        /// <param name="regionManager">The region manager.</param>
        public Clean3Ds(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.MAIN_REGION, typeof(Views.Clean3Ds));

            regionManager.RegisterViewWithRegion(Constants.RegionNames.PATH_SELECTION_REGION, typeof(PathSelection));
            regionManager.RegisterViewWithRegion(Constants.RegionNames.DATA_GRID_FILES_REGION, typeof(DataGridFiles));
            regionManager.RegisterViewWithRegion(Constants.RegionNames.FOOTER_START_REGION, typeof(FooterStart));
        }
    }
}