namespace HelperTools.MatFileGen.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Media.Imaging;
    using Infrastructure.Base;
    using Infrastructure.Events;
    using Infrastructure.Interfaces;
    using Infrastructure.Services;
    using Models;

    /// <summary>The PreviewImageViewModel.</summary>
    public class PreviewImageViewModel : ViewModelBase
    {
        #region Fields
        private bool isPreviewVisible;
        private BitmapImage previewImage;
        private BitmapImage previewThumbnail;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="PreviewImageViewModel" /> class.</summary>
        public PreviewImageViewModel()
        {
            EventAggregator.GetEvent<SelectedFilesUpdateEvent>().Subscribe(OnSelectedFilesUpdateEvent);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets a value indicating whether this instance is preview visible.</summary>
        /// <value><c>true</c> if this instance is preview visible; otherwise, <c>false</c>.</value>
        public bool IsPreviewVisible
        {
            get { return isPreviewVisible; }
            set { SetProperty(ref isPreviewVisible, value); }
        }

        /// <summary>Gets or sets the preview image.</summary>
        /// <value>The preview image.</value>
        public BitmapImage PreviewImage
        {
            get { return previewImage; }
            set { SetProperty(ref previewImage, value); }
        }


        /// <summary>Gets or sets the preview thumbnail.</summary>
        /// <value>The preview thumbnail.</value>
        public BitmapImage PreviewThumbnail
        {
            get { return previewThumbnail; }
            set { SetProperty(ref previewThumbnail, value); }
        }
        #endregion Properties

        #region Methods
        private void OnSelectedFilesUpdateEvent(ObservableCollection<IFiles> files)
        {
            if (files.Count <= 0)
            {
                PreviewImage = null;
                PreviewThumbnail = null;
                IsPreviewVisible = false;

                return;
            }

            IsPreviewVisible = true;

            var fileToGenerate = files.FirstOrDefault();
            if (fileToGenerate == null) return;

            var generateThumb = new GenerateThumbs();
            PreviewImage = generateThumb.DoGeneration(fileToGenerate.FullFilePath, new byte[3], true, ThumbnailService.ShowPreview.Original);
            PreviewThumbnail = generateThumb.DoGeneration(fileToGenerate.FullFilePath, new byte[3], true, ThumbnailService.ShowPreview.Thumbnail);
        }
        #endregion Methods
    }
}