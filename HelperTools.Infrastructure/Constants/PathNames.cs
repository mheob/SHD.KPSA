namespace HelperTools.Infrastructure.Constants
{
    using System;
    using System.IO;
    using System.Reflection;
    using Properties;

    /// <summary>Constants with the PathNames.</summary>
    public static class PathNames
    {
        /// <summary>Gets the application path.</summary>
        /// <value>The application path.</value>
        public static string AppPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";

        /// <summary>Gets the configuration path.</summary>
        /// <value>The configuration path.</value>
        public static string ConfigPath => AppPath + Settings.Default.ConfigFolder;


        /// <summary>Gets the path to the current user desktop path.</summary>
        /// <value>The desktop path.</value>
        public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\";

        /// <summary>Gets the location of the default temp directory in the system.</summary>
        /// <value>The temporary folder path.</value>
        public static string TempFolderPath => string.Format(Settings.Default.TempFolder, Path.GetTempPath());

        /// <summary>Gets the path to the used 3rd-Party applications.</summary>
        /// <value>The third party path.</value>
        public static string ThirdPartyPath => AppPath + Settings.Default.ThirdPartyFolder;
    }
}