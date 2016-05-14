namespace HelperTools.MatFileGen.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Media.Imaging;
    using Infrastructure.Base;
    using Infrastructure.Events;
    using Infrastructure.Interfaces;
    using Models;

    /// <summary>The PreviewImageViewModel.</summary>
    public class PreviewImageViewModel : ViewModelBase
    {
        #region Fields
        private BitmapImage previewImage;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="PreviewImageViewModel" /> class.</summary>
        public PreviewImageViewModel()
        {
            EventAggregator.GetEvent<SelectedFilesUpdateEvent>().Subscribe(OnSelectedFilesUpdateEvent);
            EventAggregator.GetEvent<PreviewImageUpdateEvent>().Subscribe(OnPreviewImageUpdateEvent);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets the preview image.</summary>
        /// <value>The preview image.</value>
        public BitmapImage PreviewImage
        {
            get { return previewImage; }
            set { SetProperty(ref previewImage, value); }
        }
        #endregion Properties

        #region Methods
        private void OnSelectedFilesUpdateEvent(ObservableCollection<IFiles> files)
        {
            if (files.Count <= 0) return;

            var fileToGenerate = files.FirstOrDefault();
            if (fileToGenerate == null) return;

            var generateThumb = new GenerateThumbs
            {
                GenerateInnerFrame = false,
                GenerateOuterFrame = false
            };

            generateThumb.DoGeneration(fileToGenerate.FullFilePath, new byte[3], true, true);
        }

        private void OnPreviewImageUpdateEvent(BitmapImage image)
        {
            PreviewImage = image;
        }
        #endregion Methods
    }
}