namespace SHD.KPSA.Utils
{
    using System;
    using System.Windows;
    using Ookii.Dialogs.Wpf;
    using Properties;

    /// <summary>
    /// This class provides different dialogs.
    /// </summary>
    public static class Dialogs
    {
        /// <summary>
        /// Gave the (own) possible error variations to choose from.
        /// </summary>
        public enum ExceptionType
        {
            /// <summary>
            /// The generic variation.
            /// </summary>
            Universal,

            /// <summary>
            /// The variant for data access problems.
            /// </summary>
            FileAcces
        }

        /// <summary>
        /// Displays a dialog of error messages.
        /// </summary>
        /// <param name="ex">The thrown error.</param>
        /// <param name="exType">The error variances, after which the corresponding text is output.</param>
        public static void Exception(Exception ex, ExceptionType exType)
        {
            string text;
            string title;

            switch (exType)
            {
                case ExceptionType.FileAcces:
                    text = Resources.ExceptionDialogFileAccessText;
                    title = Resources.ExceptionDialogFileAccessTitle;
                    break;
                case ExceptionType.Universal:
                    text = Resources.ExceptionDialogDefaultText;
                    title = Resources.ExceptionDialogDefaultTitle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(exType), exType, null);
            }

            MessageBox.Show(string.Format(text, ex), title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Displays a warning dialog when completing a Processing.
        /// </summary>
        public static void NothingToDo()
        {
            MessageBox.Show(Resources.NothingToDoDialogText, Resources.NothingToDoDialogTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Provides a dialog to select a folder available.
        /// </summary>
        /// <param name="path">The initial path.</param>
        /// <returns>The chosen path, if the dialog was confirmed with OK.</returns>
        public static string OpenFolderDialog(string path)
        {
            var dlg = new VistaFolderBrowserDialog
            {
                Description = Resources.OpenFolderDescription,
                UseDescriptionForTitle = true,
                SelectedPath = path
            };

            var showDialog = dlg.ShowDialog();
            if (showDialog != null && (bool) showDialog)
            {
                return dlg.SelectedPath;
            }

            return null;
        }

        /// <summary>
        /// Displays a dialog of showing the current process.
        /// </summary>
        /// <param name="sumFiles">The number of files processed.</param>
        public static void ProgressDialog(int sumFiles)
        {
            var dlg = new ProgressDialog
            {
                Description = "",
                WindowTitle = "",
                ShowCancelButton = false,
                ShowTimeRemaining = true
            };

            dlg.ShowDialog();
        }

        /// <summary>
        /// Displays a dialog as completion of processing.
        /// </summary>
        /// <param name="sumFiles">The number of files processed.</param>
        /// <returns>Returns the value, if yes or no is selected.</returns>
        public static MessageBoxResult ProgressFinished(int sumFiles)
        {
            var nl = Environment.NewLine;

            return MessageBox.Show(string.Format(Resources.ProgressFinishedDialogText, sumFiles, nl, nl), Resources.ProgressFinishedDialogTitle,
                MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}