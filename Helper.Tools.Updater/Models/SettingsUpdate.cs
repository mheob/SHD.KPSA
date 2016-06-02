namespace HelperTools.Updater.Models
{
    using Newtonsoft.Json;

    /// <summary>The SettingsUpdate.</summary>
    public class SettingsUpdate
    {
        /// <summary>Gets or sets the location.</summary>
        /// <value>The location.</value>
        [JsonProperty]
        public string Location { get; set; }

        /// <summary>Gets or sets the name of the file.</summary>
        /// <value>The name of the file.</value>
        [JsonProperty]
        public string FileName { get; set; }

        /// <summary></summary>
        /// <value>The MD5.</value>
        [JsonProperty]
        public string Md5 { get; set; }
    }
}