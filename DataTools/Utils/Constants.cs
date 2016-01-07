using System;
using System.IO;
using System.Reflection;
using DataTools.Properties;

namespace DataTools.Utils
{
    public static class Constants
    {
        public static string AppPath { get; } = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static string DesktopPath { get; set; } =
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string RegionContent { get; } = "ContentRegion";

        public static string DefaultThirdPartyFolder { get; } =
            Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +
            Settings.Default.ThirdPartyFolder;

        public static string ProgressLabelState { get; } = Resources.ProgressLabelState;

        public static string TempFolder { get; } = string.Format(Settings.Default.TempFolder,
            Path.GetTempPath());
    }
}