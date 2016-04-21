namespace HelperTools.Infrastructure.Interfaces
{
    using System.Threading.Tasks;
    using MahApps.Metro.Controls.Dialogs;

    /// <summary>The IMetroMessageDisplayService.</summary>
    public interface IMetroMessageDisplayService
    {
        /// <summary>Shows the message async.</summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="style">The style.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The MessageDialogResult.</returns>
        Task<MessageDialogResult> ShowMessageAsync(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative,
            MetroDialogSettings settings = null);

        /// <summary>Shows the progress asynchronous.</summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="isCancelable">if set to <c>true</c> the progress is cancelable.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The MessageDialogResult.</returns>
        Task<ProgressDialogController> ShowProgressAsync(string title, string message, bool isCancelable = false, MetroDialogSettings settings = null);
    }
}