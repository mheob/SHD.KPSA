namespace HelperTools.MatFileGen.Models
{
    using System.Windows.Media;
    using Newtonsoft.Json;

    /// <summary>The SettingsThumbnail.</summary>
    public class SettingsThumbnail
    {
        /// <summary>Gets or sets a value indicating whether thumbnails should generate.</summary>
        /// <value><c>true</c> if thumbnails should generate; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool GenerateThumb { get; set; }

        /// <summary>Gets or sets the thumb folder.</summary>
        /// <value>The thumb folder.</value>
        [JsonProperty]
        public string ThumbFolder { get; set; }

        /// <summary>Gets or sets a value indicating whether the outer frame should generate.</summary>
        /// <value><c>true</c> if outer frame should generate; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool GenerateOuterFrame { get; set; }

        /// <summary>Gets or sets the color of the outer frame.</summary>
        /// <value>The color of the outer frame.</value>
        [JsonProperty]
        public Color OuterFrameColor { get; set; }

        /// <summary>Gets or sets the size of the outer frame.</summary>
        /// <value>The size of the outer frame.</value>
        [JsonProperty]
        public int OuterFrameSize { get; set; }

        /// <summary>Gets or sets a value indicating whether inner frame should generate.</summary>
        /// <value><c>true</c> if inner frame should generate; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool GenerateInnerFrame { get; set; }

        /// <summary>Gets or sets the color of the inner frame.</summary>
        /// <value>The color of the inner frame.</value>
        [JsonProperty]
        public Color InnerFrameColor { get; set; }

        /// <summary>Gets or sets the size of the inner frame.</summary>
        /// <value>The size of the inner frame.</value>
        [JsonProperty]
        public int InnerFrameSize { get; set; }
    }
}