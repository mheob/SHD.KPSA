namespace HelperTools.Infrastructure.Events
{
    using System.Windows.Media.Imaging;
    using Prism.Events;

    /// <summary>The PreviewImageUpdateEvent.</summary>
    public class PreviewImageUpdateEvent : PubSubEvent<BitmapImage>
    {
    }
}