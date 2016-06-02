namespace HelperTools.Updater.ViewModels
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Infrastructure.Services;
    using Properties;

    /// <summary>The UpdateCheckerViewModel.</summary>
    public class UpdateCheckerViewModel : ViewModelBase
    {
        #region Fields
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="UpdateCheckerViewModel" /> class.</summary>
        public UpdateCheckerViewModel()
        {
            GetDataFromWebConfig();
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
        #endregion Properties

        #region Methods
        private void GetDataFromWebConfig()
        {
            var tmpPath = PathNames.TempFolderPath;
            if (!Directory.Exists(tmpPath)) Directory.CreateDirectory(tmpPath);

            var tmpFile = tmpPath + "version.txt";

            if (!File.Exists(tmpFile) || (File.GetCreationTimeUtc(tmpFile).Date - DateTime.Now).TotalHours < 2)
            {
                if (!WebService.ExistsOnServer(new Uri(Settings.Default.WebUpdateVersionFile))) return;

                new WebClient().DownloadFile(Settings.Default.WebUpdateVersionFile, tmpFile);
            }

            NewVersion = File.ReadLines(tmpFile).First();
            LastChange = string.Empty;

            foreach (var line in FileService.Read(tmpFile))
            {
                if (!line.StartsWith("[")) continue;

                LastChange += line.Replace("<br />", string.Empty) + "\n";
            }

            Directory.Delete(tmpPath, true);
        }
        #endregion Methods
    }
}