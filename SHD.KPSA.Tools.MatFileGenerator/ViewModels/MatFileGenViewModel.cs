﻿namespace SHD.KPSA.Tools.MatFileGenerator.ViewModels
{
    using KPSA.Utils;
    using Models;
    using Properties;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows.Input;
    using Utils;

    /// <summary>
    /// The ViewModel for the MatFileGenViewModel.
    /// </summary>
    public class MatFileGenViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private const string EXTENSION = "*.jpg";

        private bool isSelected;
        private string selectedPath;
        private string statusBarSummary;

        private ObservableCollection<MatFileGenFiles> fileCollection = new ObservableCollection<MatFileGenFiles>();
        private ObservableCollection<MatFileGenFiles> selectedFilesCollection = new ObservableCollection<MatFileGenFiles>();

        private ICommand checkTextBoxPathCommand;
        private ICommand getDirectoryCommand;
        private ICommand startGenerationCommand;
        private ICommand updateStatusBarCommand;
        private ICommand selectAllCommand;
        private ICommand selectNoneCommand;
        #endregion Fields

        #region Constructor
        /// <summary>
        /// Initialize a new instance of the Clean3DsViewModel class.
        /// </summary>
        public MatFileGenViewModel()
        {
            selectedPath = Settings.Default.StartUpFilePath;

            CheckTextBoxPath();
            GetFiles();
            UpdateStatusBar();
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Gets the Title of the ChangelogView.
        /// </summary>
        public string Title => Resources.TitleMatFileGenerator;

        /// <summary>
        /// Gets and sets the selection state of the listbox items.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value == isSelected) return;
                isSelected = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the path, where the Files are.
        /// </summary>
        public string SelectedPath
        {
            get { return selectedPath; }
            set
            {
                if (value == selectedPath) return;
                selectedPath = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the content of the status bar from the listbox.
        /// </summary>
        public string StatusBarSummary
        {
            get { return statusBarSummary; }
            set
            {
                if (value == statusBarSummary) return;
                statusBarSummary = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets a collection of the files in the selected path.
        /// </summary>
        public ObservableCollection<MatFileGenFiles> FileCollection
        {
            get { return fileCollection; }
            set
            {
                if (Equals(value, fileCollection)) return;
                fileCollection = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets a collection of all selected files.
        /// </summary>
        public ObservableCollection<MatFileGenFiles> SelectedFilesCollection
        {
            get { return selectedFilesCollection; }
            set
            {
                if (Equals(value, selectedFilesCollection)) return;
                selectedFilesCollection = value;
                OnPropertyChanged();
            }
        }
        #endregion Properties

        #region Commands
        /// <summary>
        /// Gets the command to check, if the path ends with a backslash.
        /// </summary>
        public ICommand CheckTextBoxPathCommand
        {
            get
            {
                if (checkTextBoxPathCommand != null) return checkTextBoxPathCommand;
                checkTextBoxPathCommand = new RelayCommand(param => CheckTextBoxPath());
                return checkTextBoxPathCommand;
            }
        }

        /// <summary>
        /// Gets the command to call up the directory with needed files.
        /// </summary>
        public ICommand GetDirectoryCommand
        {
            get
            {
                if (getDirectoryCommand != null) return getDirectoryCommand;

                getDirectoryCommand = new RelayCommand(
                    param => GetDirectory((string) param),
                    param => param != null);

                return getDirectoryCommand;
            }
        }

        /// <summary>
        /// Gets the command to start the working thread.
        /// </summary>
        public ICommand StartGenerationCommand
        {
            get
            {
                if (startGenerationCommand != null) return startGenerationCommand;

                startGenerationCommand = new RelayCommand(
                    param => StartGeneration(),
                    param => SelectedFilesCollection.Count > 0);

                return startGenerationCommand;
            }
        }

        /// <summary>
        /// Gets the command to update the status bar.
        /// </summary>
        public ICommand UpdateStatusBarCommand
        {
            get
            {
                if (updateStatusBarCommand != null) return updateStatusBarCommand;
                updateStatusBarCommand = new RelayCommand(param => UpdateStatusBar());
                return updateStatusBarCommand;
            }
        }

        /// <summary>
        /// Gets the command to select all files at once.
        /// </summary>
        public ICommand SelectAllCommand
        {
            get
            {
                if (selectAllCommand != null) return selectAllCommand;

                selectAllCommand = new RelayCommand(
                    param => SelectAll(),
                    param => FileCollection.Count > SelectedFilesCollection.Count);

                return selectAllCommand;
            }
        }

        /// <summary>
        /// Gets the command to deselect all files at once.
        /// </summary>
        public ICommand SelectNoneCommand
        {
            get
            {
                if (selectNoneCommand != null) return selectNoneCommand;

                selectNoneCommand = new RelayCommand(
                    param => SelectNone(),
                    param => SelectedFilesCollection.Count > 0);

                return selectNoneCommand;
            }
        }
        #endregion Commands

        #region Methods
        private void StartGeneration()
        {
            // ToDo: Methode "StartGeneration" erstellen
        }

        private void CheckTextBoxPath()
        {
            if (SelectedPath.EndsWith(@"\")) return;

            SelectedPath = SelectedPath + @"\";
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
            StatusBarSummary = FileCollection.Count > 0
                ? string.Format(Resources.DataGridFilesStatusBarText, SelectedFilesCollection.Count, FileCollection.Count)
                : string.Empty;
        }

        private void SelectAll()
        {
            SelectedFilesCollection.Clear();

            foreach (var selectedFile in FileCollection)
            {
                selectedFile.IsSelected = true;
                SelectedFilesCollection.Add(selectedFile);
            }
        }

        private void SelectNone()
        {
            SelectedFilesCollection.Clear();
        }

        private void GetFiles()
        {
            FileCollection.Clear();

            foreach (var file in Directory.GetFiles(SelectedPath, EXTENSION))
            {
                FileCollection.Add(new MatFileGenFiles
                {
                    FullFilePath = Path.GetFullPath(file),
                    FileName = Path.GetFileNameWithoutExtension(file),
                    LastModified = File.GetLastWriteTimeUtc(file).ToLocalTime(),
                    IsSelected = false
                });
            }
        }
        #endregion Methods
    }
}