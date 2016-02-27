namespace SHD.KPSA.Tools.Utils
{
    using System;
    using System.IO;
    using System.Reflection;
    using Properties;

    /// <summary>
    /// A class that contains a collection of constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Gets the location of the application.
        /// </summary>
        public static string AppPath { get; } = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        /// <summary>
        /// Gets the path to the desktop location from the current user.
        /// </summary>
        public static string DesktopPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <summary>
        /// Gets the default path to the used 3er-Party applications.
        /// </summary>
        public static string DefaultThirdPartyFolder { get; } =
            Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Settings.Default.ThirdPartyFolder;

        /// <summary>
        /// Gets the state within a progress label.
        /// </summary>
        public static string ProgressLabelState { get; } = Resources.ProgressLabelState;

        /// <summary>
        /// Gets the location of the default temp directory in the system.
        /// </summary>
        public static string TempFolder { get; } = string.Format(Settings.Default.TempFolder, Path.GetTempPath());
    }
}