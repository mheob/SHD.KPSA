namespace HelperTools.Infrastructure.Events
{
    using System.Collections.ObjectModel;
    using Interfaces;
    using Prism.Events;

    /// <summary>The Selected3DsFilesUpdateEvent.</summary>
    public class Selected3DsFilesUpdateEvent : PubSubEvent<ObservableCollection<IFiles>>
    {
    }
}