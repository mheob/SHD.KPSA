namespace HelperTools.Infrastructure
{
    using Prism.Commands;

    /// <summary>The ApplicationCommands.</summary>
    public static class ApplicationCommands
    {
        /// <summary>The show flyout command.</summary>
        public static CompositeCommand ShowFlyoutCommand = new CompositeCommand();

        /// <summary>The show content command.</summary>
        public static CompositeCommand ShowContentCommand = new CompositeCommand();
    }

    /// <summary></summary>
    public interface IApplicationCommands
    {
        /// <summary>Gets the show flyout command.</summary>
        /// <value>The show flyout command.</value>
        CompositeCommand ShowFlyoutCommand { get; }

        /// <summary>Gets the show content command.</summary>
        /// <value>The show content command.</value>
        CompositeCommand ShowContentCommand { get; }
    }

    /// <summary></summary>
    /// <seealso cref="IApplicationCommands" />
    public class ApplicationCommandsProxy : IApplicationCommands
    {
        /// <summary>Gets the show flyout command.</summary>
        /// <value>The show flyout command.</value>
        public CompositeCommand ShowFlyoutCommand => ApplicationCommands.ShowFlyoutCommand;

        /// <summary>Gets the show content command.</summary>
        /// <value>The show content command.</value>
        public CompositeCommand ShowContentCommand => ApplicationCommands.ShowContentCommand;
    }
}