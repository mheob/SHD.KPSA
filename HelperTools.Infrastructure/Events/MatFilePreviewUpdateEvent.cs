namespace HelperTools.Infrastructure.Events
{
    using System.Collections.Generic;
    using Prism.Events;

    /// <summary>The MatFilePreviewUpdateEvent.</summary>
    public class MatFilePreviewUpdateEvent : PubSubEvent<List<string>>
    {
    }
}