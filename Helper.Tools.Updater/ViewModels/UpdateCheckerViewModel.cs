namespace HelperTools.Updater.ViewModels
{
    using System;
    using System.Reflection;
    using Infrastructure.Base;

    /// <summary>The UpdateCheckerViewModel.</summary>
    public class UpdateCheckerViewModel : ViewModelBase, IUpdateable
    {
        #region Fields
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="UpdateCheckerViewModel" /> class.</summary>
        public UpdateCheckerViewModel()
        {
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets the current version.</summary>
        /// <value>The current version.</value>
        public string CurrentVersion { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        #region Implementation of IUpdateable
        /// <summary>The name of your application as you want it displayed on the update form</summary>
        public string ApplicationName => "TestApp";

        /// <summary>
        /// An identifier string to use to identify your application in the update.xml Should be the same as your appId in the
        /// update.xml
        /// </summary>
        public string ApplicationId => "TestApp";

        /// <summary>The current assembly</summary>
        public Assembly ApplicationAssembly => Assembly.GetExecutingAssembly();

        /// <summary>The location of the update.xml on a server</summary>
        public Uri UpdateXmlLocation => new Uri("");
        #endregion

        #endregion Properties

        #region Methods
        #endregion Methods
    }
}