namespace HelperTools.Updater.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>The SettingsUpdate.</summary>
    public class SettingsUpdate
    {
        /// <summary>Gets or sets the version.</summary>
        /// <value>The version.</value>
        [JsonProperty]
        public Version Version { get; set; }

        /// <summary>Gets or sets the last changes in ebglish.</summary>
        /// <value>The changes.</value>
        [JsonProperty]
        public string LastChangesEn { get; set; }

        /// <summary>Gets or sets the last changes in german.</summary>
        /// <value>The changes.</value>
        [JsonProperty]
        public string LastChangesDe { get; set; }

        /// <summary>Gets or sets the location.</summary>
        /// <value>The location.</value>
        [JsonProperty]
        public Uri Location { get; set; }

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