using DataTools.Models;
using DataTools.Properties;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SHD.KPSA.Utils;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace DataTools.ViewModels
{
    public class MatFileGeneratorViewModel : BindableBase, INavigationAware, IMatFileGeneratorViewModel
    {
        private const string Extension = "*.jpg";

        private IRegionNavigationJournal _navigationJournal;
        private string _title;
        private string _selectedPath;
        private string _statusBarSummary;
        private readonly bool _isInitialize;
        private bool _isSelected;

        private ObservableCollection<SelectableFiles> _collection =
            new ObservableCollection<SelectableFiles>();

        private ObservableCollection<SelectableFiles> _selectedFiles =
            new ObservableCollection<SelectableFiles>();


        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string SelectedPath
        {
            get { return _selectedPath; }
            set { SetProperty(ref _selectedPath, value); }
        }

        public string StatusBarSummary
        {
            get { return _statusBarSummary; }
            set { SetProperty(ref _statusBarSummary, value); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public ObservableCollection<SelectableFiles> Collection
        {
            get { return _collection; }
            set { SetProperty(ref _collection, value); }
        }

        public ObservableCollection<SelectableFiles> SelectedFiles
        {
            get { return _selectedFiles; }
            set { SetProperty(ref _selectedFiles, value); }
        }

        public DelegateCommand NavigateBackCommand { get; set; }
        public DelegateCommand ShutDownApplicationCommand { get; set; }
        public DelegateCommand StartGenerationCommand { get; set; }
        public DelegateCommand TextBoxPathLostFocusCommand { get; set; }
        public DelegateCommand<string> GetDirectoryCommand { get; set; }
        public DelegateCommand UpdateStatusBarCommand { get; set; }
        public DelegateCommand SelectAllCommand { get; set; }
        public DelegateCommand SelectNoneCommand { get; set; }

        public MatFileGeneratorViewModel()
        {
            _selectedPath = Settings.Default.StartUpFilePath;
            //_selectedPath = Constants.DesktopPath;

            CheckTextBoxPath();
            GetFiles();
            UpdateStatusBar();

            NavigateBackCommand = new DelegateCommand(NavigateBack);
            ShutDownApplicationCommand = new DelegateCommand(ShutDownApplication);
            StartGenerationCommand = new DelegateCommand(StartGeneration, CanStartGeneration);
            TextBoxPathLostFocusCommand = new DelegateCommand(CheckTextBoxPath);
            GetDirectoryCommand = new DelegateCommand<string>(GetDirectory);
            UpdateStatusBarCommand = new DelegateCommand(UpdateStatusBar);
            SelectAllCommand = new DelegateCommand(SelectAll, CanSelectAll);
            SelectNoneCommand = new DelegateCommand(SelectNone, CanSelectNone);

            _isInitialize = true;
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

        private void StartGeneration()
        {
            // ToDo: Methode "StartGeneration" erstellen
        }

        private bool CanStartGeneration()
        {
            return Collection.Count > 0;
        }

        private void CheckTextBoxPath()
        {
            if (SelectedPath.EndsWith("\\")) return;

            SelectedPath = SelectedPath + "\\";
        }

        private void GetDirectory(string path)
        {
            SelectedPath = Dialogs.OpenFolderDialog(path) ?? path;

            CheckTextBoxPath();
            GetFiles();
            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            StatusBarSummary = Collection.Count > 0
                ? string.Format(Resources.DataGridFilesStatusBarText, SelectedFiles.Count, Collection.Count)
                : string.Empty;

            if (!_isInitialize) return;

            UpdateListBox();
        }

        private void SelectAll()
        {
            SelectedFiles.Clear();

            foreach (var selectedFile in _collection)
            {
                selectedFile.IsSelected = true;
                SelectedFiles.Add(selectedFile);
            }

            SelectAllCommand.RaiseCanExecuteChanged();
        }

        private bool CanSelectAll()
        {
            return Collection.Count > SelectedFiles.Count && Collection.Count > 0;
        }

        private void SelectNone()
        {
            SelectedFiles.Clear();

            SelectAllCommand.RaiseCanExecuteChanged();
        }

        private bool CanSelectNone()
        {
            return SelectedFiles.Count > 0;
        }

        private void GetFiles()
        {
            _collection.Clear();

            foreach (var file in Directory.GetFiles(SelectedPath, Extension))
            {
                _collection.Add(new SelectableFiles
                {
                    FullFilePath = Path.GetFullPath(file),
                    FileName = Path.GetFileNameWithoutExtension(file),
                    LastModified = File.GetLastWriteTimeUtc(file).ToLocalTime(),
                    IsSelected = false
                });
            }
        }

        private void UpdateListBox()
        {
            StartGenerationCommand.RaiseCanExecuteChanged();
            SelectAllCommand.RaiseCanExecuteChanged();
            SelectNoneCommand.RaiseCanExecuteChanged();
        }
    }

    public interface IMatFileGeneratorViewModel
    {
        string Title { get; set; }
        string SelectedPath { get; set; }
        string StatusBarSummary { get; set; }
        bool IsSelected { get; set; }
        ObservableCollection<SelectableFiles> Collection { get; set; }
        ObservableCollection<SelectableFiles> SelectedFiles { get; set; }

        DelegateCommand NavigateBackCommand { get; set; }
        DelegateCommand ShutDownApplicationCommand { get; set; }
        DelegateCommand StartGenerationCommand { get; set; }
        DelegateCommand TextBoxPathLostFocusCommand { get; set; }
        DelegateCommand<string> GetDirectoryCommand { get; set; }
        DelegateCommand UpdateStatusBarCommand { get; set; }
        DelegateCommand SelectAllCommand { get; set; }
        DelegateCommand SelectNoneCommand { get; set; }
    }

    public class MatFileGeneratorDesignViewModel : IMatFileGeneratorViewModel
    {
        public string Title { get; set; }
        public string SelectedPath { get; set; }
        public string StatusBarSummary { get; set; }
        public bool IsSelected { get; set; }

        public ObservableCollection<SelectableFiles> Collection { get; set; } =
            new ObservableCollection<SelectableFiles>();

        public ObservableCollection<SelectableFiles> SelectedFiles { get; set; } =
            new ObservableCollection<SelectableFiles>();

        public DelegateCommand NavigateBackCommand { get; set; }
        public DelegateCommand ShutDownApplicationCommand { get; set; }
        public DelegateCommand StartGenerationCommand { get; set; }
        public DelegateCommand TextBoxPathLostFocusCommand { get; set; }
        public DelegateCommand<string> GetDirectoryCommand { get; set; }
        public DelegateCommand UpdateStatusBarCommand { get; set; }
        public DelegateCommand SelectAllCommand { get; set; }
        public DelegateCommand SelectNoneCommand { get; set; }

        public MatFileGeneratorDesignViewModel()
        {
            SelectedPath = Settings.Default.StartUpFilePath;
            //SelectedPath = @"D:\Desktop\";

            Collection.Add(new SelectableFiles()
            {
                FileName = "Testfile 1",
                LastModified = new DateTime(2016, 1, 3, 12, 40, 0),
                IsSelected = false
            });
            Collection.Add(new SelectableFiles()
            {
                FileName = "Testfile 2",
                LastModified = new DateTime(2016, 1, 2, 8, 5, 40),
                IsSelected = true
            });
            Collection.Add(new SelectableFiles()
            {
                FileName = "Testfile 3",
                LastModified = new DateTime(2015, 12, 30, 18, 15, 56),
                IsSelected = false
            });

            StatusBarSummary = Resources.DataGridFilesStatusBarText;
        }
    }
}