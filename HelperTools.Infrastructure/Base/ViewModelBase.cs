namespace HelperTools.Infrastructure.Base
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Events;
    using Prism.Regions;

    /// <summary>The ViewModelBase.</summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public abstract class ViewModelBase : BindableBase
    {
        #region Fields
        private IUnityContainer unityContainer;
        private IRegionManager regionManager;
        private IEventAggregator eventAggregator;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="ViewModelBase" /> class.</summary>
        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public ViewModelBase()
        {
            Container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets the container.</summary>
        /// <value>The container.</value>
        public IUnityContainer Container
        {
            get { return unityContainer; }
            private set { SetProperty(ref unityContainer, value); }
        }


        /// <summary>Gets the region manager.</summary>
        /// <value>The region manager.</value>
        public IRegionManager RegionManager
        {
            get { return regionManager; }
            private set { SetProperty(ref regionManager, value); }
        }


        /// <summary>Gets the event aggregator.</summary>
        /// <value>The event aggregator.</value>
        public IEventAggregator EventAggregator
        {
            get { return eventAggregator; }
            private set { SetProperty(ref eventAggregator, value); }
        }
        #endregion Properties
    }
}