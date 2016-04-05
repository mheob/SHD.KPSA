namespace HelperTools.Infrastructure.Events
{
    using System.Collections.ObjectModel;
    using Interfaces;
    using Prism.Events;

    /// <summary>The FilesUpdateEvent.</summary>
    public class FilesUpdateEvent : PubSubEvent<ObservableCollection<IFiles>>
    {
    }
}