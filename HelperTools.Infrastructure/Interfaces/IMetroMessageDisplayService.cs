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
        /// <returns></returns>
        Task<MessageDialogResult> ShowMessageAsnyc(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative,
            MetroDialogSettings settings = null);
    }
}