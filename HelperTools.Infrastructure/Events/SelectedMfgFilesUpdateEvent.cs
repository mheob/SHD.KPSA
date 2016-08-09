namespace HelperTools.Infrastructure.Events
{
    using System.Collections.ObjectModel;
    using Interfaces;
    using Prism.Events;

    /// <summary>The SelectedMfgFilesUpdateEvent.</summary>
    public class SelectedMfgFilesUpdateEvent : PubSubEvent<ObservableCollection<IFiles>>
    {
    }
}