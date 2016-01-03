using System;
using System.Diagnostics;
using System.IO;

namespace Utils
{
    /// <summary>
    /// Verarbeitungsklasse zum Starten bestimmter Programme
    /// </summary>
    public static class Programs
    {
        /// <summary>
        /// Öffnet den Windows-Explorer mit dem angegebenen Ordner-Pfad
        /// </summary>
        /// <param name="path">der Ordner-Pfad</param>
        public static void OpenExplorer(string path)
        {
            if (Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
        }

        /// <summary>
        /// Öffnet eine 3rd-Party-Application im Standardverzeichnis ./3rd-Party.
        /// </summary>
        /// <param name="progName">Die zu startende Datei.</param>
        /// <param name="path">Das Standard Unterverzeichnis.</param>
        public static void OpenThirdParty(string progName, string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Process.Start(path + progName);
                }
            }
            catch (Exception ex)
            {
                Dialogs.Exception(ex, Dialogs.ExceptionType.Universal);
            }
        }

        /// <summary>
        /// Öffnet eine 3rd-Party-Application (mit Parameterübergabe) im Standardverzeichnis ./3rd-Party.
        /// </summary>
        /// <param name="progName">Die zu startende Datei.</param>
        /// <param name="args">Etwaige Parameter.</param>
        /// <param name="path">Das Standard Unterverzeichnis.</param>
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
                Dialogs.Exception(ex, Dialogs.ExceptionType.Universal);
            }
        }

        /// <summary>
        /// Öffnet eine 3rd-Party-Application (mit Parameterübergabe) in angegebenen Unterverzeichnis.
        /// </summary>
        /// <param name="progName">Die zu startende Datei.</param>
        /// <param name="args">Etwaige Parameter.</param>
        /// <param name="isNotDefault"></param>
        /// <param name="path">Der komplette Pfad zur Datei.</param>
        public static void OpenThirdParty(string progName, string args, bool isNotDefault, string path)
        {
            try
            {
                if (!Directory.Exists(path)) return;

                if (args == "")
                {
                    Process.Start(path + "\\" + progName);
                }
                else
                {
                    var proc = new Process
                    {
                        StartInfo =
                        {
                            FileName = path + "\\" + progName,
                            Arguments = args,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };

                    proc.Start();
                }
            }
            catch (Exception ex)
            {
                Dialogs.Exception(ex, Dialogs.ExceptionType.Universal);
            }
        }
    }
}