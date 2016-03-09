namespace SHD.KPSA.Tools.MatFileGenerator.ViewModels
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows.Input;
    using System.Windows.Media;
    using KPSA.Utils;
    using Models;
    using Properties;
    using Utils;

    /// <summary>
    /// The ViewModel for the MatFileGenViewModel.
    /// </summary>
    public class MatFileGenViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private const string EXTENSION = "*.jpg";

        private bool isSelected;
        private bool isStatusBarVisible;
        private string selectedPath;
        private string statusBarSummary;

        private string selectedScaleX;
        private string selectedScaleY;
        private string selectedScaleZ;
        private string selectedRotateX;
        private string selectedRotateY;
        private string selectedRotateZ;

        private int selectedVariantTab;
        private string solidColorName;
        private Color selectedColor;

        private string thumbFolder;
        private int outerFrameSize;
        private int innerFrameSize;
        private Color outerFrameColor;
        private Color innerFrameColor;

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
            SelectedPath = Constants.DesktopPath;
            SelectedColor = Color.FromRgb(51, 68, 85);
            ThumbFolder = Resources.MatFileGenThumbFolderDefault;
            OuterFrameSize = int.Parse(Resources.MatFileGenTextBoxOuterSizeDefault);
            InnerFrameSize = int.Parse(Resources.MatFileGenTextBoxInnerSizeDefault);
            OuterFrameColor = Color.FromRgb(0, 0, 0);
            InnerFrameColor = Color.FromRgb(255, 255, 255);

            InitComboBoxes();
            CheckTextBoxPath(SelectedPath);
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
        /// Gets and sets the items for the scale combo box in X.
        /// </summary>
        public ObservableCollection<string> ComboBoxScaleCollectionX { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Gets and sets the items for the scale combo box in Y.
        /// </summary>
        public ObservableCollection<string> ComboBoxScaleCollectionY { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Gets and sets the items for the scale combo boxes in Z.
        /// </summary>
        public ObservableCollection<string> ComboBoxScaleCollectionZ { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Gets and sets the items for the rotate combo boxes in X.
        /// </summary>
        public ObservableCollection<string> ComboBoxRotateCollectionX { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Gets and sets the items for the rotate combo boxes in Y.
        /// </summary>
        public ObservableCollection<string> ComboBoxRotateCollectionY { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Gets and sets the items for the rotate combo boxes in Z.
        /// </summary>
        public ObservableCollection<string> ComboBoxRotateCollectionZ { get; } = new ObservableCollection<string>();

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
        /// Gets and sets the visibility of the status bar.
        /// </summary>
        public bool IsStatusBarVisible
        {
            get { return isStatusBarVisible; }
            set
            {
                if (value == isStatusBarVisible) return;
                isStatusBarVisible = value;
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
        /// Gets and sets the selected item from the ComboBoxScaleX.
        /// </summary>
        public string SelectedScaleX
        {
            get { return selectedScaleX; }
            set
            {
                if (value == selectedScaleX) return;
                selectedScaleX = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the selected item from the ComboBoxScaleY.
        /// </summary>
        public string SelectedScaleY
        {
            get { return selectedScaleY; }
            set
            {
                if (value == selectedScaleY) return;
                selectedScaleY = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the selected item from the ComboBoxScaleZ.
        /// </summary>
        public string SelectedScaleZ
        {
            get { return selectedScaleZ; }
            set
            {
                if (value == selectedScaleZ) return;
                selectedScaleZ = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the selected item from the ComboBoxRotateX.
        /// </summary>
        public string SelectedRotateX
        {
            get { return selectedRotateX; }
            set
            {
                if (value == selectedRotateX) return;
                selectedRotateX = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the selected item from the ComboBoxRotateY.
        /// </summary>
        public string SelectedRotateY
        {
            get { return selectedRotateY; }
            set
            {
                if (value == selectedRotateY) return;
                selectedRotateY = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the selected item from the ComboBoxRotateZ.
        /// </summary>
        public string SelectedRotateZ
        {
            get { return selectedRotateZ; }
            set
            {
                if (value == selectedRotateZ) return;
                selectedRotateZ = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the tab, where the settings for the generation variant can be found.
        /// </summary>
        public int SelectedVariantTab
        {
            get { return selectedVariantTab; }
            set
            {
                if (value == selectedVariantTab) return;
                selectedVariantTab = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the name for the solid color file.
        /// </summary>
        public string SolidColorName
        {
            get { return solidColorName; }
            set
            {
                if (value == solidColorName) return;
                solidColorName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the selected color for the solid generation.
        /// </summary>
        public Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                if (value.Equals(selectedColor)) return;
                selectedColor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the sub folder, where the thumbnails should store.
        /// </summary>
        public string ThumbFolder
        {
            get { return thumbFolder; }
            set
            {
                if (value == thumbFolder) return;
                thumbFolder = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the border size of the outer frame.
        /// </summary>
        public int OuterFrameSize
        {
            get { return outerFrameSize; }
            set
            {
                if (value == outerFrameSize) return;
                outerFrameSize = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the border size of the inner frame.
        /// </summary>
        public int InnerFrameSize
        {
            get { return innerFrameSize; }
            set
            {
                if (value == innerFrameSize) return;
                innerFrameSize = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the color of the outer frame.
        /// </summary>
        public Color OuterFrameColor
        {
            get { return outerFrameColor; }
            set
            {
                if (value.Equals(outerFrameColor)) return;
                outerFrameColor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the color of the inner frame.
        /// </summary>
        public Color InnerFrameColor
        {
            get { return innerFrameColor; }
            set
            {
                if (value.Equals(innerFrameColor)) return;
                innerFrameColor = value;
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
                checkTextBoxPathCommand = new RelayCommand(
                    param => CheckTextBoxPath((string) param),
                    param => param != null);

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
        /// Gets the command to unselect all files at once.
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

        private void CheckTextBoxPath(string path)
        {
            SelectedPath = path.EndsWith(@"\") ? path : path + @"\";

            GetFiles();
            UpdateStatusBar();
        }

        private void GetDirectory(string path)
        {
            SelectedPath = Dialogs.OpenFolderDialog(path) ?? path;

            CheckTextBoxPath(SelectedPath);
        }

        private void UpdateStatusBar()
        {
            StatusBarSummary = string.Format(Resources.DataGridFilesStatusBarText, SelectedFilesCollection.Count, FileCollection.Count);

            IsStatusBarVisible = FileCollection.Count > 0;
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

        private void InitComboBoxes()
        {
            const string initScale = "1.0";
            const string initRotate = "0";

            foreach (var comboBoxItem in Constants.GetComboBoxItems(Constants.ComboBoxVariant.Scale))
            {
                ComboBoxScaleCollectionX.Add(comboBoxItem);
                ComboBoxScaleCollectionY.Add(comboBoxItem);
                ComboBoxScaleCollectionZ.Add(comboBoxItem);
            }

            foreach (var comboBoxItem in Constants.GetComboBoxItems(Constants.ComboBoxVariant.Rotate))
            {
                ComboBoxRotateCollectionX.Add(comboBoxItem);
                ComboBoxRotateCollectionY.Add(comboBoxItem);
                ComboBoxRotateCollectionZ.Add(comboBoxItem);
            }

            SelectedScaleX = initScale;
            SelectedScaleY = initScale;
            SelectedScaleZ = initScale;

            SelectedRotateX = initRotate;
            SelectedRotateY = initRotate;
            SelectedRotateZ = initRotate;
        }
        #endregion Methods
    }
}