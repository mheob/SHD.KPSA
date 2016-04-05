namespace HelperTools.Infrastructure.Services
{
    using System.Threading.Tasks;
    using System.Windows;
    using Constants;
    using Interfaces;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using Microsoft.Practices.Unity;

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

        /// <summary>Shows the message async.</summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="style">The style.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The MessageDialogResult</returns>
        public async Task<MessageDialogResult> ShowMessageAsnyc(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Affirmative, MetroDialogSettings settings = null)
        {
            MainWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;

            return await MainWindow.ShowMessageAsync(title, message, style, MainWindow.MetroDialogOptions);
        }
    }
}