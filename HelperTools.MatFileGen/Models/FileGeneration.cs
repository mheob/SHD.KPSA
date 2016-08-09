namespace HelperTools.MatFileGen.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Infrastructure.Events;
    using Infrastructure.Interfaces;
    using Infrastructure.Services;
    using MahApps.Metro.Controls.Dialogs;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Events;
    using Prism.Logging;
    using Properties;
    using infraProps = Infrastructure.Properties;

    /// <summary>The FileGeneration.</summary>
    public class FileGeneration
    {
        #region Fields
        private readonly IUnityContainer unityContainer;
        private readonly IEventAggregator eventAggregator;

        private readonly int maxWidth = Settings.Default.MappingImageMaxWidth;
        private readonly int maxHeight = Settings.Default.MappingImageMaxHeight;
        private readonly string suffixPathForOrig = Settings.Default.MappingImageSuffixPathForOrig;

        private string logMessage;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="FileGeneration" /> class.</summary>
        public FileGeneration()
        {
            unityContainer = ServiceLocator.Current.GetInstance<IUnityContainer>();
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            logMessage = $"[{GetType().Name}] is initialized";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets the file collection.</summary>
        /// <value>The file collection.</value>
        public ObservableCollection<IFiles> FileCollection { get; set; }

        /// <summary>Gets or sets the generation files.</summary>
        /// <value>The generation files.</value>
        public ObservableCollection<IFiles> GenerationFiles { get; set; }

        /// <summary>Gets or sets the extension.</summary>
        /// <value>The extension.</value>
        public string Extension { get; set; }

        /// <summary>Gets or sets the selected path.</summary>
        /// <value>The selected path.</value>
        public string SelectedPath { get; set; }

        /// <summary>Gets or sets the solid RGB.</summary>
        /// <value>The solid RGB.</value>
        public byte[] SolidRgb { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is from JPG.</summary>
        /// <value><c>true</c> if this instance is from JPG; otherwise, <c>false</c>.</value>
        public bool IsFromJpg { get; set; }

        /// <summary>Gets or sets a value indicating whether thumbnails should generate.</summary>
        /// <value><c>true</c> if generate thumbnails; otherwise, <c>false</c>.</value>
        public bool GenerateThumb { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>Does the generation asynchronous.</summary>
        public async void DoGenerationAsync()
        {
            var metroDialog = new MetroMessageDisplayService(unityContainer);
            var controller =
                await metroDialog.ShowProgressAsync(infraProps.Resources.ProgressDialogTitle, infraProps.Resources.ProgressDialogPreviewContent);

            controller.SetIndeterminate();
            controller.SetCancelable(true);

            int sumFiles = GenerationFiles.Count;
            int curFile = 0;

            controller.Maximum = sumFiles;

            await Task.Delay(250);

            foreach (var file in GenerationFiles)
            {
                try
                {
                    var fileToGenerate = file.FullFilePath;
                    var filename = CharConverterService.ConvertCharsToAscii(file.FileName) + Extension;

                    FileInfo fi = new FileInfo(fileToGenerate);

                    if (File.Exists(fileToGenerate))
                    {
                        var pathForOrig = fi.DirectoryName + suffixPathForOrig;

                        if (!Directory.Exists(pathForOrig)) Directory.CreateDirectory(pathForOrig);

                        var savedOriginalFile = pathForOrig + file.FileName + Extension;
                        if (!File.Exists(savedOriginalFile)) File.Copy(fileToGenerate, savedOriginalFile, false);

                        await Task.Delay(50);

                        var imageService = new ImageService();
                        var resizedImage = imageService.ResizeImage(savedOriginalFile, maxWidth, maxHeight, false);
                        resizedImage.Save(fileToGenerate, ImageFormat.Jpeg);
                        resizedImage.Dispose();

                        await Task.Delay(50);

                        fileToGenerate = $@"{fi.DirectoryName}\{filename}";

                        await Task.Delay(50);
                    }

                    if (!file.FullFilePath.Equals($@"{fi.DirectoryName}\{filename}")) File.Move(file.FullFilePath, fileToGenerate);

                    await Task.Delay(50);

                    var rgb = File.Exists(fileToGenerate) ? ColorConverterService.GetRgbFromImage(fileToGenerate) : SolidRgb;

                    new GenerateThumbs().DoGeneration(fileToGenerate, rgb, IsFromJpg, ThumbnailService.ShowPreview.No);

                    await Task.Delay(50);

                    var generateFile = new GenerateMatFile(fileToGenerate, rgb, IsFromJpg);
                    generateFile.CreateMatFile();

                    await Task.Delay(50);

                    controller.SetProgress(++curFile);
                    controller.SetMessage(string.Format(infraProps.Resources.ProgressDialogRunningContent, curFile, sumFiles));

                    await Task.Delay(50);
                }
                catch (Exception ex)
                {
                    logMessage = $"[{GetType().Name}] Exception at {MethodBase.GetCurrentMethod()}: {ex}";
                    unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Exception, Priority.High);

                    DialogService.Exception(ex, DialogService.ExceptionType.Universal);
                }
            }

            await controller.CloseAsync();

            eventAggregator.GetEvent<FilesUpdateEvent>().Publish(FileCollection);

            if (controller.IsCanceled)
            {
                await metroDialog.ShowMessageAsync(infraProps.Resources.MessageDialogCancelTitle, infraProps.Resources.MessageDialogCancelContent);
                return;
            }

            if (await
                metroDialog.ShowMessageAsync(infraProps.Resources.MessageDialogCompleteTitle,
                    string.Format(infraProps.Resources.MessageDialogCompleteContent, "\n"),
                    MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
                ExternalProgramService.OpenExplorer(SelectedPath);

            eventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(Resources.StatusBarFilesGenerated, sumFiles));

            logMessage = $"[{GetType().Name}] MatFileGeneration completed";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);
        }
        #endregion Methods
    }
}