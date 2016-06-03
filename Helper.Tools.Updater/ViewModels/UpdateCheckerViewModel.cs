namespace HelperTools.Updater.ViewModels
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Infrastructure.Services;
    using MahApps.Metro.Controls.Dialogs;
    using Microsoft.Practices.Unity;
    using Models;
    using Prism.Commands;
    using Prism.Logging;
    using Properties;
    using infraProps = Infrastructure.Properties;

    /// <summary>The UpdateCheckerViewModel.</summary>
    public class UpdateCheckerViewModel : ViewModelBase
    {
        #region Fields
        private readonly JsonService jsonService = new JsonService();
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="UpdateCheckerViewModel" /> class.</summary>
        public UpdateCheckerViewModel()
        {
            GetDataFromWebConfig();

            StartUpdateCommand = new DelegateCommand(StartUpdate, CanStartUpdate);

            if (!CanStartUpdate()) NoAccessToWebservice();
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets the new version.</summary>
        /// <value>The new version.</value>
        public string NewVersion { get; private set; }

        /// <summary>Gets or sets the current version.</summary>
        /// <value>The current version.</value>
        public string CurrentVersion => Assembly.GetEntryAssembly().GetName().Version.ToString();

        /// <summary>Gets or sets the last change.</summary>
        /// <value>The last change.</value>
        public string LastChange { get; private set; }

        /// <summary>Gets or sets the location.</summary>
        /// <value>The location.</value>
        public Uri Location { get; private set; }

        /// <summary>Gets the command to start the working thread.</summary>
        /// <value>The start generation command.</value>
        public DelegateCommand StartUpdateCommand { get; }
        #endregion Properties

        #region Methods
        private void GetDataFromWebConfig()
        {
            var tmpPath = PathNames.TempFolderPath;
            if (!Directory.Exists(tmpPath)) Directory.CreateDirectory(tmpPath);

            var tmpFile = tmpPath + Settings.Default.VersionJsonFileName;

            if (!File.Exists(tmpFile) || (File.GetCreationTimeUtc(tmpFile).Date - DateTime.Now).TotalHours <= 1)
            {
                if (!WebService.ExistsOnServer(Settings.Default.WebUpdateVersionFile)) return;

                new WebClient().DownloadFile(Settings.Default.WebUpdateVersionFile.ToString(), tmpFile);
            }

            ReadJson();

            Directory.Delete(tmpPath, true);
        }

        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        private bool CanStartUpdate()
        {
            if (!WebService.ExistsOnServer(Settings.Default.WebUpdateVersionFile)) return false;
            if (!WebService.ExistsOnServer(Location)) return false;

            return true;
        }

        private void StartUpdate()
        {
            // TODO: start the update / download
        }

        private void ReadJson()
        {
            SettingsUpdate settings = jsonService.ReadJson<SettingsUpdate>(Settings.Default.VersionJsonFileName, JsonService.StoringArea.Tempfolder);
            NewVersion = settings.Version.ToString();
            LastChange = settings.LastChangesDe.Replace("<br />", "\n"); // TODO: switching between english and german | locale == "de" ? de : en;
            Location = settings.Location;
        }

        private async void NoAccessToWebservice()
        {
            var metroDialog = new MetroMessageDisplayService(Container);

            MainWindow.MetroDialogOptions.AffirmativeButtonText = infraProps.Resources.DialogOk;

            await
                metroDialog.ShowMessageAsync(Resources.NoAccessToWebserviceDialogTitle,
                    string.Format(Resources.NoAccessToWebserviceDialogContent, "\n"), MessageDialogStyle.Affirmative, MainWindow.MetroDialogOptions);

            var logMessage = $"[{GetType().Name}] the UpdateCheckerViewModel can't access to the webservice";
            Container.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);
        }
    }
    #endregion Methods
}