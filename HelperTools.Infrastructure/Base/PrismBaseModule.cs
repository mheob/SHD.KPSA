namespace HelperTools.Infrastructure.Base
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Regions;

    /// <summary>The PrismBaseModule.</summary>
    /// <seealso cref="Prism.Modularity.IModule" />
    public abstract class PrismBaseModule : IModule
    {
        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="PrismBaseModule" /> class.</summary>
        /// <param name="unityContainer">The unity container.</param>
        /// <param name="regionManager">The region manager.</param>
        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public PrismBaseModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            UnityContainer = unityContainer;
            RegionManager = regionManager;
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets the unity container.</summary>
        /// <value>The unity container.</value>
        public IUnityContainer UnityContainer { get; private set; }

        /// <summary>Gets the region manager.</summary>
        /// <value>The region manager.</value>
        public IRegionManager RegionManager { get; private set; }
        #endregion Properties

        #region Interface IModule
        /// <summary>Notifies the module that it has be initialized.</summary>
        public virtual void Initialize()
        {
        }
        #endregion Interface IModule
    }
}