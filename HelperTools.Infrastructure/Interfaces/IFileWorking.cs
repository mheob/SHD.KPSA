namespace HelperTools.Infrastructure.Interfaces
{
    using System.Collections.ObjectModel;
    using Prism.Commands;

    /// <summary>The IFileWorking.</summary>
    public interface IFileWorking
    {
        /// <summary>Gets or sets my view.</summary>
        /// <value>My view.</value>
        string MyView { get; }

        /// <summary>Gets or sets the selected path.</summary>
        /// <value>The selected path.</value>
        string SelectedPath { get; set; }

        /// <summary>Gets or sets the file collection.</summary>
        /// <value>The file collection.</value>
        ObservableCollection<IFiles> FileCollection { get; set; }

        /// <summary>Gets or sets the selected files collection.</summary>
        /// <value>The selected files collection.</value>
        ObservableCollection<IFiles> SelectedFilesCollection { get; set; }

        /// <summary>Gets the start generation command.</summary>
        /// <value>The start generation command.</value>
        DelegateCommand StartGenerationCommand { get; }
    }
}