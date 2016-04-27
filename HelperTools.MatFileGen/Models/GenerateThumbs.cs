namespace HelperTools.MatFileGen.Models
{
    using System.Windows.Media;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Events;

    /// <summary>The GenerateThumb.</summary>
    public class GenerateThumbs
    {
        #region Fields
        private readonly IUnityContainer unityContainer;
        private readonly IEventAggregator eventAggregator;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="GenerateThumbs" /> class.</summary>
        public GenerateThumbs()
        {
            unityContainer = ServiceLocator.Current.GetInstance<IUnityContainer>();
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets the thumb folder.</summary>
        /// <value>The thumb folder.</value>
        public string ThumbFolder { get; set; }

        /// <summary>Gets or sets a value indicating whether the outer frame should generate.</summary>
        /// <value><c>true</c> if outer frame should generate; otherwise, <c>false</c>.</value>
        public bool OuterFrame { get; set; }

        /// <summary>Gets or sets the color of the outer frame.</summary>
        /// <value>The color of the outer frame.</value>
        public Color OuterFrameColor { get; set; }

        /// <summary>Gets or sets the size of the outer frame.</summary>
        /// <value>The size of the outer frame.</value>
        public int OuterFrameSize { get; set; }

        /// <summary>Gets or sets a value indicating whether the inner frame should generate.</summary>
        /// <value><c>true</c> if inner frame should generate; otherwise, <c>false</c>.</value>
        public bool InnerFrame { get; set; }

        /// <summary>Gets or sets the color of the inner frame.</summary>
        /// <value>The color of the inner frame.</value>
        public Color InnerFrameColor { get; set; }

        /// <summary>Gets or sets the size of the inner frame.</summary>
        /// <value>The size of the inner frame.</value>
        public int InnerFrameSize { get; set; }
        #endregion Properties

        #region Methods
        void DoGeneration()
        {
        }
        #endregion Methods
    }
}