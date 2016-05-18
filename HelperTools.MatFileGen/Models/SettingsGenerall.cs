namespace HelperTools.MatFileGen.Models
{
    using Newtonsoft.Json;

    /// <summary>The SettingsGenerall.</summary>
    public class SettingsGenerall
    {
        /// <summary>Gets or sets a value indicating whether the scale should added.</summary>
        /// <value><c>true</c> if the scale should added; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool AddScale { get; set; }

        /// <summary>Gets or sets the selected scale x.</summary>
        /// <value>The selected scale x.</value>
        [JsonProperty]
        public string SelectedScaleX { get; set; }

        /// <summary>Gets or sets the selected scale y.</summary>
        /// <value>The selected scale y.</value>
        [JsonProperty]
        public string SelectedScaleY { get; set; }

        /// <summary>Gets or sets the selected scale z.</summary>
        /// <value>The selected scale z.</value>
        [JsonProperty]
        public string SelectedScaleZ { get; set; }

        /// <summary>Gets or sets a value indicating whether the rotate should added.</summary>
        /// <value><c>true</c> if the rotate should added; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool AddRotate { get; set; }

        /// <summary>Gets or sets the selected rotate x.</summary>
        /// <value>The selected rotate x.</value>
        [JsonProperty]
        public string SelectedRotateX { get; set; }

        /// <summary>Gets or sets the selected rotate y.</summary>
        /// <value>The selected rotate y.</value>
        [JsonProperty]
        public string SelectedRotateY { get; set; }

        /// <summary>Gets or sets the selected rotate z.</summary>
        /// <value>The selected rotate z.</value>
        [JsonProperty]
        public string SelectedRotateZ { get; set; }

        /// <summary>Gets or sets a value indicating whether the shi should added.</summary>
        /// <value><c>true</c> if the shi should added; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool AddShi { get; set; }

        /// <summary>Gets or sets the shi.</summary>
        /// <value>The shi.</value>
        [JsonProperty]
        public string Shi { get; set; }

        /// <summary>Gets or sets a value indicating whether the ref should added.</summary>
        /// <value><c>true</c> if the ref should added; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool AddRef { get; set; }

        /// <summary>Gets or sets the reference.</summary>
        /// <value>The reference.</value>
        [JsonProperty]
        public string Ref { get; set; }

        /// <summary>Gets or sets a value indicating whether the tra should added.</summary>
        /// <value><c>true</c> if the tra should added; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool AddTra { get; set; }

        /// <summary>Gets or sets the tra.</summary>
        /// <value>The tra.</value>
        [JsonProperty]
        public string Tra { get; set; }

        /// <summary>Gets or sets a value indicating whether auto should added.</summary>
        /// <value><c>true</c> if auto should added; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool AddAuto { get; set; }

        /// <summary>Gets or sets a value indicating whether rauto should added.</summary>
        /// <value><c>true</c> if rauto should added; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool AddRauto { get; set; }

        /// <summary>Gets or sets a value indicating whether the type glass should added.</summary>
        /// <value><c>true</c> if the type glass should added; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool AddGlass { get; set; }

        /// <summary>Gets or sets a value indicating whether the mirror should added.</summary>
        /// <value><c>true</c> if the mirror should added; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool AddMirror { get; set; }

        /// <summary>Gets or sets the mirror.</summary>
        /// <value>The mirror.</value>
        [JsonProperty]
        public string Mirror { get; set; }
    }
}