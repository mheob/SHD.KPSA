namespace HelperTools.Updater
{
    using System;
    using System.Net;
    using Infrastructure.Services;
    using Models;

    /// <summary>The UpdaterConfig.</summary>
    public class UpdaterConfig
    {
        #region Fields
        private const string CONFIG_FILE = ""; // TODO: outsourcing
        #endregion Fields

        #region Constructor       
        /// <summary>Creates a new UpdaterConfig object</summary>
        public UpdaterConfig(Version version, Uri uri, string fileName, string md5, string description, string launchArgs)
        {
            Version = version;
            Uri = uri;
            FileName = fileName;
            Md5 = md5;
            Description = description;
            LaunchArgs = launchArgs;
        }
        #endregion Constructor

        #region Properties
        /// <summary>The update version #</summary>
        public Version Version { get; set; }

        /// <summary>The location of the update binary</summary>
        public Uri Uri { get; set; }

        /// <summary>The file name of the binary for use on local computer</summary>
        public string FileName { get; set; }

        /// <summary>The MD5 of the update's binary</summary>
        public string Md5 { get; set; }

        /// <summary>The update's description</summary>
        public string Description { get; set; }

        /// <summary>The arguments to pass to the updated application on startup</summary>
        public string LaunchArgs { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>Checks if update's version is newer than the old version</summary>
        /// <param name="version">Application's current version</param>
        /// <returns>If the update's version # is newer</returns>
        public bool IsNewerThan(Version version)
        {
            return Version > version;
        }

        /// <summary>Checks the Uri to make sure file exist</summary>
        /// <param name="location">The Uri of the update.xml</param>
        /// <returns>If the file exists</returns>
        public static bool ExistsOnServer(Uri location)
        {
            try
            {
                HttpWebResponse resp = (HttpWebResponse) ((HttpWebRequest) WebRequest.Create(location.AbsoluteUri)).GetResponse();
                resp.Close();

                return resp.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Parses the update.xml into UpdaterConfig object</summary>
        /// <param name="location">Uri of update.xml on server</param>
        /// <param name="appId">The application's ID</param>
        /// <returns>The UpdaterConfig object with the data, or null of any errors</returns>
        public static UpdaterConfig Parse(Uri location, string appId)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (s, ce, ch, ssl) => true;

                SettingsUpdate settings = new JsonService().ReadJson<SettingsUpdate>(CONFIG_FILE);
                var version = Version.Parse(settings.Version);
                var url = settings.Url;
                var fileName = settings.FileName;
                var md5 = settings.Md5;
                var description = settings.Description;
                var launchArgs = settings.LaunchArgs;

                return new UpdaterConfig(version, new Uri(url), fileName, md5, description, launchArgs);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion Methods
    }
}