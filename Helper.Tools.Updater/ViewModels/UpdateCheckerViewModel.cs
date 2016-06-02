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
    public class UpdateCheckerViewModel : ViewModelBase, IUpdateable
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
        public string CurrentVersion { get; set; } = Assembly.GetEntryAssembly().GetName().Version.ToString();

        /// <summary>Gets or sets the last change.</summary>
        /// <value>The last change.</value>
        public string LastChange { get; set; }

        #region Implementation of IUpdateable
        /// <summary>The name of your application as you want it displayed on the update form.</summary>
        public string ApplicationName => "TestApp";

        /// <summary>An identifier string to use to identify your application in the update.json.</summary>
        /// <remarks>Should be the same as your appId in the update.json.</remarks>
        public string ApplicationId => "TestApp";

        /// <summary>The location of the update.json on a server.</summary>
        public Uri UpdateJsonLocation => new Uri("");
        #endregion

        #endregion Properties

        #region Methods
        private void GetDataFromWebConfig()
        {
            var tmpPath = PathNames.TempFolderPath;
            if (!Directory.Exists(tmpPath)) Directory.CreateDirectory(tmpPath);

            var tmpFile = tmpPath + "version.txt";
            new WebClient().DownloadFile(Settings.Default.WebUpdateVersionFile, tmpFile);

            NewVersion = File.ReadLines(tmpFile).First();

            LastChange = string.Empty;

            foreach (var line in FileService.Read(tmpFile))
            {
                if (line.StartsWith("[")) LastChange += line + "\n";
            }

            Directory.Delete(tmpPath, true);
        }
        #endregion Methods
    }
}