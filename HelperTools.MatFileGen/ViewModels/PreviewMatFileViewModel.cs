namespace HelperTools.MatFileGen.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Infrastructure.Base;
    using Infrastructure.Events;
    using Infrastructure.Interfaces;
    using Infrastructure.Services;
    using Models;

    /// <summary>The PreviewMatFileViewModel.</summary>
    public class PreviewMatFileViewModel : ViewModelBase
    {
        #region Fields
        private bool isPreviewVisible;
        private List<string> matFilePreview;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="PreviewMatFileViewModel" /> class.</summary>
        public PreviewMatFileViewModel()
        {
            EventAggregator.GetEvent<SelectedMfgFilesUpdateEvent>().Subscribe(OnSelectedFilesUpdateEvent);
            EventAggregator.GetEvent<MatFilePreviewUpdateEvent>().Subscribe(OnMatFilePreviewUpdateEvent);
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

        /// <summary>Gets or sets the mat file preview.</summary>
        /// <value>The mat file preview.</value>
        public List<string> MatFilePreview
        {
            get { return matFilePreview; }
            set { SetProperty(ref matFilePreview, value); }
        }
        #endregion Properties

        #region Methods
        private void OnSelectedFilesUpdateEvent(ObservableCollection<IFiles> files)
        {
            if (files.Count <= 0)
            {
                IsPreviewVisible = false;
                return;
            }

            IsPreviewVisible = true;

            var fileToPreview = files.FirstOrDefault();
            if (fileToPreview == null) return;

            var generateFile = new GenerateMatFile(fileToPreview.FullFilePath, ColorConverterService.GetRgbFromImage(fileToPreview.FullFilePath));
            generateFile.CreateMatFile(true);
        }

        private void OnMatFilePreviewUpdateEvent(List<string> fileContent)
        {
            var first = fileContent.First();
            if (first != null && first.Length < 5)
            {
                IsPreviewVisible = false;

                return;
            }

            IsPreviewVisible = true;

            MatFilePreview = fileContent;
        }
        #endregion Methods
    }
}