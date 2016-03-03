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
        /// Is the possible ComboBox versions to choose from.
        /// </summary>
        public enum ComboBoxVariant
        {
            /// <summary>
            /// The variant "Scale".
            /// </summary>
            Scale,

            /// <summary>
            /// The variant "Rotate".
            /// </summary>
            Rotate
        };

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
        public static string DefaultThirdPartyFolder { get; } = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +
                                                                Settings.Default.ThirdPartyFolder;

        /// <summary>
        /// Gets the state within a progress label.
        /// </summary>
        public static string ProgressLabelState { get; } = Resources.ProgressLabelState;

        /// <summary>
        /// Gets the location of the default temp directory in the system.
        /// </summary>
        public static string TempFolder { get; } = string.Format(Settings.Default.TempFolder, Path.GetTempPath());

        /// <summary>
        /// Fills a combo box with the predefined values.
        /// </summary>
        /// <param name="cbbVar">the ComboBox variant that is to be filled.</param>
        /// <returns>An array of all items.</returns>
        public static string[] GetComboBoxItems(ComboBoxVariant cbbVar)
        {
            switch (cbbVar)
            {
                case ComboBoxVariant.Scale:
                    string[] scale =
                    {
                        "0.1", "0.25", "0.33", "0.5", "0.67", "0.75", "1.0", "1.25", "1.5", "1.75", "2.0", "2.5"
                    };
                    return scale;
                case ComboBoxVariant.Rotate:
                    string[] rotate = {"0", "90"};
                    return rotate;
                default:
                    string[] sArr = {"0"};
                    return sArr;
            }
        }
    }
}