namespace HelperTools.MatFileGen.Models
{
    using System;
    using System.Collections.ObjectModel;
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
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="FileGeneration" /> class.</summary>
        public FileGeneration()
        {
            unityContainer = ServiceLocator.Current.GetInstance<IUnityContainer>();
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }
        #endregion Constructor

        #region Properties
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

            //if (!Directory.Exists(PathNames.TempFolderPath))
            //{
            //    Directory.CreateDirectory(PathNames.TempFolderPath);
            //}

            foreach (var file in GenerationFiles)
            {
                try
                {
                    // TODO: create the generation

                    var fileToGenerate = file.FullFilePath;

                    var rgb = File.Exists(fileToGenerate) ? ColorConverterService.GetRgbFromImage(fileToGenerate) : SolidRgb;

                    var generateFile = new GenerateMatFile(fileToGenerate, rgb, IsFromJpg);

                    generateFile.CreateMatFile();

                    //var tmpFile = PathNames.TempFolderPath + CharConverterService.ConvertCharsToAscii(file.FileName + Extension);
                    //
                    //File.Copy(file.FullFilePath, tmpFile, true);
                    //
                    //await Task.Delay(50);
                    //
                    //ExternalProgramService.OpenThirdParty("fix3ds.exe", " -m \"" + tmpFile + "\"", PathNames.ThirdPartyPath);
                    //
                    //await Task.Delay(50);
                    //
                    //File.Delete(file.FullFilePath);
                    //File.Copy(tmpFile, SelectedPath + Path.GetFileName(tmpFile), true);
                    //File.SetCreationTime(SelectedPath + Path.GetFileName(tmpFile), DateTime.Now);

                    await Task.Delay(50);

                    controller.SetProgress(++curFile);
                    controller.SetMessage(string.Format(infraProps.Resources.ProgressDialogRunningContent, curFile, sumFiles));

                    await Task.Delay(50);
                }
                catch (Exception ex)
                {
                    var logMessage = $"[{GetType().Name}] Exception at {MethodBase.GetCurrentMethod()}: {ex}";
                    unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Exception, Priority.High);

                    DialogService.Exception(ex, DialogService.ExceptionType.Universal);
                }
            }

            //if (Directory.Exists(PathNames.TempFolderPath))
            //{
            //    Directory.Delete(PathNames.TempFolderPath, true);
            //}

            await controller.CloseAsync();

            //GetFiles();

            if (controller.IsCanceled)
            {
                await metroDialog.ShowMessageAsync(infraProps.Resources.MessageDialogCancelTitle, infraProps.Resources.MessageDialogCancelContent);
                return;
            }

            if (await
                metroDialog.ShowMessageAsync(infraProps.Resources.MessageDialogCompleteTitle,
                    string.Format(infraProps.Resources.MessageDialogCompleteContent, "\n"),
                    MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                ExternalProgramService.OpenExplorer(SelectedPath);
            }

            eventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(Resources.StatusBarFilesGenerated, sumFiles));
        }
        #endregion Methods
    }
}