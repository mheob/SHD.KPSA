namespace HelperTools.Infrastructure.Interfaces
{
    /// <summary>The IContentService.</summary>
    public interface IContentService
    {
        /// <summary>Shows the content.</summary>
        /// <param name="contentName">Name of the content.</param>
        void ShowContent(string contentName);

        /// <summary>Determines whether this instance [can show the content] the specified content name.</summary>
        /// <param name="contentName">Name of the content.</param>
        /// <returns><c>treu</c>, if the content can be shown, else <c>false</c></returns>
        bool CanShowContent(string contentName);
    }
}