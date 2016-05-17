namespace HelperTools.MatFileGen.Models
{
    using System.Windows.Media;
    using Newtonsoft.Json;

    /// <summary>The SettingsSolid.</summary>
    public class SettingsSolid
    {
        /// <summary>Gets or sets the name of the solid color.</summary>
        /// <value>The name of the solid color.</value>
        [JsonProperty]
        public string SolidColorName { get; set; }

        /// <summary>Gets or sets the selected color.</summary>
        /// <value>The selected color.</value>
        [JsonProperty]
        public Color SelectedColor { get; set; }
    }
}