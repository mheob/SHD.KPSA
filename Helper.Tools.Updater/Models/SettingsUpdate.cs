namespace HelperTools.Updater.Models
{
    using Newtonsoft.Json;

    /// <summary>The SettingsUpdate.</summary>
    public class SettingsUpdate
    {
        /// <summary>Gets or sets the version.</summary>
        /// <value>The version.</value>
        [JsonProperty]
        public string Version { get; set; }

        /// <summary>Gets or sets the URL.</summary>
        /// <value>The URL.</value>
        [JsonProperty]
        public string Url { get; set; }

        /// <summary>Gets or sets the name of the file.</summary>
        /// <value>The name of the file.</value>
        [JsonProperty]
        public string FileName { get; set; }

        /// <summary></summary>
        /// <value>The MD5.</value>
        [JsonProperty]
        public string Md5 { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [JsonProperty]
        public string Description { get; set; }

        /// <summary>Gets or sets the launch arguments.</summary>
        /// <value>The launch arguments.</value>
        [JsonProperty]
        public string LaunchArgs { get; set; }
    }
}