namespace HelperTools.Infrastructure.Services
{
    using System.Threading.Tasks;
    using System.Windows;
    using Constants;
    using Interfaces;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using Microsoft.Practices.Unity;
    using Properties;

    /// <summary>The MetroMessageDisplayService.</summary>
    /// <seealso cref="IMetroMessageDisplayService" />
    public class MetroMessageDisplayService : IMetroMessageDisplayService
    {
        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="MetroMessageDisplayService" /> class.</summary>
        /// <param name="container">The container.</param>
        public MetroMessageDisplayService(IUnityContainer container)
        {
            MainWindow = container.Resolve<Window>(WindowNames.MAIN_WINDOW_NAME) as MetroWindow;
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets the main window.</summary>
        /// <value>The main window.</value>
        public MetroWindow MainWindow { get; }
        #endregion Properties

        #region Methods
        /// <summary>Shows the input asynchronous.</summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The input string.</returns>
        public async Task<string> ShowInputAsync(string title, string message, MetroDialogSettings settings = null)
        {
            if (settings != null) MainWindow.MetroDialogOptions = settings;

            if (settings != null && settings.AffirmativeButtonText == null || settings == null)
                MainWindow.MetroDialogOptions.AffirmativeButtonText = Resources.DialogOk;

            if (settings != null && settings.NegativeButtonText == null || settings == null)
                MainWindow.MetroDialogOptions.NegativeButtonText = Resources.DialogCancel;

            return await MainWindow.ShowInputAsync(title, message, MainWindow.MetroDialogOptions);
        }

        /// <summary>Shows the message async.</summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="style">The style.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The MessageDialogResult</returns>
        public async Task<MessageDialogResult> ShowMessageAsync(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Affirmative, MetroDialogSettings settings = null)
        {
            if (settings != null) MainWindow.MetroDialogOptions = settings;

            if (settings != null && settings.AffirmativeButtonText == null || settings == null)
                MainWindow.MetroDialogOptions.AffirmativeButtonText = Resources.DialogYes;

            if (settings != null && settings.NegativeButtonText == null || settings == null)
                MainWindow.MetroDialogOptions.NegativeButtonText = Resources.DialogNo;

            return await MainWindow.ShowMessageAsync(title, message, style, MainWindow.MetroDialogOptions);
        }

        /// <summary>Shows the progress asynchronous.</summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="isCancelable">if set to <c>true</c> the progress is cancelable.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The MessageDialogResult</returns>
        public async Task<ProgressDialogController> ShowProgressAsync(string title, string message, bool isCancelable = false,
            MetroDialogSettings settings = null)
        {
            if (settings != null) MainWindow.MetroDialogOptions = settings;

            if (settings != null && settings.NegativeButtonText == null || settings == null)
                MainWindow.MetroDialogOptions.NegativeButtonText = Resources.DialogCancel;

            return await MainWindow.ShowProgressAsync(title, message, isCancelable, MainWindow.MetroDialogOptions);
        }
        #endregion Methods
    }
}