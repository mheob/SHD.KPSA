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
    using Properties;

    /// <summary>The PreviewImageViewModel.</summary>
    public class PreviewImageViewModel : ViewModelBase
    {
        #region Fields
        private const string EXTENSION = ".jpg";

        private bool isInitializedSolidRgb;
        private bool isPreviewVisible;
        private BitmapImage previewImage;
        private BitmapImage previewThumbnail;
        private string selectedPath;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="PreviewImageViewModel" /> class.</summary>
        public PreviewImageViewModel()
        {
            EventAggregator.GetEvent<SelectedPathUpdateEvent>().Subscribe(OnSelectedPathUpdateEvent);
            EventAggregator.GetEvent<SelectedMfgFilesUpdateEvent>().Subscribe(OnSelectedFilesUpdateEvent);
            EventAggregator.GetEvent<SolidRgbUpdateEvent>().Subscribe(OnSolidRgbUpdateEvent);
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

        /// <summary>Gets or sets the selected path.</summary>
        /// <value>The selected path.</value>
        public string SelectedPath
        {
            get { return selectedPath; }
            set { SetProperty(ref selectedPath, value); }
        }
        #endregion Properties

        #region Methods
        private void OnSelectedPathUpdateEvent(string path)
        {
            SelectedPath = string.IsNullOrEmpty(path) ? selectedPath : path;
        }

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

        private void OnSolidRgbUpdateEvent(byte[] rgb)
        {
            if (!isInitializedSolidRgb)
            {
                PreviewImage = null;
                PreviewThumbnail = null;
                IsPreviewVisible = false;
                isInitializedSolidRgb = true;

                return;
            }

            IsPreviewVisible = true;

            var settings = new JsonService().ReadJson<SettingsSolid>(Settings.Default.SettingsMfgSolidFile);
            var file = SelectedPath + settings.SolidColorName + EXTENSION;

            var generateThumb = new GenerateThumbs();
            PreviewImage = generateThumb.DoGeneration(file, rgb, false, ThumbnailService.ShowPreview.Original);
            PreviewThumbnail = generateThumb.DoGeneration(file, rgb, false, ThumbnailService.ShowPreview.Thumbnail);
        }
        #endregion Methods
    }
}