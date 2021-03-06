﻿namespace HelperTools.Clean3Ds.ViewModels
{
    using System;
    using System.Reflection;
    using System.Windows.Input;
    using Infrastructure.Base;
    using Infrastructure.Constants;
    using Infrastructure.Events;
    using Infrastructure.Properties;
    using Infrastructure.Services;
    using Microsoft.Practices.Unity;
    using Prism.Commands;
    using Prism.Logging;

    /// <summary>The PathSelectionViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class PathSelectionViewModel : ViewModelBase
    {
        #region Fields
        private string selectedPath;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="PathSelectionViewModel" /> class.</summary>
        public PathSelectionViewModel()
        {
            selectedPath = PathNames.DesktopPath;

            GetDirectoryCommand = new DelegateCommand<string>(GetDirectory);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets the command to call up the directory with needed files.</summary>
        /// <value>The get directory command.</value>
        public ICommand GetDirectoryCommand { get; }

        /// <summary>Gets and sets the path, where the Files are.</summary>
        /// <value>The selected path.</value>
        public string SelectedPath
        {
            get
            {
                selectedPath = string.IsNullOrEmpty(selectedPath) || selectedPath.EndsWith(@"\") ? selectedPath : selectedPath + @"\";

                return selectedPath;
            }
            set
            {
                if (!SetProperty(ref selectedPath, value)) return;

                EventAggregator.GetEvent<SelectedPathUpdateEvent>().Publish(SelectedPath);
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(Resources.StatusBarSelectedPathChanged);
            }
        }
        #endregion Properties

        #region Methods
        private void GetDirectory(string path)
        {
            try
            {
                SelectedPath = DialogService.OpenFolderDialog(path) ?? path;
            }
            catch (Exception ex)
            {
                var logMessage = $"[{GetType().Name}] Exception at {MethodBase.GetCurrentMethod()}: {ex}";
                Container.Resolve<ILoggerFacade>().Log(logMessage, Category.Exception, Priority.High);

                DialogService.Exception(ex, DialogService.ExceptionType.Universal);
            }
        }
        #endregion Methods
    }
}