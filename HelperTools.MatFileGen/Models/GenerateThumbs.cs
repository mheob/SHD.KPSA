namespace HelperTools.MatFileGen.Models
{
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Infrastructure.Services;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Events;
    using Prism.Logging;
    using Properties;

    /// <summary>The GenerateThumb.</summary>
    public class GenerateThumbs
    {
        #region Fields
        private readonly IUnityContainer unityContainer;
        private readonly IEventAggregator eventAggregator;

        private readonly JsonService jsonService = new JsonService();
        private readonly string configFile = Settings.Default.SettingsThumbnailFile;
        private readonly ThumbnailService thumbnailService = new ThumbnailService();
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="GenerateThumbs" /> class.</summary>
        public GenerateThumbs()
        {
            unityContainer = ServiceLocator.Current.GetInstance<IUnityContainer>();
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            var logMessage = $"[{GetType().Name}] is initialized";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets a value indicating whether thumbnails should generate.</summary>
        /// <value><c>true</c> if thumbnails should generate; otherwise, <c>false</c>.</value>
        public bool GenerateThumb { get; set; }

        /// <summary>Gets or sets the thumb folder.</summary>
        /// <value>The thumb folder.</value>
        public string ThumbFolder { get; set; }

        /// <summary>Gets or sets a value indicating whether the outer frame should generate.</summary>
        /// <value><c>true</c> if outer frame should generate; otherwise, <c>false</c>.</value>
        public bool GenerateOuterFrame { get; set; }

        /// <summary>Gets or sets the color of the outer frame.</summary>
        /// <value>The color of the outer frame.</value>
        public Color OuterFrameColor { get; set; }

        /// <summary>Gets or sets the size of the outer frame.</summary>
        /// <value>The size of the outer frame.</value>
        public int OuterFrameSize { get; set; }

        /// <summary>Gets or sets a value indicating whether the inner frame should generate.</summary>
        /// <value><c>true</c> if inner frame should generate; otherwise, <c>false</c>.</value>
        public bool GenerateInnerFrame { get; set; }

        /// <summary>Gets or sets the color of the inner frame.</summary>
        /// <value>The color of the inner frame.</value>
        public Color InnerFrameColor { get; set; }

        /// <summary>Gets or sets the size of the inner frame.</summary>
        /// <value>The size of the inner frame.</value>
        public int InnerFrameSize { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>Does the thumbnail generation.</summary>
        /// <param name="file">The full path to the image.</param>
        /// <param name="rgb">The desired color as RGB values.</param>
        /// <param name="isFromJpg">if set to <c>true</c> a thumbnail should create from an existing image.</param>
        /// <param name="preview">The preview.</param>
        /// <returns>The preview BitmapImage.</returns>
        public BitmapImage DoGeneration(string file, byte[] rgb, bool isFromJpg, ThumbnailService.ShowPreview preview)
        {
            BitmapImage previewImage;

            ReadJson();

            if (preview == ThumbnailService.ShowPreview.Original)
            {
                GenerateInnerFrame = false;
                GenerateOuterFrame = false;
            }

            var outerColor = new SolidColorBrush(OuterFrameColor);
            var innerColor = new SolidColorBrush(InnerFrameColor);

            if (isFromJpg)
            {
                if (GenerateInnerFrame)
                    previewImage = thumbnailService.BuildThumbnail(file, ThumbFolder, outerColor, OuterFrameSize, innerColor, InnerFrameSize, preview,
                        GenerateThumb);
                else if (GenerateOuterFrame)
                    previewImage = thumbnailService.BuildThumbnail(file, ThumbFolder, outerColor, OuterFrameSize, preview, GenerateThumb);
                else
                    previewImage = thumbnailService.BuildThumbnail(file, ThumbFolder, preview, GenerateThumb);
            }
            else
            {
                if (GenerateInnerFrame)
                    previewImage = thumbnailService.BuildThumbnail(file, ThumbFolder, rgb, outerColor, OuterFrameSize, innerColor, InnerFrameSize,
                        preview, GenerateThumb);
                else if (GenerateOuterFrame)
                    previewImage = thumbnailService.BuildThumbnail(file, ThumbFolder, rgb, outerColor, OuterFrameSize, preview, GenerateThumb);
                else
                    previewImage = thumbnailService.BuildThumbnail(file, ThumbFolder, rgb, preview, GenerateThumb);
            }

            var logMessage = $"[{GetType().Name}] Thumbnail was generated";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);

            return previewImage;
        }

        private void ReadJson()
        {
            SettingsThumbnail settings = jsonService.ReadJson<SettingsThumbnail>(configFile);
            GenerateThumb = settings.GenerateThumb;
            ThumbFolder = settings.ThumbFolder;

            GenerateOuterFrame = settings.GenerateOuterFrame;
            OuterFrameColor = settings.OuterFrameColor;
            OuterFrameSize = settings.OuterFrameSize;

            GenerateInnerFrame = settings.GenerateInnerFrame;
            InnerFrameColor = settings.InnerFrameColor;
            InnerFrameSize = settings.InnerFrameSize;
        }
        #endregion Methods
    }
}