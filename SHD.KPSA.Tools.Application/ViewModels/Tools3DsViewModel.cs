namespace SHD.KPSA.Tools.Application.ViewModels
{
    using KPSA.Utils;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using Models;
    using Properties;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using Utils;

    public class Tools3DsViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private const string Extension = "*.3ds";

        private readonly bool _isInitialize;

        private bool _isSelected;
        private string _selectedPath;
        private string _statusBarSummary;

        private ObservableCollection<Tools3DsFiles> _fileCollection = new ObservableCollection<Tools3DsFiles>();
        private ObservableCollection<Tools3DsFiles> _selectedFilesCollection = new ObservableCollection<Tools3DsFiles>();

        private ICommand _checkTextBoxPathCommand;
        private ICommand _getDirectoryCommand;
        private ICommand _startGenerationCommand;
        private ICommand _updateStatusBarCommand;
        private ICommand _selectAllCommand;
        private ICommand _selectNoneCommand;
        #endregion Fields

        #region Constructor
        public Tools3DsViewModel()
        {
            SelectedPath = Settings.Default.StartUpFilePath;

            CheckTextBoxPath();
            GetFiles();
            UpdateStatusBar();

            _isInitialize = true;
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Gets the Title of the ChangelogView.
        /// </summary>
        public string Title => Resources.TitleTools3Ds;

        /// <summary>
        /// Gets and sets the selection state of the listbox items.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the path, where the Files are.
        /// </summary>
        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                if (value == _selectedPath) return;
                _selectedPath = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the content of the status bar from the listbox.
        /// </summary>
        public string StatusBarSummary
        {
            get { return _statusBarSummary; }
            set
            {
                if (value == _statusBarSummary) return;
                _statusBarSummary = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets a collection of the files in the selected path.
        /// </summary>
        public ObservableCollection<Tools3DsFiles> FileCollection
        {
            get { return _fileCollection; }
            set
            {
                if (Equals(value, _fileCollection)) return;
                _fileCollection = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets a collection of all selected files.
        /// </summary>
        public ObservableCollection<Tools3DsFiles> SelectedFilesCollection
        {
            get { return _selectedFilesCollection; }
            set
            {
                if (Equals(value, _selectedFilesCollection)) return;
                _selectedFilesCollection = value;
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
                if (_checkTextBoxPathCommand != null) return _checkTextBoxPathCommand;
                _checkTextBoxPathCommand = new RelayCommand(param => CheckTextBoxPath());
                return _checkTextBoxPathCommand;
            }
        }

        /// <summary>
        /// Gets the command to call up the directory with needed files.
        /// </summary>
        public ICommand GetDirectoryCommand
        {
            get
            {
                if (_getDirectoryCommand != null) return _getDirectoryCommand;

                _getDirectoryCommand = new RelayCommand(
                    param => GetDirectory((string) param),
                    param => param != null);

                return _getDirectoryCommand;
            }
        }

        public ICommand StartGenerationCommand
        {
            get
            {
                if (_startGenerationCommand != null) return _startGenerationCommand;

                _startGenerationCommand = new RelayCommand(
                    param => StartGeneration(),
                    param => FileCollection.Count > 0);

                return _startGenerationCommand;
            }
        }

        public ICommand UpdateStatusBarCommand
        {
            get
            {
                if (_updateStatusBarCommand != null) return _updateStatusBarCommand;
                _updateStatusBarCommand = new RelayCommand(param => UpdateStatusBar());
                return _updateStatusBarCommand;
            }
        }

        public ICommand SelectAllCommand
        {
            get
            {
                if (_selectAllCommand != null) return _selectAllCommand;

                _selectAllCommand = new RelayCommand(
                    param => SelectAll(),
                    param => FileCollection.Count > SelectedFilesCollection.Count && FileCollection.Count > 0);

                return _selectAllCommand;
            }
        }

        public ICommand SelectNoneCommand
        {
            get
            {
                if (_selectNoneCommand != null) return _selectNoneCommand;

                _selectNoneCommand = new RelayCommand(
                    param => SelectNone(),
                    param => SelectedFilesCollection.Count > 0);

                return _selectNoneCommand;
            }
        }
        #endregion Commands

        #region Methods
        private async void StartGeneration()
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();

            var controller =
                await window.ShowProgressAsync(Resources.ProgressDialogTitle, Resources.ProgressDialogPreviewContent);

            controller.SetIndeterminate();
            controller.SetCancelable(true);

            int sumFiles = SelectedFilesCollection.Count;
            int curFile = 0;

            controller.Maximum = sumFiles;

            await Task.Delay(1000);

            foreach (var file in SelectedFilesCollection.Select(selectedFile => selectedFile.FullFilePath))
            {
                if (File.Exists(file))
                {
                    try
                    {
                        Programs.OpenThirdParty("fix3ds.exe", " -m \"" + file + "\"", Constants.DefaultThirdPartyFolder);

                        controller.SetProgress(++curFile);
                        controller.SetMessage(string.Format(Resources.ProgressDialogRunningContent, curFile, sumFiles));

                        File.SetCreationTime(file, DateTime.Now);

                        await Task.Delay(200);
                    }
                    catch (Exception ex)
                    {
                        Dialogs.Exception(ex, Dialogs.ExceptionType.Universal);
                    }
                }

                if (controller.IsCanceled) break;
            }

            await controller.CloseAsync();

            GetFiles();

            if (controller.IsCanceled)
            {
                await window.ShowMessageAsync(Resources.MessageDialogCancelTitle, Resources.MessageDialogCancelContent);

                return;
            }

            if (await window.ShowMessageAsync(Resources.MessageDialogCompleteTitle,
                string.Format(Resources.MessageDialogCompleteContent, "\n"),
                MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                Programs.OpenExplorer(SelectedPath);
            }
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
                ? string.Format(Resources.DataGridFilesStatusBarText, SelectedFilesCollection.Count,
                    FileCollection.Count)
                : string.Empty;

            if (!_isInitialize) return;

            UpdateListBox();
        }

        private void SelectAll()
        {
            SelectedFilesCollection.Clear();

            foreach (var selectedFile in FileCollection)
            {
                selectedFile.IsSelected = true;
                SelectedFilesCollection.Add(selectedFile);
            }

            //SelectAllCommand.RaiseCanExecuteChanged();
        }

        private void SelectNone()
        {
            SelectedFilesCollection.Clear();

            //SelectAllCommand.RaiseCanExecuteChanged();
        }

        private void GetFiles()
        {
            FileCollection.Clear();

            foreach (var file in Directory.GetFiles(SelectedPath, Extension))
            {
                FileCollection.Add(new Tools3DsFiles()
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
            //StartGenerationCommand.RaiseCanExecuteChanged();
            //SelectAllCommand.RaiseCanExecuteChanged();
            //SelectNoneCommand.RaiseCanExecuteChanged();
        }
        #endregion Methods
    }
}