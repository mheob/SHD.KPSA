namespace HelperTools.Infrastructure.Services
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>Processing class to start certain extern programs.</summary>
    public static class ExternalProgramService
    {
        /// <summary>Opens Windows Explorer with the specified folder path.</summary>
        /// <param name="path">The folder path to use.</param>
        public static void OpenExplorer(string path)
        {
            if (Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
        }

        /// <summary>Opens a 3rd party application (with params) in the specified subdirectory.</summary>
        /// <param name="progName">The file to start.</param>
        /// <param name="args">The desired parameters to be used.</param>
        /// <param name="path">The subdirectory.</param>
        public static void OpenThirdParty(string progName, string args, string path)
        {
            try
            {
                if (!Directory.Exists(path)) return;

                var proc = new Process
                {
                    StartInfo =
                    {
                        FileName = path + progName,
                        Arguments = args,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };


                proc.Start();
            }
            catch (Exception ex)
            {
                DialogService.Exception(ex, DialogService.ExceptionType.Universal);
            }
        }
    }
}