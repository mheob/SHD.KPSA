namespace HelperTools.Clean3Ds.ViewModels
{
    using Infrastructure.Base;
    using Infrastructure.Events;
    using Infrastructure.Interfaces;
    using Infrastructure.Properties;
    using Prism.Commands;
    using System.Collections.ObjectModel;

    /// <summary>The DataGridFilesViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class DataGridFilesViewModel : ViewModelBase
    {
        #region Fields
        private bool isSelected;
        private bool isStatusBarVisible;

        private string statusBarSummary;

        private ObservableCollection<IFiles> fileCollection = new ObservableCollection<IFiles>();
        private ObservableCollection<IFiles> selectedFilesCollection = new ObservableCollection<IFiles>();
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="DataGridFilesViewModel" /> class.</summary>
        public DataGridFilesViewModel()
        {
            EventAggregator.GetEvent<FilesUpdateEvent>().Subscribe(OnFilesUpdateEvent);

            InitializeCommands();
            RaiseCanExecuteChanged();
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets and sets the selection state of the listbox items.</summary>
        /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }

        /// <summary>Gets and sets the visibility of the status bar.</summary>
        /// <value><c>true</c> if the status bar is visible; otherwise, <c>false</c>.</value>
        public bool IsStatusBarVisible
        {
            get { return isStatusBarVisible; }
            set { SetProperty(ref isStatusBarVisible, value); }
        }

        /// <summary>Gets and sets the content of the status bar from the listbox.</summary>
        /// <value>The status bar summary.</value>
        public string StatusBarSummary
        {
            get { return statusBarSummary; }
            set { SetProperty(ref statusBarSummary, value); }
        }

        /// <summary>Gets and sets a collection of the files in the selected path.</summary>
        /// <value>The file collection.</value>
        public ObservableCollection<IFiles> FileCollection
        {
            get { return fileCollection; }
            set { SetProperty(ref fileCollection, value); }
        }

        /// <summary>Gets and sets a collection of all selected files.</summary>
        /// <value>The selected files collection.</value>
        public ObservableCollection<IFiles> SelectedFilesCollection
        {
            get { return selectedFilesCollection; }
            set { SetProperty(ref selectedFilesCollection, value); }
        }

        /// <summary>Gets the command to select all files at once.</summary>
        /// <value>The select all command.</value>
        public DelegateCommand SelectAllCommand { get; private set; }

        /// <summary>Gets the command to deselect all files at once.</summary>
        /// <value>The select none command.</value>
        public DelegateCommand SelectNoneCommand { get; private set; }

        /// <summary>Gets the command to update the status bar.</summary>
        /// <value>The update status bar command.</value>
        public DelegateCommand UpdateSelectedFilesCommand { get; private set; }
        #endregion Properties

        #region Event-Handler
        private void OnFilesUpdateEvent(ObservableCollection<IFiles> files)
        {
            IsStatusBarVisible = FileCollection.Count > 0;

            FileCollection = files;

            RaiseCanExecuteChanged();

            EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarFileCollectionChanged);
        }
        #endregion Event-Handler

        #region Methods
        private void InitializeCommands()
        {
            UpdateSelectedFilesCommand = new DelegateCommand(UpdateSelectedFiles);
            SelectAllCommand = new DelegateCommand(SelectAll, CanSelectAll);
            SelectNoneCommand = new DelegateCommand(SelectNone, CanSelectNone);
        }

        private void RaiseCanExecuteChanged()
        {
            UpdateSelectedFilesCommand.RaiseCanExecuteChanged();
            SelectAllCommand.RaiseCanExecuteChanged();
            SelectNoneCommand.RaiseCanExecuteChanged();
        }

        private void UpdateSelectedFiles()
        {
            StatusBarSummary = string.Format(Resources.DataGridFilesStatusBarText, SelectedFilesCollection.Count, FileCollection.Count);

            IsStatusBarVisible = FileCollection.Count > 0;

            EventAggregator.GetEvent<Selected3DsFilesUpdateEvent>().Publish(SelectedFilesCollection);

            RaiseCanExecuteChanged();

            if (SelectedFilesCollection.Count < 1)
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarSelectNone);
            else if (SelectedFilesCollection.Count.Equals(FileCollection.Count))
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarSelectAll);
        }

        private bool CanSelectAll()
        {
            return FileCollection.Count > SelectedFilesCollection.Count;
        }

        private void SelectAll()
        {
            SelectedFilesCollection.Clear();

            foreach (var selectedFile in FileCollection)
            {
                selectedFile.IsSelected = true;
                SelectedFilesCollection.Add(selectedFile);
            }

            RaiseCanExecuteChanged();

            EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarSelectAll);
        }

        private bool CanSelectNone()
        {
            return SelectedFilesCollection.Count > 0;
        }

        private void SelectNone()
        {
            SelectedFilesCollection.Clear();
            RaiseCanExecuteChanged();

            EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarSelectNone);
        }
        #endregion Methods
    }
}