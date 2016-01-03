using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using DataTools.Models;
using DataTools.Properties;
using DataTools.Utils;
using DataTools.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Utils;

namespace DataTools.ViewModels
{
    public class Tools3DsViewModel : BindableBase, ITools3DsViewModel
    {
        private readonly IRegionManager _regionManager;
        private string _selectedPath;
        private string _statusBarSummary;
        private readonly bool _isInitialize;
        private bool _isSelected;
        private ObservableCollection<SelectableFiles> _collection = new ObservableCollection<SelectableFiles>();
        private ObservableCollection<SelectableFiles> _selectedFiles = new ObservableCollection<SelectableFiles>();

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

        public Tools3DsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            //_selectedPath = Settings.Default.StartUpFilePath;
            //_selectedPath = Constants.DesktopPath;
            SelectedPath = @"C:\Users\mheob\Desktop\DataTools_Tests\3ds\";

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
            _regionManager.RequestNavigate(Constants.RegionContent, typeof (NavigationView).Name);
        }

        private static void ShutDownApplication()
        {
            Application.Current.Shutdown();
        }

        private void StartGeneration()
        {
            // ToDo: Start der Generierung ...
            //var tools3DsModel = new Tools3DsModel();
            //tools3DsModel.RemoveColorIn3Ds();
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
                ? string.Format(Resources.ListBoxFilesStatusBarText, SelectedFiles.Count, Collection.Count)
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

            foreach (var file in Directory.GetFiles(SelectedPath, "*.3ds"))
            {
                _collection.Add(new SelectableFiles() {FileName = Path.GetFileNameWithoutExtension(file), IsSelected = false});
            }
        }

        private void UpdateListBox()
        {
            StartGenerationCommand.RaiseCanExecuteChanged();
            SelectAllCommand.RaiseCanExecuteChanged();
            SelectNoneCommand.RaiseCanExecuteChanged();
        }
    }

    public interface ITools3DsViewModel
    {
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

    public class Tools3DsDesignViewModel : ITools3DsViewModel
    {
        public string SelectedPath { get; set; }
        public string StatusBarSummary { get; set; }
        public bool IsSelected { get; set; }
        public ObservableCollection<SelectableFiles> Collection { get; set; } = new ObservableCollection<SelectableFiles>();
        public ObservableCollection<SelectableFiles> SelectedFiles { get; set; } = new ObservableCollection<SelectableFiles>();

        public DelegateCommand NavigateBackCommand { get; set; }
        public DelegateCommand ShutDownApplicationCommand { get; set; }
        public DelegateCommand StartGenerationCommand { get; set; }
        public DelegateCommand TextBoxPathLostFocusCommand { get; set; }
        public DelegateCommand<string> GetDirectoryCommand { get; set; }
        public DelegateCommand UpdateStatusBarCommand { get; set; }
        public DelegateCommand SelectAllCommand { get; set; }
        public DelegateCommand SelectNoneCommand { get; set; }

        public Tools3DsDesignViewModel()
        {
            //SelectedPath = Settings.Default.StartUpFilePath;
            SelectedPath = @"D:\Desktop\";

            Collection.Add(new SelectableFiles() {FileName = "Testfile 1", IsSelected = false});
            Collection.Add(new SelectableFiles() {FileName = "Testfile 2", IsSelected = true});
            Collection.Add(new SelectableFiles() {FileName = "Testfile 3", IsSelected = false});

            StatusBarSummary = Resources.ListBoxFilesStatusBarText;
        }
    }
}