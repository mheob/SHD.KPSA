namespace HelperTools.Infrastructure.Behaviors
{
    using Prism.Regions;

    /// <summary>Specifies the <see cref="DialogActivationBehavior" /> class for using the behavior on WPF.</summary>
    /// <seealso cref="DialogActivationBehavior" />
    public class WindowDialogActivationBehavior : DialogActivationBehavior
    {
        /// <summary>
        /// Override this method to create an instance of the <see cref="IWindow" /> that will be shown when a view is
        /// activated.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IWindow" /> that will be shown when a view is activated on the target
        /// <see cref="IRegion" />.
        /// </returns>
        protected override IWindow CreateWindow()
        {
            return new WindowWrapper();
        }
    }
}