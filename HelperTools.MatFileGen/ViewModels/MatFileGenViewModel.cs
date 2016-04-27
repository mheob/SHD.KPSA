namespace HelperTools.MatFileGen.ViewModels
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Infrastructure.Events;
    using Infrastructure.Interfaces;
    using Models;
    using Prism.Commands;
    using Views;

    /// <summary>The MatFileGenViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    /// <seealso cref="IFileWorking" />
    public class MatFileGenViewModel : ViewModelBase, IFileWorking
    {
        #region Fields
        private const string EXTENSION = "*.jpg";

        private string selectedPath;
        private string solidColorName;
        private byte[] solidRgb;

        private int selectedVariantTab;

        private ObservableCollection<IFiles> fileCollection = new ObservableCollection<IFiles>();
        private ObservableCollection<IFiles> selectedFilesCollection = new ObservableCollection<IFiles>();
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="MatFileGenViewModel" /> class.</summary>
        public MatFileGenViewModel()
        {
            EventAggregator.GetEvent<SelectedPathUpdateEvent>().Subscribe(OnSelectedPathUpdateEvent);
            EventAggregator.GetEvent<SelectedFilesUpdateEvent>().Subscribe(OnSelectedFilesUpdateEvent);
            EventAggregator.GetEvent<SolidColorNameUpdateEvent>().Subscribe(OnSolidColorNameUpdateEvent);
            EventAggregator.GetEvent<SolidRgbUpdateEvent>().Subscribe(OnSolidRgbUpdateEvent);

            SelectedPath = PathNames.DesktopPath;

            GetFiles();

            StartGenerationCommand = new DelegateCommand(StartGeneration, CanStartGeneration);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets my view.</summary>
        /// <value>My view.</value>
        public string MyView => typeof(MatFileGen).ToString();

        /// <summary>Gets or sets the selected path.</summary>
        /// <value>The selected path.</value>
        public string SelectedPath
        {
            get { return selectedPath; }
            set { SetProperty(ref selectedPath, value); }
        }

        /// <summary>Gets or sets the name of the solid color.</summary>
        /// <value>The name of the solid color.</value>
        public string SolidColorName
        {
            get { return solidColorName; }
            set { SetProperty(ref solidColorName, value); }
        }

        /// <summary>Gets or sets the solid RGB.</summary>
        /// <value>The solid RGB.</value>
        public byte[] SolidRgb
        {
            get { return solidRgb; }
            set { SetProperty(ref solidRgb, value); }
        }

        /// <summary>Gets or sets the selected variant tab.</summary>
        /// <value>The selected variant tab.</value>
        public int SelectedVariantTab
        {
            get { return selectedVariantTab; }
            set { SetProperty(ref selectedVariantTab, value); }
        }

        /// <summary>Gets or sets the file collection.</summary>
        /// <value>The file collection.</value>
        public ObservableCollection<IFiles> FileCollection
        {
            get { return fileCollection; }
            set { SetProperty(ref fileCollection, value); }
        }

        /// <summary>Gets or sets the selected files collection.</summary>
        /// <value>The selected files collection.</value>
        public ObservableCollection<IFiles> SelectedFilesCollection
        {
            get { return selectedFilesCollection; }
            set { SetProperty(ref selectedFilesCollection, value); }
        }

        /// <summary>Gets the start generation command.</summary>
        /// <value>The start generation command.</value>
        public DelegateCommand StartGenerationCommand { get; }
        #endregion Properties

        #region Event-Handler
        private void OnSelectedPathUpdateEvent(string path)
        {
            SelectedPath = string.IsNullOrEmpty(path) ? selectedPath : path;

            GetFiles();
        }

        private void OnSelectedFilesUpdateEvent(ObservableCollection<IFiles> files)
        {
            SelectedFilesCollection = files;

            StartGenerationCommand.RaiseCanExecuteChanged();
        }

        private void OnSolidColorNameUpdateEvent(string colorName)
        {
            SolidColorName = colorName;
        }

        private void OnSolidRgbUpdateEvent(byte[] rgb)
        {
            SolidRgb = rgb;
        }
        #endregion Event-Handler

        #region Methods
        private void GetFiles()
        {
            var currentView = RegionManager.Regions[RegionNames.MAIN_REGION].ActiveViews.FirstOrDefault();

            if (currentView == null || !currentView.ToString().Equals(MyView))
            {
                return;
            }

            FileCollection.Clear();

            foreach (var file in Directory.GetFiles(SelectedPath, EXTENSION))
            {
                var fi = new FileInfo(file);

                FileCollection.Add(new MatFileGenFiles()
                {
                    FullFilePath = Path.GetFullPath(file),
                    FileName = Path.GetFileNameWithoutExtension(file),
                    CreatedTime = fi.CreationTimeUtc.ToLocalTime(),
                    FileSize = (fi.Length / 1024) + 1,
                    IsSelected = false
                });
            }

            EventAggregator.GetEvent<FilesUpdateEvent>().Publish(FileCollection);

            if (FileCollection.Count < 1)
            {
                return;
            }

            StartGenerationCommand.RaiseCanExecuteChanged();
        }

        private bool CanStartGeneration()
        {
            return SelectedFilesCollection.Count > 0;
        }

        private void StartGeneration()
        {
            if (SelectedVariantTab == 1)
            {
                SelectedFilesCollection.Clear();

                SelectedFilesCollection.Add(new MatFileGenFiles()
                {
                    FullFilePath = SelectedPath + SolidColorName + EXTENSION.Substring(1),
                    FileName = SolidColorName
                });
            }

            // TODO: get the settings via the event aggregator -- maybe these settings should be saved tempory in a JSON file

            var mfg = new FileGeneration()
            {
                GenerationFiles = SelectedFilesCollection,
                Extension = EXTENSION,
                SelectedPath = SelectedPath,
                SolidRgb = SolidRgb,
                IsFromJpg = SelectedVariantTab == 0
            };

            mfg.DoGenerationAsync();
        }
        #endregion Methods
    }
}