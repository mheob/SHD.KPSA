namespace HelperTools.Changelog.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Infrastructure.Services;
    using Properties;

    /// <summary>The ChangelogListViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class ChangelogListViewModel : ViewModelBase
    {
        #region Fields
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="ChangelogListViewModel" /> class.</summary>
        public ChangelogListViewModel()
        {
            DesignChangelogContent();
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets the changelog file location.</summary>
        /// <value>The changelog file location.</value>
        public string ChangelogFile { get; } = PathNames.AppPath + Settings.Default.PathToChangelog;

        /// <summary>Gets the build note.</summary>
        /// <value>The build note.</value>
        public string BuildNote
        {
            get
            {
                DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;

                return string.Format(Resources.LastChangeText, buildDate);
            }
        }

        /// <summary>Gets or sets the changelog lines.</summary>
        /// <value>The changelog lines.</value>
        public ObservableCollection<Label> ChangelogLines { get; set; } = new ObservableCollection<Label>();
        #endregion Properties

        #region Methods
        [ExcludeFromCodeCoverage] // TODO: could maybe remove after creating a test of this
        private void DesignChangelogContent()
        {
            if (!File.Exists(ChangelogFile))
                return;

            foreach (var line in FileService.Read(ChangelogFile))
            {
                if (line.Equals("# CHANGELOG")) continue;

                var label = new Label
                {
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = 14
                };

                var tmpLine = line;

                if (tmpLine.EndsWith("<br />"))
                    tmpLine = tmpLine.Remove(tmpLine.Length - 6, 6);

                if (tmpLine.StartsWith("## "))
                {
                    tmpLine = tmpLine.Remove(0, 3);

                    label.FontSize = 18;
                }

                if (tmpLine.StartsWith("[ADD]")) label.Foreground = Brushes.Green;
                else if (tmpLine.StartsWith("[FIX]")) label.Foreground = Brushes.Orange;
                else if (tmpLine.StartsWith("[REMOVE]")) label.Foreground = Brushes.Red;

                label.Content = tmpLine;

                ChangelogLines.Add(label);
            }
        }
        #endregion Methods
    }
}