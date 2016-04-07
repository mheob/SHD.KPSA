namespace HelperTools.MatFileGen
{
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Microsoft.Practices.Unity;
    using Prism.Regions;
    using SharedModules.Views;
    using Views;

    /// <summary>The MatFileGen.</summary>
    /// <seealso cref="PrismBaseModule" />
    public class MatFileGen : PrismBaseModule
    {
        /// <summary>Initializes a new instance of the <see cref="PrismBaseModule" /> class.</summary>
        /// <param name="unityContainer">The unity container.</param>
        /// <param name="regionManager">The region manager.</param>
        public MatFileGen(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.MAIN_REGION, typeof (Views.MatFileGen));

            regionManager.RegisterViewWithRegion(Constants.RegionNames.PATH_SELECTION_REGION, typeof (PathSelection));
            regionManager.RegisterViewWithRegion(Constants.RegionNames.DATA_GRID_FILES_REGION, typeof (DataGridFiles));
            regionManager.RegisterViewWithRegion(Constants.RegionNames.FOOTER_START_REGION, typeof (FooterStart));

            regionManager.RegisterViewWithRegion(Constants.RegionNames.SETTINGS_ATTRIBUTES_REGION, typeof (SettingsAttributes));
            regionManager.RegisterViewWithRegion(Constants.RegionNames.SETTINGS_SOLID_REGION, typeof (SettingsSolid));
            regionManager.RegisterViewWithRegion(Constants.RegionNames.SETTINGS_THUMBNAIL_REGION, typeof (SettingsThumbnail));
        }
    }
}