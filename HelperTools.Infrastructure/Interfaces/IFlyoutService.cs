namespace HelperTools.Infrastructure.Interfaces
{
    /// <summary>The IFlyoutService.</summary>
    public interface IFlyoutService
    {
        /// <summary>Shows the flyout.</summary>
        /// <param name="flyoutName">Name of the flyout.</param>
        void ShowFlyout(string flyoutName);

        /// <summary>Determines whether this instance [can show flyout] the specified flyout name.</summary>
        /// <param name="flyoutName">Name of the flyout.</param>
        /// <returns></returns>
        bool CanShowFlyout(string flyoutName);
    }
}