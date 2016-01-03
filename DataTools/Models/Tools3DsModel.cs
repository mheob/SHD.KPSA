using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DataTools.Utils;
using Utils;

namespace DataTools.Models
{
    public class Tools3DsModel
    {
        public string FilePath { get; set; }
        public List<string> Files { get; set; }
        public int CurPos { get; set; }

        public void RemoveColorIn3Ds(Label labelProgress, ProgressBar progressBarProgress, List<string> selectedFiles)
        {
            int sumFiles = selectedFiles.Count;

            if (FilePath.Length <= 0 || sumFiles <= 0) return;

            labelProgress.Visibility = Visibility.Visible;
            progressBarProgress.Visibility = Visibility.Visible;
            progressBarProgress.Maximum = sumFiles;
            CurPos = 0;

            foreach (var t in selectedFiles.Select(file => new Thread(new ThreadStart(delegate
            {
                progressBarProgress.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(delegate
                {
                    if (File.Exists(file))
                    {
                        try
                        {
                            Programs.OpenThirdParty("fix3ds.exe", " -m \"" + file + "\"", Constants.DefaultThirdPartyFolder);

                            CurPos++;
                        }
                        catch (Exception ex)
                        {
                            Dialogs.Exception(ex, Dialogs.ExceptionType.Universal);
                        }
                    }

                    progressBarProgress.Value = CurPos;
                    labelProgress.Content = string.Format(Constants.ProgressLabelState, CurPos, sumFiles);

                    Thread.Sleep(100);
                }
                    ));
            }
                ))))
            {
                t.Start();
            }

            if (Dialogs.ProgressFinished(sumFiles) == MessageBoxResult.Yes)
            {
                Programs.OpenExplorer(FilePath);
            }

            foreach (var filename in selectedFiles.Select(Path.GetFileName))
            {
                File.Copy(Constants.TempFolder + filename, FilePath + "\\" + filename, true);
            }

            Directory.Delete(Constants.TempFolder, true);

            labelProgress.Content = "";
            progressBarProgress.Visibility = Visibility.Hidden;
        }
    }
}