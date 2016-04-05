namespace HelperTools.Infrastructure.Events
{
    using System.Collections.ObjectModel;
    using Interfaces;
    using Prism.Events;

    /// <summary>The SelectedFilesUpdateEvent.</summary>
    public class SelectedFilesUpdateEvent : PubSubEvent<ObservableCollection<IFiles>>
    {
    }
}