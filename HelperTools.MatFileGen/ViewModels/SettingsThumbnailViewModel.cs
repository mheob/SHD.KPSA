namespace HelperTools.MatFileGen.ViewModels
{
    using System.Windows.Media;
    using Infrastructure.Base;
    using Properties;

    /// <summary>The SettingsThumbnailViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class SettingsThumbnailViewModel : ViewModelBase
    {
        #region Fields
        private bool generateThumb;
        private string thumbFolder;

        private bool generateOuterFrame;
        private Color outerFrameColor;
        private int outerFrameSize;

        private bool generateInnerFrame;
        private Color innerFrameColor;
        private int innerFrameSize;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="SettingsThumbnailViewModel" /> class.</summary>
        public SettingsThumbnailViewModel()
        {
            GenerateThumb = true;
            ThumbFolder = Settings.Default.ThumbFolder;

            GenerateOuterFrame = true;
            OuterFrameColor = Color.FromRgb(0, 0, 0);
            OuterFrameSize = Settings.Default.OuterFrameSize;

            GenerateInnerFrame = true;
            InnerFrameColor = Color.FromRgb(255, 255, 255);
            InnerFrameSize = Settings.Default.InnerFrameSize;
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets a value indicating whether thumbnails should generate.</summary>
        /// <value><c>true</c> if thumbnails should generate; otherwise, <c>false</c>.</value>
        public bool GenerateThumb
        {
            get { return generateThumb; }
            set
            {
                if (!SetProperty(ref generateThumb, value)) return;

                GenerateOuterFrame = value;
                GenerateInnerFrame = value;
            }
        }

        /// <summary>Gets or sets the thumb folder.</summary>
        /// <value>The thumb folder.</value>
        public string ThumbFolder
        {
            get { return thumbFolder; }
            set { SetProperty(ref thumbFolder, value); }
        }

        /// <summary>Gets or sets a value indicating whether the outer frame should generate.</summary>
        /// <value><c>true</c> if outer frame should generate; otherwise, <c>false</c>.</value>
        public bool GenerateOuterFrame
        {
            get { return generateOuterFrame; }
            set
            {
                if (!SetProperty(ref generateOuterFrame, value)) return;

                if (!GenerateOuterFrame) GenerateInnerFrame = value;
            }
        }

        /// <summary>Gets or sets the color of the outer frame.</summary>
        /// <value>The color of the outer frame.</value>
        public Color OuterFrameColor
        {
            get { return outerFrameColor; }
            set { SetProperty(ref outerFrameColor, value); }
        }

        /// <summary>Gets or sets the size of the outer frame.</summary>
        /// <value>The size of the outer frame.</value>
        public int OuterFrameSize
        {
            get { return outerFrameSize; }
            set { SetProperty(ref outerFrameSize, value); }
        }

        /// <summary>Gets or sets a value indicating whether inner frame should generate.</summary>
        /// <value><c>true</c> if inner frame should generate; otherwise, <c>false</c>.</value>
        public bool GenerateInnerFrame
        {
            get { return generateInnerFrame; }
            set { SetProperty(ref generateInnerFrame, value); }
        }

        /// <summary>Gets or sets the color of the inner frame.</summary>
        /// <value>The color of the inner frame.</value>
        public Color InnerFrameColor
        {
            get { return innerFrameColor; }
            set { SetProperty(ref innerFrameColor, value); }
        }

        /// <summary>Gets or sets the size of the inner frame.</summary>
        /// <value>The size of the inner frame.</value>
        public int InnerFrameSize
        {
            get { return innerFrameSize; }
            set { SetProperty(ref innerFrameSize, value); }
        }
        #endregion Properties

        #region Methods
        #endregion Methods
    }
}