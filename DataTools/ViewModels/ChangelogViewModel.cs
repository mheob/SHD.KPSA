using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataTools.Properties;
using DataTools.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SHD.KPSA.Utils;

namespace DataTools.ViewModels
{
    public class ChangelogViewModel : BindableBase, INavigationAware, IChangelogViewModel
    {
        private IRegionNavigationJournal _navigationJournal;

        public int ScrollHeight { get; set; }

        public string Changelog { get; } = Constants.AppPath + Settings.Default.PathToChangelog;

        public string BuildNote
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                var date = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);

                return string.Format(Resources.LastChangeText, date);
            }
        }

        public ObservableCollection<Label> ChangelogLines { get; set; } = new ObservableCollection<Label>();

        public DelegateCommand NavigateBackCommand { get; set; }
        public DelegateCommand ShutDownApplicationCommand { get; set; }
        public DelegateCommand SetChangesCommand { get; set; }

        public ChangelogViewModel()
        {
            NavigateBackCommand = new DelegateCommand(NavigateBack);
            ShutDownApplicationCommand = new DelegateCommand(ShutDownApplication);
            SetChangesCommand = new DelegateCommand(SetChanges);
        }

        private void NavigateBack()
        {
            _navigationJournal.GoBack();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationJournal = navigationContext.NavigationService.Journal;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private static void ShutDownApplication()
        {
            Application.Current.Shutdown();
        }

        private void SetChanges()
        {
            if (!File.Exists(Changelog)) return;

            ScrollHeight = File.ReadLines(Changelog).Count() * 20;

            foreach (var line in FilesHandler.Read(Changelog))
            {
                if (line.Equals("# CHANGELOG")) continue;

                var label = new Label
                {
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = 14
                };

                var tmpLine = line;

                if (tmpLine.EndsWith("<br />"))
                {
                    tmpLine = tmpLine.Remove(tmpLine.Length - 6, 6);
                }

                if (tmpLine.StartsWith("## "))
                {
                    tmpLine = tmpLine.Remove(0, 3);

                    label.FontSize = 18;
                }

                if (tmpLine.StartsWith("[ADD]"))
                {
                    label.Foreground = Brushes.Green;
                }
                else if (tmpLine.StartsWith("[FIX]"))
                {
                    label.Foreground = Brushes.Orange;
                }
                else if (tmpLine.StartsWith("[REMOVE]"))
                {
                    label.Foreground = Brushes.Red;
                }

                label.Content = tmpLine;

                ChangelogLines.Add(label);
            }
        }
    }

    public interface IChangelogViewModel
    {
        int ScrollHeight { get; set; }
        string BuildNote { get; }
        ObservableCollection<Label> ChangelogLines { get; set; }

        DelegateCommand NavigateBackCommand { get; set; }
        DelegateCommand ShutDownApplicationCommand { get; set; }
        DelegateCommand SetChangesCommand { get; set; }
    }

    public class ChangelogDesignViewModel : IChangelogViewModel
    {
        public int ScrollHeight { get; set; }
        public string BuildNote { get; } = string.Format(Resources.LastChangeText, new DateTime(2016, 1, 7));
        public ObservableCollection<Label> ChangelogLines { get; set; } = new ObservableCollection<Label>();


        public DelegateCommand NavigateBackCommand { get; set; }
        public DelegateCommand ShutDownApplicationCommand { get; set; }
        public DelegateCommand SetChangesCommand { get; set; }

        public ChangelogDesignViewModel()
        {
            ChangelogLines.Add(new Label() {Content = "Zeile 1"});
            ChangelogLines.Add(new Label() {Content = "Zeile 2"});
            ChangelogLines.Add(new Label() {Content = "Zeile 3"});
        }
    }
}